using System;
using System.Linq;
using Photon.Pun;
using Sonigon;
using SoundImplementation;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000046 RID: 70
	public class BarrierObject : MonoBehaviour
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int mask = LayerMask.GetMask(new string[]
				{
					"Projectile"
				});
				foreach (Collider2D collider2D in from uwu in Physics2D.OverlapCircleAll(base.transform.position, 4.7f, mask)
				where uwu.gameObject.GetComponentInParent<ProjectileHit>() != null && uwu.gameObject.GetComponentInParent<ProjectileHit>().ownPlayer != this.player && PlayerManager.instance.CanSeePlayer(uwu.gameObject.transform.position, this.player).canSee
				select uwu)
				{
					this.ResetTimer();
					SoundManager.Instance.PlayAtPosition(this.soundShoot, SoundManager.Instance.GetTransform(), base.transform);
					Object.Instantiate<GameObject>(BarrierMono.barrierBlock, base.gameObject.transform.position, Quaternion.identity);
					collider2D.gameObject.GetComponentInParent<ProjectileHit>().GetComponent<MoveTransform>().velocity *= -1f;
					if (collider2D.GetComponent<ProjectileCollision>() != null)
					{
						collider2D.GetComponent<ProjectileCollision>().Die();
					}
					PhotonView componentInParent = collider2D.GetComponentInParent<PhotonView>();
					collider2D.GetComponentInParent<SpawnedAttack>().gameObject.SetActive(false);
					if (componentInParent.IsMine)
					{
						PhotonNetwork.Destroy(componentInParent);
					}
				}
				this.counter += 0.05f;
				this.ResetTimer();
				if (this.counter >= 0.9f)
				{
					Object.Instantiate<GameObject>(BarrierMono.barrierVisual, base.gameObject.transform.position, Quaternion.identity);
				}
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000DB58 File Offset: 0x0000BD58
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0400020B RID: 523
		public Player player;

		// Token: 0x0400020C RID: 524
		private readonly float updateDelay = 0.05f;

		// Token: 0x0400020D RID: 525
		private float startTime;

		// Token: 0x0400020E RID: 526
		private float counter;

		// Token: 0x0400020F RID: 527
		public static GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;

		// Token: 0x04000210 RID: 528
		private SoundEvent soundShoot = BarrierObject.addObjectToPlayer.GetComponent<Empower>().addObjectToBullet.GetComponent<SoundUnityEventPlayer>().soundStart;
	}
}
