using System;
using System.Collections.Generic;
using CR.Extensions;
using HarmonyLib;

namespace CR.Patches
{
	// Token: 0x02000004 RID: 4
	[HarmonyPatch(typeof(Gun), "ResetStats")]
	[Serializable]
	internal class GunPatchResetStats
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000026F4 File Offset: 0x000008F4
		private static void Prefix(Gun __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
			__instance.player.data.currentCards = new List<CardInfo>();
		}
	}
}
