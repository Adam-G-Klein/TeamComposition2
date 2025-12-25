using System;
using Sonigon;
using Sonigon.Internal;
using TeamComposition2.Stats;
using UnityEngine;
using Object = UnityEngine.Object;
using TeamComposition2;

// the thing that spawns if the hit occurs
public class IceRing : MonoBehaviour
{
    public void Update()
    {
        if (this.hit != null && (this.hit.data.dead || this.hit.data.health <= 0f || !this.hit.gameObject.activeInHierarchy))
        {
            this.Destroy();
            return;
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
                        float baseHeal = 1f + component.maxHealth * 0.1f;
                        float healMultiplier = this.player != null ? this.player.GetHealingDealtMultiplier() : 1f;
                        array[i].gameObject.GetComponent<HealthHandler>().Heal(baseHeal * healMultiplier);
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
                // Try to use AquaRingMono visual if available, otherwise skip
                try
                {
                    Type aquaRingType = Type.GetType("CR.MonoBehaviors.AquaRingMono, CosmicRounds");
                    if (aquaRingType != null)
                    {
                        var aquaVisualField = aquaRingType.GetField("aquaVisual", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                        if (aquaVisualField != null)
                        {
                            GameObject aquaVisual = aquaVisualField.GetValue(null) as GameObject;
                            if (aquaVisual != null)
                            {
                                Object.Instantiate<GameObject>(aquaVisual, this.gameObject.transform.position, Quaternion.identity);
                            }
                        }
                    }
                }
                catch { }
                
                if (!IceRing.fieldsound2 && MyPlugin.asset != null)
                {
                    AudioClip audioClip = MyPlugin.asset.LoadAsset<AudioClip>("pulsar");
                    if (audioClip != null)
                    {
                        SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
                        soundContainer.audioClip[0] = audioClip;
                        soundContainer.setting.volumeIntensityEnable = true;
                        IceRing.fieldsound2 = ScriptableObject.CreateInstance<SoundEvent>();
                        IceRing.fieldsound2.soundContainerArray[0] = soundContainer;
                    }
                }
                if (IceRing.fieldsound2 != null)
                {
                    this.soundParameterIntensity2.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f;
                    SoundManager.Instance.Play(IceRing.fieldsound2, base.transform, new SoundParameterBase[]
                    {
                        this.soundParameterIntensity2
                    });
                    SoundManager.Instance.Stop(IceRing.fieldsound2, base.transform, true);
                }
            }
        }
    }

    public void Destroy()
    {
        if (this.gameObject != null) Object.Destroy(this.gameObject);
        if (this.gameObject2 != null) Object.Destroy(this.gameObject2);
        if (this.ice != null) Object.Destroy(this.ice);
        Object.Destroy(this);
    }

    private void ResetTimer()
    {
        this.startTime = Time.time;
    }

    private readonly float updateDelay = 0.2f;
    private float startTime;
    private float counter;
    public Player player;
    public Player hit;
    public GameObject ice;
    public GameObject gameObject;
    public GameObject gameObject2;
    public static SoundEvent fieldsound;
    private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);
    public static SoundEvent fieldsound2;
    private SoundParameterIntensity soundParameterIntensity2 = new SoundParameterIntensity(0.6f, 0);
}

