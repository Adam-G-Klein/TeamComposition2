using System;
using System.Collections;
using UnityEngine;

public class ParticleExplosionModifier : MonoBehaviour
{
	public AnimationCurve curve;

	public float speed = 1f;

	private ParticleSystem effect;

	private ParticleSystem.MainModule main;

	private Coroutine corutine;

	private void Start()
	{
		if (curve == null)
		{
			Debug.LogWarning("ParticleExplosionModifier missing curve, disabling.");
			base.enabled = false;
			return;
		}
		effect = GetComponent<ParticleSystem>();
		if (effect == null)
		{
			Debug.LogWarning("ParticleExplosionModifier missing ParticleSystem, disabling.");
			base.enabled = false;
			return;
		}
		main = effect.main;
		Explosion componentInParent = GetComponentInParent<Explosion>();
		if (componentInParent == null)
		{
			Debug.LogWarning("ParticleExplosionModifier missing Explosion parent, disabling.");
			base.enabled = false;
			return;
		}
		componentInParent.DealDamageAction = (Action<Damagable>)Delegate.Combine(componentInParent.DealDamageAction, new Action<Damagable>(DealDamage));
		componentInParent.DealHealAction = (Action<Damagable>)Delegate.Combine(componentInParent.DealHealAction, new Action<Damagable>(DealDamage));
	}

	public void DealDamage(Damagable damagable)
	{
		if (corutine != null)
		{
			StopCoroutine(corutine);
		}
		corutine = StartCoroutine(DoCurve());
	}

	private IEnumerator DoCurve()
	{
		float c = 0f;
		float t = curve.keys[curve.keys.Length - 1].time;
		while (c < t)
		{
			ParticleSystem.MinMaxCurve startSize = main.startSize;
			startSize.constantMin = curve.Evaluate(c) * 0.5f;
			startSize.constantMax = curve.Evaluate(c);
			main.startSize = startSize;
			c += TimeHandler.deltaTime * speed;
			yield return null;
		}
	}
}
