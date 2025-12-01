using System;
using CR.Extensions;
using HarmonyLib;
using UnboundLib.Cards;

namespace CR.Patches
{
	// Token: 0x02000007 RID: 7
	[HarmonyPatch(typeof(CustomCard), "OnRemoveCard")]
	[Serializable]
	internal class CustomCardOnRemoveCard
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002748 File Offset: 0x00000948
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}
}
