using System;
using System.Collections.Generic;
using CR.Extensions;
using HarmonyLib;

namespace CR.Patches
{
	// Token: 0x02000003 RID: 3
	[HarmonyPatch(typeof(Player), "FullReset")]
	[Serializable]
	internal class PlayerPatchFullReset
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000026CF File Offset: 0x000008CF
		private static void Prefix(Player __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
			__instance.data.currentCards = new List<CardInfo>();
		}
	}
}
