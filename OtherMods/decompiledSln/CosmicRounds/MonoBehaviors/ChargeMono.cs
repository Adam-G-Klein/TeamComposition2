using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000077 RID: 119
	public class ChargeMono : MonoBehaviour
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00017F10 File Offset: 0x00016110
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.gun = base.GetComponent<Gun>();
			this.able = true;
			this.ResetTimer();
			this.ResetEffectTimer();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00017F60 File Offset: 0x00016160
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Charge")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
					return;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && this.player.data.block.IsOnCD() && !this.player.GetComponent<ChargeCDMono>())
				{
					this.player.transform.gameObject.AddComponent<ChargeCDMono>();
					this.ResetEffectTimer();
				}
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001804A File Offset: 0x0001624A
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001805E File Offset: 0x0001625E
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001806B File Offset: 0x0001626B
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x040003CE RID: 974
		private CharacterData data;

		// Token: 0x040003CF RID: 975
		private Block block;

		// Token: 0x040003D0 RID: 976
		public Player player;

		// Token: 0x040003D1 RID: 977
		private Gun gun;

		// Token: 0x040003D2 RID: 978
		public bool able;

		// Token: 0x040003D3 RID: 979
		private readonly float updateDelay = 0.1f;

		// Token: 0x040003D4 RID: 980
		private readonly float effectCooldown;

		// Token: 0x040003D5 RID: 981
		private float timeOfLastEffect;

		// Token: 0x040003D6 RID: 982
		private float startTime;

		// Token: 0x040003D7 RID: 983
		public int numcheck;
	}
}
