using HarmonyLib;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnboundLib;
using UnboundLib.Extensions;
using UnboundLib.GameModes;

namespace RWF.Patches
{
    [HarmonyPatch(typeof(RoundCounter), "UpdatePoints")]
    class RoundCounter_Patch_UpdatePoints
    {
        static void Prefix(RoundCounter __instance) {
            __instance.GetData().teamPoints = null;
        }
    }

    [HarmonyPatch(typeof(RoundCounter), "UpdateRounds")]
    class RoundCounter_Patch_UpdateRounds
    {
        static void Prefix(RoundCounter __instance, int r1, int r2)
        {
            __instance.GetData().teamRounds = null;
        }
    }

    [HarmonyPatch(typeof(RoundCounter), "SetNumberOfRounds")]
    class RoundCounter_Patch_SetNumberOfRounds
    {
        static void Postfix(RoundCounter __instance)
        {
            if (__instance.gameObject.activeInHierarchy)
            {
                __instance.ExecuteAfterFrames(1, () => __instance.InvokeMethod("ReDraw"));
            }
        }
    }

    [HarmonyPatch(typeof(RoundCounter), "Clear")]
    class RoundCounter_Patch_Clear
    {
        static void Postfix(RoundCounter __instance) {
            // Remove extra player counters (originally there are two)
            var parent = __instance.p1Parent.parent;
            while (parent.transform.childCount > 3) {
                GameObject.DestroyImmediate(parent.transform.GetChild(3).gameObject);
            }
        }
    }

    [HarmonyPatch(typeof(RoundCounter), "GetPointPos")]
    class RoundCounter_Patch_GetPointPos
    {
        static bool Prefix(RoundCounter __instance, ref Vector3 __result, int teamID) {
            var teamPoints = __instance.GetData().teamPoints;
            var teamRounds = __instance.GetData().teamRounds;

            if (teamPoints == null || teamRounds == null) {
                return true;
            }

            if (!teamPoints.ContainsKey(teamID) || !teamRounds.ContainsKey(teamID)) {
                return true;
            }

            var parent = __instance.p1Parent.parent;
            var counter = parent.GetChild(teamID + 1);
            
            __result = counter.GetChild(teamRounds[teamID]).transform.position;
            return false;
        }
    }

    [HarmonyPatch(typeof(RoundCounter), "ReDraw")]
    class RoundCounter_Patch_ReDraw
    {
        static void Postfix(RoundCounter __instance) {
            // Remove extra player counters (originally there are two)
            var parent = __instance.p1Parent.parent;
            while (parent.transform.childCount > 3) {
                GameObject.DestroyImmediate(parent.transform.GetChild(3).gameObject);
            }

            var teamPoints = __instance.GetData().teamPoints;
            var teamRounds = __instance.GetData().teamRounds;

            if (teamPoints == null || teamRounds == null) {
                return;
            }

            // Only draw teams that exist in both dictionaries to avoid stale/mismatched keys
            var teamIds = teamPoints.Keys.Intersect(teamRounds.Keys).OrderBy(id => id).ToList();
            if (teamIds.Count == 0) {
                return;
            }

            var numTeams = teamIds.Count;
            var roundCountOrange = parent.Find("P1").gameObject;
            var roundCountBlue = parent.Find("P2").gameObject;
            var deltaY = roundCountBlue.transform.position.y - roundCountOrange.transform.position.y;
            var localDeltaY = roundCountBlue.transform.localPosition.y - roundCountOrange.transform.localPosition.y;

            // This postfix just adds handling for more than two teams, so we skip instantiating the first two
            for (int i = 0; i < numTeams; i++) {
                var teamId = teamIds[i];
                GameObject roundCountGo = null;

                if (i <= 1)
                {
                    roundCountGo = i == 0 ? roundCountOrange : roundCountBlue;
                }
                else if (i > 1)
                {
                    var newPos = roundCountOrange.transform.position + new Vector3(0, deltaY * i, 0);
                    roundCountGo = GameObject.Instantiate(roundCountOrange, newPos, Quaternion.identity, parent);
                    roundCountGo.transform.localPosition = roundCountOrange.transform.localPosition + new Vector3(0, localDeltaY * i, 0);
                    roundCountGo.transform.localScale = Vector3.one;
                }


                for (int j = 0; j < roundCountGo.transform.childCount; j++) {
                    var child = roundCountGo.transform.GetChild(j);
                    var rounds = teamRounds.ContainsKey(teamId) ? teamRounds[teamId] : 0;
                    var points = teamPoints.ContainsKey(teamId) ? teamPoints[teamId] : 0;

                    // fill rounds
                    if (rounds > j)
                    {
                        child.GetComponentInChildren<Image>().color = PlayerSkinBank.GetPlayerSkinColors(PlayerManager.instance.GetPlayersInTeam(teamId)[0].colorID()).color;
                        child.GetComponentInChildren<Image>().type = Image.Type.Simple;
                        child.localScale = Vector3.one;
                    }
                    else if (rounds == j && points > 0)
                    {
                        // use radial filling for points - so that any number of points per round are supported
                        child.GetComponentInChildren<Image>().color = PlayerSkinBank.GetPlayerSkinColors(PlayerManager.instance.GetPlayersInTeam(teamId)[0].colorID()).color;
                        child.GetComponentInChildren<Image>().type = Image.Type.Filled;
                        child.GetComponentInChildren<Image>().fillMethod = Image.FillMethod.Radial360;
                        child.GetComponentInChildren<Image>().fillAmount = (float) points / (int) GameModeManager.CurrentHandler.Settings["pointsToWinRound"];
                        child.localScale = new Vector3(1f, -1f, 1f);

                    }
                    else
                    {
                        // no partial points
                        child.GetComponentInChildren<Image>().color = __instance.offColor;
                        child.GetComponentInChildren<Image>().type = Image.Type.Simple;
                        child.localScale = Vector3.one * 0.3f;
                    }
                }
            }
        }
    }
}
