using System;
using BepInEx.Configuration;
using HarmonyLib;
using Photon.Pun;
using TMPro;
using UnboundLib;
using UnboundLib.Networking;
using UnboundLib.Utils.UI;
using UnityEngine;

namespace TeamComposition2
{
    public static class FriendlyFireManager
    {
        private const string ModId = "com.adamklein.teamcomposition.friendlyfire";
        private const string MenuName = "Toggle Friendly Fire";

        private static ConfigEntry<bool> DisableFriendlyFireConfig;
        private static ConfigEntry<bool> DisableSelfDamageConfig;
        private static ConfigEntry<bool> DisableHitEffectsConfig;

        internal static bool disableFriendlyFire;
        internal static bool disableSelfDamage;
        internal static bool disableHitEffects;

        public static void Initialize(ConfigFile config)
        {
            // bind configs
            DisableFriendlyFireConfig = config.Bind("FriendlyFire", "DisableFriendlyFire", false, "Disable friendly fire damage");
            DisableSelfDamageConfig = config.Bind("FriendlyFire", "DisableSelfDamage", false, "Disable self damage");
            DisableHitEffectsConfig = config.Bind("FriendlyFire", "DisableHitEffects", false, "Disable hit effects on friendly fire or self damage if either is disabled");

            // read config values
            disableFriendlyFire = DisableFriendlyFireConfig.Value;
            disableSelfDamage = DisableSelfDamageConfig.Value;
            disableHitEffects = DisableHitEffectsConfig.Value;

            // apply Harmony patches
            new Harmony(ModId).PatchAll(typeof(ProjectileHitPatchHit));
            new Harmony(ModId).PatchAll(typeof(ProjectileHitPatchRPCA_DoHit));
            new Harmony(ModId).PatchAll(typeof(HealthHandlerPatchDoDamage));
        }

        public static void RegisterMenu()
        {
            Unbound.RegisterMenu(MenuName, () => { }, BuildMenu, null, true);
            Unbound.RegisterHandshake(ModId, OnHandShakeCompleted);
        }

        private static bool CanChangeSettings()
        {
            return !PhotonNetwork.IsConnected || PhotonNetwork.IsMasterClient;
        }

        private static void BuildMenu(GameObject menu)
        {
            MenuHandler.CreateText(MenuName + " Options", menu, out TextMeshProUGUI _, 60);
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI warningText, 30, color: Color.red);

            void FriendlyFireCheckbox(bool flag)
            {
                if (!CanChangeSettings()) { warningText.text = "ONLY THE HOST CAN CHANGE THESE SETTINGS MID-GAME"; return; }
                else { warningText.text = " "; }
                DisableFriendlyFireConfig.Value = flag;
                disableFriendlyFire = flag;
                OnHandShakeCompleted();
            }
            MenuHandler.CreateToggle(disableFriendlyFire, "Disable Friendly Fire", menu, FriendlyFireCheckbox, 60);

            void SelfDamageCheckbox(bool flag)
            {
                if (!CanChangeSettings()) { warningText.text = "ONLY THE HOST CAN CHANGE THESE SETTINGS MID-GAME"; return; }
                else { warningText.text = " "; }
                DisableSelfDamageConfig.Value = flag;
                disableSelfDamage = flag;
                OnHandShakeCompleted();
            }
            MenuHandler.CreateToggle(disableSelfDamage, "Disable Self Damage", menu, SelfDamageCheckbox, 60);

            void HitEffectsCheckbox(bool flag)
            {
                if (!CanChangeSettings()) { warningText.text = "ONLY THE HOST CAN CHANGE THESE SETTINGS MID-GAME"; return; }
                else { warningText.text = " "; }
                DisableHitEffectsConfig.Value = flag;
                disableHitEffects = flag;
                OnHandShakeCompleted();
            }
            MenuHandler.CreateToggle(disableHitEffects, "Disable Hit Effects for any disabled friendly fire or self damage", menu, HitEffectsCheckbox, 60);

            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, 30);
        }

        private static void OnHandShakeCompleted()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                NetworkingManager.RPC_Others(typeof(FriendlyFireManager), nameof(SyncSettings), new object[] { disableFriendlyFire, disableSelfDamage, disableHitEffects });
            }
        }

        [UnboundRPC]
        private static void SyncSettings(bool friendly, bool self, bool hit)
        {
            disableFriendlyFire = friendly;
            disableSelfDamage = self;
            disableHitEffects = hit;
        }
    }

    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "Hit")]
    internal class ProjectileHitPatchHit
    {
        // disable friendly fire or self damage and hit effects if that setting is enabled
        private static bool Prefix(ProjectileHit __instance, HitInfo hit, bool forceCall)
        {
            // if disableHitEffects is false, then ProjectileHit.RPCA_DoHit will handle all of this
            if (!FriendlyFireManager.disableHitEffects)
            {
                return true;
            }

            int num = -1;
            if (hit.transform)
            {
                PhotonView component = hit.transform.root.GetComponent<PhotonView>();
                if (component)
                {
                    num = component.ViewID;
                }
            }
            int num2 = -1;
            if (num == -1)
            {
                Collider2D[] componentsInChildren = MapManager.instance.currentMap.Map.GetComponentsInChildren<Collider2D>();
                for (int i = 0; i < componentsInChildren.Length; i++)
                {
                    if (componentsInChildren[i] == hit.collider)
                    {
                        num2 = i;
                    }
                }
            }
            HealthHandler healthHandler = null;
            if (hit.transform)
            {
                healthHandler = hit.transform.GetComponent<HealthHandler>();
            }
            if (healthHandler)
            {
                Player hitPlayer = healthHandler.GetComponent<Player>();
                // if the hit player is not null
                if (hitPlayer != null)
                {
                    // self-damage
                    if (hitPlayer.playerID == __instance.ownPlayer.playerID && FriendlyFireManager.disableSelfDamage)
                    {
                        return false;
                    }
                    // friendly fire
                    else if (hitPlayer.playerID != __instance.ownPlayer.playerID && hitPlayer.teamID == __instance.ownPlayer.teamID && FriendlyFireManager.disableFriendlyFire)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "RPCA_DoHit")]
    internal class ProjectileHitPatchRPCA_DoHit
    {
        // prefix to disable bullet hits (BUT RETAIN HITEFFECTS LIKE FLIP) for friendly fire and self damage
        [HarmonyBefore("pykess.rounds.plugins.moddingutils")]
        private static bool Prefix(ProjectileHit __instance, Vector2 hitPoint, Vector2 hitNormal, Vector2 vel, int viewID, int colliderID, bool wasBlocked)
        {
            // if disableHitEffects is true, then ProjectileHit.Hit has already handled all of this
            if (FriendlyFireManager.disableHitEffects)
            {
                return true;
            }

            if (__instance.ownPlayer != null)
            {
                // get the thing the bullet hit
                HitInfo hitInfo = new HitInfo();
                hitInfo.point = hitPoint;
                hitInfo.normal = hitNormal;
                hitInfo.collider = null;
                if (viewID != -1)
                {
                    PhotonView photonView = PhotonNetwork.GetPhotonView(viewID);
                    hitInfo.collider = photonView.GetComponentInChildren<Collider2D>();
                    hitInfo.transform = photonView.transform;
                }
                else if (colliderID != -1)
                {
                    hitInfo.collider = MapManager.instance.currentMap.Map.GetComponentsInChildren<Collider2D>()[colliderID];
                    hitInfo.transform = hitInfo.collider.transform;
                }
                HealthHandler healthHandler = null;
                if (hitInfo.transform)
                {
                    healthHandler = hitInfo.transform.GetComponent<HealthHandler>();
                }
                if (healthHandler != null)
                {
                    Player hitPlayer = healthHandler.gameObject.GetComponent<Player>();

                    // if the hit player is not null
                    if (hitPlayer != null)
                    {
                        // self-damage
                        if (hitPlayer.playerID == __instance.ownPlayer.playerID && FriendlyFireManager.disableSelfDamage)
                        {
                            return false;
                        }
                        // friendly fire
                        else if (hitPlayer.playerID != __instance.ownPlayer.playerID && hitPlayer.teamID == __instance.ownPlayer.teamID && FriendlyFireManager.disableFriendlyFire)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }

    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "DoDamage")]
    internal class HealthHandlerPatchDoDamage
    {
        // prefix to disable damage for friendly fire and self damage
        private static bool Prefix(HealthHandler __instance, Vector2 damage, Vector2 position, Color blinkColor, GameObject damagingWeapon = null, Player damagingPlayer = null, bool healthRemoval = false, bool lethal = true, bool ignoreBlock = false)
        {
            Player ownPlayer = __instance.GetComponent<Player>();
            if (damagingPlayer != null && ownPlayer != null)
            {
                // self-damage
                if (damagingPlayer.playerID == ownPlayer.playerID && FriendlyFireManager.disableSelfDamage)
                {
                    return false;
                }
                // friendly fire
                else if (damagingPlayer.playerID != ownPlayer.playerID && damagingPlayer.teamID == ownPlayer.teamID && FriendlyFireManager.disableFriendlyFire)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
