using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200006C RID: 108
	public class IceRing : MonoBehaviour
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00014CF8 File Offset: 0x00012EF8
		public void Update()
		{
			if (this.hit.data.dead || this.hit.data.health <= 0f || !this.hit.gameObject.activeInHierarchy)
			{
				this.Destroy();
			}
			if (Time.time >= this.startTime + this.updateDelay)
			{
				Vector2 vector = this.gameObject.transform.position;
				Player[] array = PlayerManager.instance.players.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (Vector2.Distance(vector, array[i].transform.position) <= 9f)
					{
						bool flag = array[i].playerID == this.player.playerID;
						bool flag2 = array[i].teamID == this.player.teamID;
						if (flag || flag2)
						{
							CharacterData component = array[i].gameObject.GetComponent<CharacterData>();
							array[i].gameObject.GetComponent<HealthHandler>().Heal(1f + component.maxHealth * 0.1f);
						}
						else
						{
							Damagable component2 = array[i].gameObject.GetComponent<HealthHandler>();
							CharacterData component3 = array[i].gameObject.GetComponent<CharacterData>();
							component3.stats.AddSlowAddative(0.1f, 1f, false);
							component2.TakeDamage((1f + component3.maxHealth * 0.01f) * Vector2.down, array[i].transform.position, this.player.data.weaponHandler.gameObject, this.player, true, true);
						}
					}
				}
				this.ResetTimer();
				this.counter += 0.1f;
				if (this.counter >= 1.7f)
				{
					Object.Instantiate<GameObject>(AquaRingMono.aquaVisual, this.gameObject.transform.position, Quaternion.identity);
					if (!IceRing.fieldsound2)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("pulsar");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						IceRing.fieldsound2 = ScriptableObject.CreateInstance<SoundEvent>();
						IceRing.fieldsound2.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity2.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(IceRing.fieldsound2, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity2
					});
					SoundManager.Instance.Stop(IceRing.fieldsound2, base.transform, true);
				}
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00014FB8 File Offset: 0x000131B8
		public void Destroy()
		{
			Object.Destroy(this.gameObject);
			Object.Destroy(this.gameObject2);
			Object.Destroy(this.ice);
			Object.Destroy(this);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00014FE1 File Offset: 0x000131E1
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000345 RID: 837
		private readonly float updateDelay = 0.2f;

		// Token: 0x04000346 RID: 838
		private float startTime;

		// Token: 0x04000347 RID: 839
		private float counter;

		// Token: 0x04000348 RID: 840
		public Player player;

		// Token: 0x04000349 RID: 841
		public Player hit;

		// Token: 0x0400034A RID: 842
		public GameObject ice;

		// Token: 0x0400034B RID: 843
		public GameObject gameObject;

		// Token: 0x0400034C RID: 844
		public GameObject gameObject2;

		// Token: 0x0400034D RID: 845
		public static SoundEvent fieldsound;

		// Token: 0x0400034E RID: 846
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);

		// Token: 0x0400034F RID: 847
		public static SoundEvent fieldsound2;

		// Token: 0x04000350 RID: 848
		private SoundParameterIntensity soundParameterIntensity2 = new SoundParameterIntensity(0.6f, 0);
	}
}
