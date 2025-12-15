using System;
using Photon.Pun;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000020 RID: 32
	public class PulseMono : MonoBehaviour
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000065A1 File Offset: 0x000047A1
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.state = true;
			this.ResetTimer();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000065D4 File Offset: 0x000047D4
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000065DC File Offset: 0x000047DC
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000065EC File Offset: 0x000047EC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				ProjectileHit componentInParent = base.GetComponentInParent<ProjectileHit>();
				Player ownPlayer = componentInParent.ownPlayer;
				if (this.state)
				{
					MoveTransform moveTransform = this.move;
					moveTransform.velocity.x = moveTransform.velocity.x * 1.5f;
					MoveTransform moveTransform2 = this.move;
					moveTransform2.velocity.y = moveTransform2.velocity.y * 1.5f;
					this.move.velocity.z = 0f;
					this.state = false;
					if (this.poppers > 0)
					{
						this.poppers--;
						Vector2 vector = componentInParent.gameObject.transform.position;
						Object.Instantiate<GameObject>(GlueMono.glueVisual, vector, Quaternion.identity);
						Player[] array = PlayerManager.instance.players.ToArray();
						for (int i = 0; i < array.Length; i++)
						{
							if (Vector2.Distance(vector, array[i].transform.position) < 7f && array[i].teamID != ownPlayer.teamID)
							{
								array[i].gameObject.GetComponent<CharacterData>();
								Player component = array[i].gameObject.GetComponent<Player>();
								array[i].gameObject.GetComponent<HealthHandler>();
								component.data.healthHandler.DoDamage(componentInParent.damage * 0.5f * Vector2.down, component.transform.position, new Color(1f, 1f, 1f, 1f), ownPlayer.data.weaponHandler.gameObject, ownPlayer, false, true, false);
							}
						}
						return;
					}
				}
				else
				{
					MoveTransform moveTransform3 = this.move;
					moveTransform3.velocity.x = moveTransform3.velocity.x * 0.5f;
					MoveTransform moveTransform4 = this.move;
					moveTransform4.velocity.y = moveTransform4.velocity.y * 0.5f;
					this.move.velocity.z = 0f;
					this.state = true;
				}
			}
		}

		// Token: 0x040000C6 RID: 198
		private SyncProjectile sync;

		// Token: 0x040000C7 RID: 199
		private MoveTransform move;

		// Token: 0x040000C8 RID: 200
		private readonly float updateDelay = 0.4f;

		// Token: 0x040000C9 RID: 201
		private float startTime;

		// Token: 0x040000CA RID: 202
		public bool state;

		// Token: 0x040000CB RID: 203
		public int poppers = 2;
	}
}
