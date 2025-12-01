using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;
using Object = UnityEngine.Object;

public class FrozenMono : ReversibleEffect
{
    public override void OnOnEnable()
    {
        if (this.colorEffect != null)
        {
            this.colorEffect.Destroy();
        }
    }

    public override void OnStart()
    {
        this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
        this.colorEffect.SetColor(this.color);
        this.colorEffect.SetLivesToEffect(1);
        this.ResetTimer();
        this.ResetEffectTimer();
    }

    // probably for turning off the effect after a certain time
    public override void OnUpdate()
    {
        if (Time.time >= this.startTime + this.updateDelay)
        {
            this.ResetTimer();
            if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
            {
                this.ResetTimer();
                base.Destroy();
            }
            if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
            {
                base.Destroy();
                if (this.colorEffect != null)
                {
                    this.colorEffect.Destroy();
                }
            }
        }
    }

    public override void OnOnDisable()
    {
        if (this.colorEffect != null)
        {
            this.colorEffect.Destroy();
            this.ResetEffectTimer();
            if (this.ice != null) Object.Destroy(this.ice);
            if (this.snow != null) Object.Destroy(this.snow);
            if (this.iceRing != null) Object.Destroy(this.iceRing);
            if (this.gameObject != null) Object.Destroy(this.gameObject);
            if (this.gameObject2 != null) Object.Destroy(this.gameObject2);
        }
    }

    public override void OnOnDestroy()
    {
        if (this.colorEffect != null)
        {
            this.colorEffect.Destroy();
            this.ResetEffectTimer();
            if (this.ice != null) Object.Destroy(this.ice);
            if (this.snow != null) Object.Destroy(this.snow);
            if (this.iceRing != null) Object.Destroy(this.iceRing);
            if (this.gameObject != null) Object.Destroy(this.gameObject);
            if (this.gameObject2 != null) Object.Destroy(this.gameObject2);
        }
    }

    private void ResetTimer()
    {
        this.startTime = Time.time;
    }

    private void ResetEffectTimer()
    {
        this.timeOfLastEffect = Time.time;
    }

    private readonly Color color = Color.cyan;
    private ReversibleColorEffect colorEffect;
    public MistletoeMono effect;
    public GameObject ice;
    public GameObject snow;
    public IceRing iceRing;
    public GameObject gameObject;
    public GameObject gameObject2;
    private readonly float effectCooldown = 4.2f;
    private readonly float updateDelay = 0.1f;
    private float timeOfLastEffect;
    private float startTime;
}

