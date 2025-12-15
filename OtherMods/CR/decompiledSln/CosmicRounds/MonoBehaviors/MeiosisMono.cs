using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200001C RID: 28
	public class MeiosisMono : MonoBehaviour
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00005F10 File Offset: 0x00004110
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

		// Token: 0x06000095 RID: 149 RVA: 0x00005F60 File Offset: 0x00004160
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				int i = 0;
				if (this.player.data.currentCards != null)
				{
					while (i <= this.player.data.currentCards.Count - 1)
					{
						if (this.player.data.currentCards[i].cardName == "Meiosis")
						{
							this.numcheck++;
						}
						i++;
					}
				}
				if (this.numcheck > 0)
				{
					if (Time.time >= this.timeOfLastEffect + this.effectCooldown && this.player.data.weaponHandler.gun.isReloading && this.able)
					{
						this.player.transform.gameObject.AddComponent<MeiosisReloadMono>();
						this.ResetEffectTimer();
						this.able = false;
						return;
					}
				}
				else
				{
					this.Destroy();
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000605E File Offset: 0x0000425E
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000606B File Offset: 0x0000426B
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006078 File Offset: 0x00004278
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x040000AA RID: 170
		private CharacterData data;

		// Token: 0x040000AB RID: 171
		private Block block;

		// Token: 0x040000AC RID: 172
		public Player player;

		// Token: 0x040000AD RID: 173
		private Gun gun;

		// Token: 0x040000AE RID: 174
		public bool able;

		// Token: 0x040000AF RID: 175
		private readonly float updateDelay = 0.1f;

		// Token: 0x040000B0 RID: 176
		private readonly float effectCooldown = 1f;

		// Token: 0x040000B1 RID: 177
		private float timeOfLastEffect;

		// Token: 0x040000B2 RID: 178
		private float startTime;

		// Token: 0x040000B3 RID: 179
		public int numcheck;
	}
}
