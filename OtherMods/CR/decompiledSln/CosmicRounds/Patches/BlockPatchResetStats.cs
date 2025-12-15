using System;
using CR.Extensions;
using HarmonyLib;

namespace CR.Patches
{
	// Token: 0x02000005 RID: 5
	[HarmonyPatch(typeof(Block), "ResetStats")]
	[Serializable]
	internal class BlockPatchResetStats
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000271E File Offset: 0x0000091E
		private static void Prefix(Block __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}
}
