using System;
using System.Linq;
using Photon.Pun;
using Sonigon;
using SoundImplementation;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000048 RID: 72
	public class HeartitionObject : MonoBehaviour
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
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
					Object.Instantiate<GameObject>(HeartitionMono.heartitionBlock, base.gameObject.transform.position, Quaternion.identity);
					collider2D.gameObject.GetComponentInParent<ProjectileHit>().GetComponent<MoveTransform>().velocity *= -1f;
					if (collider2D.GetComponent<ProjectileCollision>() != null)
					{
						collider2D.GetComponent<ProjectileCollision>().Die();
					}
					this.player.data.healthHandler.Heal(1f + this.player.data.maxHealth * 0.25f);
					PhotonView componentInParent = collider2D.GetComponentInParent<PhotonView>();
					collider2D.GetComponentInParent<SpawnedAttack>().gameObject.SetActive(false);
					if (componentInParent.IsMine)
					{
						PhotonNetwork.Destroy(componentInParent);
					}
				}
				this.counter += 0.05f;
				this.ResetTimer();
				if (this.counter >= 1f)
				{
					Object.Instantiate<GameObject>(HeartitionMono.heartitionVisu, base.gameObject.transform.position, Quaternion.identity);
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000E4B8 File Offset: 0x0000C6B8
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000220 RID: 544
		public Player player;

		// Token: 0x04000221 RID: 545
		private readonly float updateDelay = 0.05f;

		// Token: 0x04000222 RID: 546
		private float startTime;

		// Token: 0x04000223 RID: 547
		private float counter;

		// Token: 0x04000224 RID: 548
		public static GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;

		// Token: 0x04000225 RID: 549
		private SoundEvent soundShoot = HeartitionObject.addObjectToPlayer.GetComponent<Empower>().addObjectToBullet.GetComponent<SoundUnityEventPlayer>().soundStart;
	}
}
