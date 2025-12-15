using System;
using System.Linq;
using CR.Cards;
using ModdingUtils.Utils;
using Sonigon;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200008B RID: 139
	public class ZodiacMono : MonoBehaviour
	{
		// Token: 0x0600038B RID: 907 RVA: 0x0001B464 File Offset: 0x00019664
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.gun = base.GetComponent<Gun>();
			this.able = false;
			this.ResetTimer();
			this.ResetEffectTimer();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001B4B4 File Offset: 0x000196B4
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Zodiac")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
				}
				else
				{
					foreach (CardInfo cardInfo in this.player.data.currentCards.ToList<CardInfo>())
					{
						if (cardInfo.categories.Contains(ZodiacCard.ZodiacClass))
						{
							Unbound.Instance.StartCoroutine(Cards.instance.ReplaceCard(this.player, cardInfo, Cards.instance.GetCardWithObjectName(cardInfo.name + "Plus"), "", 0f, 0f, 1, true));
						}
					}
				}
				this.ResetTimer();
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001B5F0 File Offset: 0x000197F0
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
			this.zodi = 0;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001B60B File Offset: 0x0001980B
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001B618 File Offset: 0x00019818
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0400048E RID: 1166
		private CharacterData data;

		// Token: 0x0400048F RID: 1167
		private Block block;

		// Token: 0x04000490 RID: 1168
		public Player player;

		// Token: 0x04000491 RID: 1169
		private Gun gun;

		// Token: 0x04000492 RID: 1170
		public bool able;

		// Token: 0x04000493 RID: 1171
		private readonly float updateDelay = 0.4f;

		// Token: 0x04000494 RID: 1172
		private float timeOfLastEffect;

		// Token: 0x04000495 RID: 1173
		private float startTime;

		// Token: 0x04000496 RID: 1174
		public int numcheck;

		// Token: 0x04000497 RID: 1175
		private int zodi;

		// Token: 0x04000498 RID: 1176
		public static SoundEvent fieldsound;

		// Token: 0x04000499 RID: 1177
		public CardInfo[] zodiCards;
	}
}
