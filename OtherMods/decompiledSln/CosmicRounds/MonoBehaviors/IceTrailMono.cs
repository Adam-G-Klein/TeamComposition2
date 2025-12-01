using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000056 RID: 86
	public class IceTrailMono : MonoBehaviour
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00011730 File Offset: 0x0000F930
		private void Start()
		{
			if (!IceTrailMono.fieldsound)
			{
				AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("ice");
				SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
				soundContainer.audioClip[0] = audioClip;
				soundContainer.setting.volumeIntensityEnable = true;
				IceTrailMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
				IceTrailMono.fieldsound.soundContainerArray[0] = soundContainer;
			}
			this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
			SoundManager.Instance.Play(IceTrailMono.fieldsound, base.transform, new SoundParameterBase[]
			{
				this.soundParameterIntensity
			});
			this.trail = new GameObject("Trail", new Type[]
			{
				typeof(TrailRenderer)
			});
			this.trail.transform.SetPositionAndRotation(base.transform.position, base.transform.rotation);
			this.trail.GetComponent<TrailRenderer>().startWidth = 0.1f;
			this.trail.GetComponent<TrailRenderer>().endWidth = 0.01f;
			this.trail.GetComponent<TrailRenderer>().time = 0.1f;
			this.trail.GetComponent<TrailRenderer>().startColor = Color.cyan;
			this.trail.GetComponent<TrailRenderer>().endColor = Color.blue;
			this.trail.GetComponent<TrailRenderer>().sharedMaterial = CR.ArtAsset.LoadAsset<Material>("IcyMaterial");
			this.trail.AddComponent<RemoveAfterSeconds>().seconds = 0.5f;
			if (base.GetComponentInParent<ProjectileHit>() != null)
			{
				this.color = base.GetComponentInParent<ProjectileHit>().projectileColor;
			}
			if (this.color == Color.black || this.color == Color.clear)
			{
				this.trail.GetComponent<TrailRenderer>().sharedMaterial.color = Color.cyan;
				return;
			}
			this.trail.GetComponent<TrailRenderer>().sharedMaterial.color = this.color;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00011946 File Offset: 0x0000FB46
		private void Destroy()
		{
			Object.Destroy(this.trail);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00011953 File Offset: 0x0000FB53
		private void Update()
		{
			this.trail.transform.SetPositionAndRotation(base.transform.position, base.transform.rotation);
		}

		// Token: 0x0400029A RID: 666
		private GameObject trail;

		// Token: 0x0400029B RID: 667
		private GameObject bullet;

		// Token: 0x0400029C RID: 668
		public static SoundEvent fieldsound;

		// Token: 0x0400029D RID: 669
		public Color color;

		// Token: 0x0400029E RID: 670
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.3f, 0);
	}
}
