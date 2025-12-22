using HarmonyLib;
using InControl;
using ModdingUtils.AIMinion;
using System.Collections;
using System.Linq;
using TeamComposition2.Bots.Extensions;
using UnboundLib;
using UnityEngine;

namespace TeamComposition2.Bots.Patches
{
    [HarmonyPatch(typeof(PlayerAssigner))]
    internal class PlayerAssignerPatch
    {
        [HarmonyPatch("CreatePlayer")]
        public static bool Prefix(bool isAI, ref IEnumerator __result)
        {
            if (GameManager.instance.isPlaying && !AIMinionHandler.sandbox)
            {
                __result = EmptyEnumerator();
                return false;
            }
            else if (isAI)
            {
                BotManager.Instance.StartCoroutine(DelayedAIReplacement());
            }
            return true;
        }

        private static IEnumerator DelayedAIReplacement()
        {
            yield return null;

            Player player = PlayerManager.instance.players.Last();
            if (player == null)
            {
                Debug.LogError("Player could not be found.");
                yield break;
            }

            MonoBehaviour playerAI = player.GetComponentInChildren<PlayerAI>() ?? (MonoBehaviour)player.GetComponentInChildren<PlayerAIZorro>();
            if (playerAI != null)
            {
                if (playerAI is PlayerAIZorro playerAIZorro)
                {
                    player.data.healthHandler.delayedReviveAction -= playerAIZorro.Init;
                }

                playerAI.gameObject.AddComponent<PlayerAIPhilip>();
                player.data.GetAdditionalData().IsBot = true;

                Object.Destroy(playerAI);
            }
        }

        private static IEnumerator EmptyEnumerator()
        {
            yield break;
        }
    }
}
