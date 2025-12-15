using System;
using System.Collections.Generic;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200001B RID: 27
	public class MitosisBlockMono : ReversibleEffect
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00005B66 File Offset: 0x00003D66
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005B84 File Offset: 0x00003D84
		public override void OnStart()
		{
			this.gunStatModifier.numberOfProjectiles_add++;
			this.gunStatModifier.spread_add += 0.05f;
			this.gunStatModifier.damage_mult *= 1.25f;
			this.gunStatModifier.projectielSimulatonSpeed_mult *= 1.75f;
			this.gunStatModifier.projectileColor = new Color(0f, 1f, 0f, 1f);
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.effect = this.player.GetComponent<MitosisMono>();
			List<ObjectsToSpawn> objectsToSpawn_add = this.gunStatModifier.objectsToSpawn_add;
			GameObject gameObject = Object.Instantiate<GameObject>(((GameObject)Resources.Load("0 cards/Demonic pact")).GetComponent<Gun>().objectsToSpawn[0].effect);
			gameObject.transform.position = new Vector3(1000f, 0f, 0f);
			gameObject.hideFlags = 61;
			gameObject.name = "Explosion";
			gameObject.GetComponent<RemoveAfterSeconds>().seconds = 2f;
			gameObject.GetComponent<Explosion>().force = 400f;
			gameObject.GetComponent<Explosion>().damage = 5f;
			gameObject.GetComponent<Explosion>().range = 4f;
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].startColor = new Color(0.1f, 1f, 0f, 1f);
			}
			foreach (ParticleSystemRenderer particleSystemRenderer in gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
			{
				particleSystemRenderer.material.color = new Color(0.2f, 1f, 0.2f, 1f);
				particleSystemRenderer.sharedMaterial.color = new Color(0.2f, 1f, 0.2f, 1f);
			}
			Material[] componentsInChildren3 = gameObject.GetComponentsInChildren<Material>();
			for (int i = 0; i < componentsInChildren3.Length; i++)
			{
				componentsInChildren3[i].color = new Color(0.2f, 1f, 0.2f, 1f);
			}
			objectsToSpawn_add.Add(new ObjectsToSpawn
			{
				effect = gameObject,
				normalOffset = 0f,
				numberOfSpawns = 1,
				scaleFromDamage = 1f
			});
			this.gunStatModifier.objectsToSpawn_add = objectsToSpawn_add;
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005E08 File Offset: 0x00004008
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (!this.effect.Failsafe || !this.effect.Active)
				{
					base.Destroy();
					this.colorEffect.Destroy();
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
				{
					this.ResetTimer();
					base.Destroy();
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005EA1 File Offset: 0x000040A1
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005EBC File Offset: 0x000040BC
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.effect.Active = false;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005EE3 File Offset: 0x000040E3
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040000A4 RID: 164
		private readonly Color color = Color.green;

		// Token: 0x040000A5 RID: 165
		private ReversibleColorEffect colorEffect;

		// Token: 0x040000A6 RID: 166
		public MitosisMono effect;

		// Token: 0x040000A7 RID: 167
		public GameObject epo;

		// Token: 0x040000A8 RID: 168
		private readonly float updateDelay = 0.1f;

		// Token: 0x040000A9 RID: 169
		private float startTime;
	}
}
