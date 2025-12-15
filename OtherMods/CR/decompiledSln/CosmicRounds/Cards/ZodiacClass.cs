using System;
using System.Collections;
using ClassesManagerReborn;

namespace CR.Cards
{
	// Token: 0x020000FF RID: 255
	internal class ZodiacClass : ClassHandler
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x00025B96 File Offset: 0x00023D96
		public override IEnumerator Init()
		{
			while (!ZodiacCard.card || !AquariusCard.card || !PiscesCard.card || !AriesCard.card || !TaurusCard.card || !GeminiCard.card || !CancerCard.card || !LeoCard.card || !VirgoCard.card || !LibraCard.card || !ScorpioCard.card || !SagittariusCard.card || !CapricornCard.card || !ZodiacCardPlus.card || !AquariusCardPlus.card || !PiscesCardPlus.card || !AriesCardPlus.card || !TaurusCardPlus.card || !GeminiCardPlus.card || !CancerCardPlus.card || !LeoCardPlus.card || !VirgoCardPlus.card || !LibraCardPlus.card || !ScorpioCardPlus.card || !SagittariusCardPlus.card || !CapricornCardPlus.card)
			{
				yield return null;
			}
			ClassesRegistry.Register(ZodiacCard.card, 1, 0);
			ClassesRegistry.Register(AquariusCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(PiscesCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(AriesCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(TaurusCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(GeminiCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(CancerCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(LeoCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(VirgoCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(LibraCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(ScorpioCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(SagittariusCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(CapricornCard.card, 16, ZodiacCard.card, 0);
			ClassesRegistry.Register(ZodiacCardPlus.card, 8, ZodiacCard.card, 0);
			ClassesRegistry.Register(AquariusCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(PiscesCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(AriesCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(TaurusCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(GeminiCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(CancerCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(LeoCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(VirgoCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(LibraCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(ScorpioCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(SagittariusCardPlus.card, 16, ZodiacCardPlus.card, 0);
			ClassesRegistry.Register(CapricornCardPlus.card, 16, ZodiacCardPlus.card, 0);
			yield break;
		}

		// Token: 0x040004BE RID: 1214
		internal static string name = "Zodiac";
	}
}
