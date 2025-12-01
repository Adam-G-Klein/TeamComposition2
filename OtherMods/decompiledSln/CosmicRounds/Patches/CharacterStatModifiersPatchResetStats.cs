using System;
using CR.Extensions;
using HarmonyLib;

namespace CR.Patches
{
	// Token: 0x02000006 RID: 6
	[HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
	[Serializable]
	internal class CharacterStatModifiersPatchResetStats
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002733 File Offset: 0x00000933
		private static void Prefix(CharacterStatModifiers __instance)
		{
			CustomEffects.DestroyAllEffects(__instance.gameObject);
		}
	}
}
