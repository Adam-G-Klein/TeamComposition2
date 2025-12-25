using System;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Networking;
using UnityEngine;
using System.Linq;

namespace TeamComposition2
{
    /// <summary>
    /// Retreat and Regroup – activating your ability blinks you half a screen back toward your spawn.
    /// Uses the former block button and respects block cooldown.
    /// </summary>
    public class RetreatAndRegroupCard : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            // No direct stat changes; the effect triggers on ability (block) use.
        }

        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var effect = player.gameObject.GetComponent<RetreatAndRegroupEffect>() ?? player.gameObject.AddComponent<RetreatAndRegroupEffect>();
            effect.Initialize(player, block);
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var effect = player.gameObject.GetComponent<RetreatAndRegroupEffect>();
            if (effect != null)
            {
                GameObject.Destroy(effect);
            }
        }

        protected override string GetTitle() => "Retreat and Regroup";

        protected override string GetDescription() => "Activate ability to blink half a screen back toward your spawn.";

        protected override GameObject GetCardArt() => MyPlugin.asset?.LoadAsset<GameObject>("C_HealingField");

        protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Uncommon;

        protected override CardInfoStat[] GetStats()
        {
            return new[]
            {
                new CardInfoStat
                {
                    positive = true,
                    stat = "Ability",
                    amount = "Teleport toward spawn",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.DefensiveBlue;

        public override string GetModName() => MyPlugin.modInitials;
    }

    /// <summary>
    /// Handles the half-screen teleport toward the player's spawn when their ability (block) is used.
    /// </summary>
    public class RetreatAndRegroupEffect : MonoBehaviour
    {
        private const float FallbackHalfWidth = 8f;

        private Player player;
        private Block block;
        private Action<BlockTrigger.BlockTriggerType> blockHandler;

        public void Initialize(Player owner, Block ownerBlock)
        {
            player = owner;
            block = ownerBlock != null ? ownerBlock : owner?.GetComponent<Block>();
            Attach();
        }

        private void OnDestroy()
        {
            Detach();
        }

        private void Attach()
        {
            if (block == null)
            {
                return;
            }

            Detach();
            blockHandler = OnBlock;
            block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(block.BlockAction, blockHandler);
        }

        private void Detach()
        {
            if (block != null && blockHandler != null)
            {
                block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Remove(block.BlockAction, blockHandler);
            }

            blockHandler = null;
        }

        private void OnBlock(BlockTrigger.BlockTriggerType _)
        {
            if (player == null)
            {
                Detach();
                return;
            }

            Vector3 spawn = GetSpawnPosition(player);
            Vector3 current = player.transform.position;
            Vector3 toSpawn = spawn - current;
            if (toSpawn.sqrMagnitude < 0.01f)
            {
                return;
            }

            float step = Mathf.Min(GetHalfScreenWidth(), toSpawn.magnitude);
            Vector3 target = current + toSpawn.normalized * step;

            NetworkingManager.RPC(typeof(RetreatAndRegroupEffect), nameof(RPCA_Teleport), player.playerID, target);
        }

        private static float GetHalfScreenWidth()
        {
            Camera cam = MainCam.instance?.cam ?? Camera.main;
            if (cam == null)
            {
                return FallbackHalfWidth;
            }

            return cam.orthographicSize * cam.aspect;
        }

        private static Vector3 GetSpawnPosition(Player p)
        {
            Vector3 spawnPos;
            if (TeamSpawnManager.TryGetTeamSpawn(p.teamID, out spawnPos))
            {
                return spawnPos;
            }

            return p.transform.position;
        }

        [UnboundRPC]
        private static void RPCA_Teleport(int playerId, Vector3 targetPosition)
        {
            var player = GetPlayerById(playerId);
            if (player == null)
            {
                return;
            }

            player.transform.position = targetPosition;
            if (player.data?.healthHandler != null)
            {
                player.data.healthHandler.transform.position = targetPosition;
            }

            // Skip velocity reset; rely on existing physics to settle after teleport.
        }

        private static Player GetPlayerById(int playerId)
        {
            var players = PlayerManager.instance?.players;
            if (players == null)
            {
                return null;
            }

            return players.FirstOrDefault(p => p != null && p.playerID == playerId);
        }
    }
}

