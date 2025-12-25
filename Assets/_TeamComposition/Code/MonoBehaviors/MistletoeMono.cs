using System;
using System.Linq;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;
using Object = UnityEngine.Object;
using TeamComposition2;

public class MistletoeMono : RayHitEffect
{
    private void Start()
    {
        this.proj = base.GetComponentInParent<ProjectileHit>();
        // Don't access ownPlayer here - it might not be set yet
        // We'll lazy-load it in DoHitEffect when we actually need it
        // this.me = this.proj.ownPlayer;
    }

    public override HasToReturn DoHitEffect(HitInfo hit)
    {
        UnityEngine.Debug.Log("[MistletoeMono] DoHitEffect called"); // LOG-REMOVE-LATER

        // Lazy-load ownPlayer here if we don't have it yet
        if (this.me == null && this.proj != null)
        {
            this.me = this.proj.ownPlayer;
            UnityEngine.Debug.Log("[MistletoeMono] Lazy-loaded 'me' from proj.ownPlayer: " + (this.me != null ? this.me.ToString() : "null")); // LOG-REMOVE-LATER
        }

        // Add null checks
        if (this.proj == null)
        {
            UnityEngine.Debug.LogError("[MistletoeMono] proj is null"); // LOG-REMOVE-LATER
            return HasToReturn.hasToReturn;
        }
        if (this.me == null)
        {
            UnityEngine.Debug.LogError("[MistletoeMono] me is null"); // LOG-REMOVE-LATER
            return HasToReturn.hasToReturn;
        }

        if (!hit.transform)
        {
            UnityEngine.Debug.LogWarning("[MistletoeMono] hit.transform is null"); // LOG-REMOVE-LATER
            return HasToReturn.hasToReturn;
        }

        this.comp = hit.transform.GetComponent<Player>();
        UnityEngine.Debug.Log("[MistletoeMono] Player component found: " + (this.comp != null)); // LOG-REMOVE-LATER

        bool isNotSelf = (this.comp != this.me);
        UnityEngine.Debug.Log("[MistletoeMono] isNotSelf: " + isNotSelf); // LOG-REMOVE-LATER

        bool isActiveAndEnabled = (this.comp != null && this.comp.isActiveAndEnabled);
        UnityEngine.Debug.Log("[MistletoeMono] isActiveAndEnabled: " + isActiveAndEnabled); // LOG-REMOVE-LATER

        bool isNotDead = (this.comp != null && !this.comp.data.dead);
        UnityEngine.Debug.Log("[MistletoeMono] isNotDead: " + isNotDead); // LOG-REMOVE-LATER

        bool notFrozenYet = (this.comp != null && this.comp.gameObject.GetComponent<FrozenMono>() == null);
        UnityEngine.Debug.Log("[MistletoeMono] notFrozenYet: " + notFrozenYet); // LOG-REMOVE-LATER

        if (
            this.comp != null
            && isNotSelf
            && isActiveAndEnabled
            && isNotDead
            && notFrozenYet
        )
        {
            UnityEngine.Debug.Log("[MistletoeMono] All boolean checks passed, proceeding with effect"); // LOG-REMOVE-LATER
            this.activated = true;
            this.ResetEffectTimer();
            FrozenMono frozenMono = this.comp.gameObject.AddComponent<FrozenMono>();
            UnityEngine.Debug.Log("[MistletoeMono] Added FrozenMono to target"); // LOG-REMOVE-LATER

            // Sound setup (you may need to add audio assets to your bundle)
            if (!MistletoeMono.fieldsound && MyPlugin.asset != null)
            {
                UnityEngine.Debug.Log("[MistletoeMono] Creating new fieldsound from asset bundle"); // LOG-REMOVE-LATER
                AudioClip audioClip = MyPlugin.asset.LoadAsset<AudioClip>("ice");
                if (audioClip != null)
                {
                    SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
                    soundContainer.audioClip[0] = audioClip;
                    soundContainer.setting.volumeIntensityEnable = true;
                    MistletoeMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
                    MistletoeMono.fieldsound.soundContainerArray[0] = soundContainer;
                    UnityEngine.Debug.Log("[MistletoeMono] SoundEvent and SoundContainer set up"); // LOG-REMOVE-LATER
                }
                else
                {
                    UnityEngine.Debug.LogWarning("[MistletoeMono] Could not load ice AudioClip from asset bundle"); // LOG-REMOVE-LATER
                }
            }

            if (MistletoeMono.fieldsound != null)
            {
                UnityEngine.Debug.Log("[MistletoeMono] SoundEvent found, playing icy sound"); // LOG-REMOVE-LATER
                this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1f;
                SoundManager.Instance.Play(MistletoeMono.fieldsound, base.transform, new SoundParameterBase[]
                {
                    this.soundParameterIntensity
                });
            }
            else
            {
                UnityEngine.Debug.LogWarning("[MistletoeMono] fieldsound is null, cannot play sound"); // LOG-REMOVE-LATER
            }

            // Visual effects (you'll need to add these assets to your bundle)
            if (MyPlugin.asset != null)
            {
                UnityEngine.Debug.Log("[MistletoeMono] MyPlugin.asset is not null, trying to create ice/snow effects"); // LOG-REMOVE-LATER
                GameObject snowflakePrefab = MyPlugin.asset.LoadAsset<GameObject>("snowflake");
                if (snowflakePrefab != null)
                {
                    this.ice = Object.Instantiate<GameObject>(snowflakePrefab, hit.transform);
                    this.ice.transform.right = hit.transform.right;
                    this.ice.transform.localScale *= 1.9f;
                    this.ice.AddComponent<RemoveAfterSeconds>().seconds = 4.2f;
                    SpriteRenderer sr = this.ice.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        // Tone down glow and lower alpha to make the snowflake less distracting
                        Color muted = new Color(0.7f, 0.85f, 1f, 0.55f);
                        sr.color = muted;
                        if (sr.material != null && sr.material.HasProperty("_EmissionColor"))
                        {
                            sr.material.SetColor("_EmissionColor", muted * 0.35f);
                        }
                    }
                    if (this.ice.GetComponent<Animator>() != null)
                    {
                        this.ice.GetComponent<Animator>().recorderStartTime = 0f;
                        this.ice.GetComponent<Animator>().recorderStopTime = 4.2f;
                    }
                    UnityEngine.Debug.Log("[MistletoeMono] Instantiated and configured ice visual"); // LOG-REMOVE-LATER
                }
                else
                {
                    UnityEngine.Debug.LogWarning("[MistletoeMono] Could not load snowflake prefab from asset bundle"); // LOG-REMOVE-LATER
                }

                GameObject snowparticlesPrefab = MyPlugin.asset.LoadAsset<GameObject>("snowparticles");
                if (snowparticlesPrefab != null)
                {
                    this.snow = Object.Instantiate<GameObject>(snowparticlesPrefab, hit.transform);
                    this.snow.transform.right = hit.transform.right;
                    this.snow.transform.localScale *= 1.5f;
                    this.snow.AddComponent<RemoveAfterSeconds>().seconds = 4.2f;
                    if (this.snow.GetComponent<ParticleSystem>() != null)
                    {
                        ParticleSystem.MainModule main = this.snow.GetComponent<ParticleSystem>().main;
                        main.startColor = new Color(0f, 1f, 1f, 1f);
                    }
                    UnityEngine.Debug.Log("[MistletoeMono] Instantiated and configured snow particles"); // LOG-REMOVE-LATER
                }
                else
                {
                    UnityEngine.Debug.LogWarning("[MistletoeMono] Could not load snowparticles prefab from asset bundle"); // LOG-REMOVE-LATER
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning("[MistletoeMono] MyPlugin.asset is null, can't spawn visuals"); // LOG-REMOVE-LATER
            }

            this.gameObject = new GameObject();
            this.gameObject.transform.position = hit.transform.position;
            this.gameObject.transform.parent = hit.transform;
            this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 4.2f;
            this.iceRing = this.gameObject.AddComponent<IceRing>();
            this.iceRing.player = this.me;
            this.iceRing.hit = this.comp;
            this.iceRing.ice = this.ice;
            UnityEngine.Debug.Log("[MistletoeMono] Created IceRing object and set references"); // LOG-REMOVE-LATER

            // Line effect setup (requires ChillingPresence card or alternative)
            if (MistletoeMono.lineEffect == null)
            {
                UnityEngine.Debug.Log("[MistletoeMono] lineEffect is null, trying to find line effect"); // LOG-REMOVE-LATER
                this.FindLineEffect();
            }
            if (MistletoeMono.lineEffect != null)
            {
                UnityEngine.Debug.Log("[MistletoeMono] Found line effect, instantiating"); // LOG-REMOVE-LATER
                GameObject lineEffectObj = Object.Instantiate<GameObject>(MistletoeMono.lineEffect, this.gameObject.transform);
                LineEffect componentInChildren = lineEffectObj.GetComponentInChildren<LineEffect>();
                if (componentInChildren != null)
                {
                    UnityEngine.Debug.Log("[MistletoeMono] Found LineEffect component in new line effect object"); // LOG-REMOVE-LATER
                    Gradient gradient = new Gradient();
                    gradient.alphaKeys = new GradientAlphaKey[]
                    {
                        new GradientAlphaKey(1f, 0f)
                    };
                    gradient.colorKeys = new GradientColorKey[]
                    {
                        new GradientColorKey(new Color(0.1f, 0.9f, 1f, 1f), 0f)
                    };
                    gradient.mode = (GradientMode)1; // Blend mode
                    componentInChildren.colorOverTime = gradient;
                    componentInChildren.widthMultiplier = 2f;
                    componentInChildren.radius = 8.25f;
                    componentInChildren.raycastCollision = false;
                    componentInChildren.useColorOverTime = true;
                    ParticleSystem particleSystem = lineEffectObj.AddComponent<ParticleSystem>();
                    ParticleSystem.MainModule main = particleSystem.main;
                    main.duration = 4.2f;
                    main.startSpeed = 10f;
                    main.startLifetime = 0.5f;
                    main.startSize = 0.1f;
                    ParticleSystem.EmissionModule emission = particleSystem.emission;
                    emission.enabled = true;
                    emission.rateOverTime = 150f;
                    ParticleSystem.ShapeModule shape = particleSystem.shape;
                    shape.enabled = true;
                    shape.shapeType = ParticleSystemShapeType.Circle;
                    shape.radius = 0.5f;
                    shape.radiusThickness = 1f;
                    if (lineEffectObj.GetComponentInChildren<ParticleSystemRenderer>() != null)
                    {
                        lineEffectObj.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(0.1f, 0.9f, 1f, 1f);
                    }
                    foreach (ParticleSystem particleSystem2 in lineEffectObj.GetComponentsInChildren<ParticleSystem>())
                    {
                        ParticleSystem.MainModule main2 = particleSystem2.main;
                        main2.startColor = new Color(0.1f, 0.9f, 1f, 1f);
                    }
                    this.iceRing.gameObject2 = lineEffectObj;
                    UnityEngine.Debug.Log("[MistletoeMono] Finished configuring line effect and assigned to iceRing"); // LOG-REMOVE-LATER
                }
                else
                {
                    UnityEngine.Debug.LogWarning("[MistletoeMono] Could not find LineEffect component in lineEffect prefab"); // LOG-REMOVE-LATER
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning("[MistletoeMono] lineEffect still null after trying to find line effect"); // LOG-REMOVE-LATER
            }

            this.iceRing.gameObject = this.gameObject;
            frozenMono.gameObject2 = this.iceRing.gameObject2;
            frozenMono.gameObject = this.gameObject;
            frozenMono.ice = this.ice;
            frozenMono.snow = this.snow;
            frozenMono.iceRing = this.iceRing;
            UnityEngine.Debug.Log("[MistletoeMono] Set references on FrozenMono"); // LOG-REMOVE-LATER
        }
        else
        {
            UnityEngine.Debug.Log("[MistletoeMono] Boolean check failed. Details: " + // LOG-REMOVE-LATER
                "comp=" + (this.comp != null ? "not null" : "null") +
                ", isNotSelf=" + (this.comp != null ? (this.comp != this.me).ToString() : "n/a") +
                ", isActiveAndEnabled=" + (this.comp != null ? this.comp.isActiveAndEnabled.ToString() : "n/a") +
                ", isNotDead=" + (this.comp != null ? (!this.comp.data.dead).ToString() : "n/a") +
                ", notFrozenYet=" + (this.comp != null ? (this.comp.gameObject.GetComponent<FrozenMono>() == null).ToString() : "n/a")
            );
        }
        return HasToReturn.canContinue;
    }

    public void Update()
    {
        if (Time.time >= this.startTime + this.updateDelay)
        {
            this.ResetTimer();
        }
        if (this.comp && (this.comp.data.dead || this.comp.data.health <= 0f || !this.comp.gameObject.activeInHierarchy))
        {
            UnityEngine.Debug.Log("[MistletoeMono] Player is dead or inactive, destroying MistletoeMono"); // LOG-REMOVE-LATER
            if (this.ice != null) Destroy(this.ice);
            if (this.iceRing != null) Destroy(this.iceRing);
            if (this.gameObject != null) Destroy(this.gameObject);
            if (this.gameObject2 != null) Destroy(this.gameObject2);
            this.Destroy();
        }
    }

    private void FindLineEffect()
    {
        // Try to find ChillingPresence card's line effect, or use alternative
        if (CardChoice.instance != null && CardChoice.instance.cards != null)
        {
            CardInfo chillingPresence = CardChoice.instance.cards.FirstOrDefault(c => c.name.Equals("ChillingPresence"));
            if (chillingPresence != null && chillingPresence.gameObject != null)
            {
                CharacterStatModifiers statMods = chillingPresence.gameObject.GetComponentInChildren<CharacterStatModifiers>();
                if (statMods != null && statMods.AddObjectToPlayer != null)
                {
                    LineEffect lineEffect = statMods.AddObjectToPlayer.GetComponentInChildren<LineEffect>();
                    if (lineEffect != null)
                    {
                        MistletoeMono.lineEffect = lineEffect.gameObject;
                    }
                }
            }
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

    public void Destroy()
    {
        UnityEngine.Debug.Log("[MistletoeMono] Destroying MistletoeMono"); // LOG-REMOVE-LATER
        Destroy(this.gameObject);
    }

    private readonly float updateDelay = 0.1f;
    private readonly float effectCooldown = 5f;
    private float startTime;
    private float timeOfLastEffect;
    public bool activated;
    public float time;
    public GameObject ice;
    public GameObject snow;
    public IceRing iceRing;
    public GameObject gameObject;
    public GameObject gameObject2;
    public ProjectileHit proj;
    private static GameObject lineEffect;
    private Player comp;
    private Player me;
    private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);
    public static SoundEvent fieldsound;
}


