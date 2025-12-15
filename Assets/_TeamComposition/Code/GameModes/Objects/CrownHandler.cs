using MapEmbiggener;
using Photon.Pun;
using System.Collections;
using System.Linq;
using TeamComposition2.GameModes;
using TeamComposition2.GameModes.Physics;
using UnboundLib;
using UnboundLib.GameModes;
using UnityEngine;

namespace TeamComposition2.GameModes.Objects
{
    public static class CrownPrefab
    {
        private static GameObject _crown;

        public static GameObject Crown
        {
            get
            {
                if (_crown == null)
                {
                    GM_ArmsRace gm = GameModeManager.GetGameMode<GM_ArmsRace>(GameModeManager.ArmsRaceID);

                    GameObject crown = GameObject.Instantiate(gm.gameObject.transform.GetChild(0).gameObject);
                    UnityEngine.Debug.Log("[TeamComposition2] Crown prefab instantiated.");
                    GameObject.DontDestroyOnLoad(crown);
                    crown.name = "CrownPrefab";
                    crown.AddComponent<PhotonView>();
                    CrownHandler crownHandler = crown.AddComponent<CrownHandler>();
                    crownHandler.transitionCurve = new AnimationCurve((Keyframe[])crown.GetComponent<GameCrownHandler>().transitionCurve.InvokeMethod("GetKeys"));

                    UnityEngine.Object.DestroyImmediate(crown.GetComponent<GameCrownHandler>());

                    PhotonNetwork.PrefabPool.RegisterPrefab(crown.name, crown);

                    _crown = crown;
                }
                return _crown;
            }
        }
    }

    public class CrownHandler : NetworkPhysicsItem<BoxCollider2D, CircleCollider2D>
    {
        private static CrownHandler instance;

        private const float TriggerRadius = 1.5f;

        private const float MaxFreeTime = 20f;
        private const float MaxRespawns = 20;

        private const float FadeOutTime = 3f;
        private const float FadeInTime = 1f;

        private const float Bounciness = 0.2f;
        private const float Friction = 0.2f;
        private const float Mass = 500f;
        private const float MinAngularDrag = 0.1f;
        private const float MaxAngularDrag = 1f;
        private const float MinDrag = 0f;
        private const float MaxDrag = 5f;
        private const float MaxSpeed = 200f;
        private const float MaxAngularSpeed = 1000f;
        private const float PhysicsForceMult = 10f;
        private const float PhysicsImpulseMult = 0.001f;

        private bool hidden = true;
        private float crownPos;
        public AnimationCurve transitionCurve;
        private int currentCrownHolder = -1;
        private int previousCrownHolder = -1;
        internal SpriteRenderer Renderer => this.gameObject.GetComponentInChildren<SpriteRenderer>();
        public int CrownHolder => this.currentCrownHolder;

        private int respawns = 0;
        private float fadeInTime = 0f;
        private float freeFor = 0f;
        private float heldForInternal = 0f;
        public float HeldFor
        {
            get => heldForInternal;
            private set => heldForInternal = value;
        }

        public override void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            this.gameObject.transform.SetParent(GM_CrownControl.instance.transform);
            GM_CrownControl.instance.SetCrown(this);
            instance = this;
        }
        internal static void DestroyCrown()
        {
            GM_CrownControl.instance.DestroyCrown();
            if (instance != null)
            {
                UnityEngine.Object.DestroyImmediate(instance);
            }
        }
        internal static IEnumerator MakeCrownHandler()
        {
            if (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode)
            {
                PhotonNetwork.Instantiate(
                    CrownPrefab.Crown.name,
                    GM_CrownControl.instance.transform.position,
                    GM_CrownControl.instance.transform.rotation,
                    0
                    );
            }

            yield return new WaitUntil(() => instance != null);
        }
        protected override void Awake()
        {
            this.PhysicalProperties = new ItemPhysicalProperties(
                bounciness: Bounciness,
                friction: Friction,
                mass: Mass,
                minAngularDrag: MinAngularDrag,
                maxAngularDrag: MaxAngularDrag,
                minDrag: MinDrag,
                maxDrag: MaxDrag,
                maxAngularSpeed: MaxAngularSpeed,
                maxSpeed: MaxSpeed,
                forceMult: PhysicsForceMult,
                impulseMult: PhysicsImpulseMult
                );

            base.Awake();
        }
        protected override void Start()
        {
            this.transform.localScale = Vector3.one;
            this.transform.GetChild(0).localScale = new Vector3(0.5f, 0.4f, 1f);

            base.Start();

            this.Trig.radius = TriggerRadius;
            this.Col.size = new Vector2(1f, 0.5f);
            this.Col.edgeRadius = 0.1f;
        }

        public bool TooManyRespawns => this.respawns >= MaxRespawns;

        protected override bool SyncDataNow()
        {
            return !this.hidden;
        }

        public void Reset()
        {
            this.hidden = true;
            this.currentCrownHolder = -1;
            this.previousCrownHolder = -1;
            this.HeldFor = 0f;
            this.freeFor = 0f;
            this.respawns = 0;
        }

        public void Spawn(Vector3 normalized_position)
        {
            if (this.TooManyRespawns)
            {
                this.hidden = true;
                return;
            }
            this.hidden = false;
            this.fadeInTime = 0f;
            this.SetPos(OutOfBoundsUtils.GetPoint(normalized_position));
            this.SetVel(Vector2.zero);
            this.SetRot(0f);
            this.SetAngularVel(0f);
        }

        public override void SetPos(Vector3 position)
        {
            this.GiveCrownToPlayer(-1);
            base.SetPos(position);
        }
        private Vector3 GetFarthestSpawnFromPlayers()
        {
            Vector3[] spawns = MapManager.instance.GetSpawnPoints().Select(s => s.localStartPos).ToArray();
            float dist = -1f;
            Vector3 best = Vector3.zero;
            foreach (Vector3 spawn in spawns)
            {
                float thisDist = PlayerManager.instance.players.Where(p => !p.data.dead).Select(p => Vector3.Distance(p.transform.position, spawn)).Sum();
                if (thisDist > dist)
                {
                    dist = thisDist;
                    best = spawn;
                }
            }
            return best;
        }

        protected internal override void OnCollisionEnter2D(Collision2D collision2D)
        {
            int? playerID = collision2D?.collider?.GetComponent<Player>()?.playerID;
            if (playerID != null)
            {
                this.GiveCrownToPlayer((int)playerID);
            }
            base.OnCollisionEnter2D(collision2D);
        }
        protected internal override void OnTriggerEnter2D(Collider2D collider2D)
        {
            int? playerID = collider2D?.GetComponent<Player>()?.playerID;
            if (playerID != null && this.CanSeePlayer(PlayerManager.instance.players.Find(p => p.playerID == playerID)))
            {
                this.GiveCrownToPlayer((int)playerID);
            }
            base.OnTriggerEnter2D(collider2D);
        }
        protected override void Update()
        {
            if (this.transform.parent == null)
            {
                this.Rig.isKinematic = true;
                this.transform.position = 100000f * Vector2.up;
                this.currentCrownHolder = -1;
                this.previousCrownHolder = -1;
                return;
            }

            base.Update();

            if (this.currentCrownHolder != -1 || this.hidden)
            {
                this.HeldFor += TimeHandler.deltaTime;

                this.Rig.isKinematic = true;
                this.SetRot(0f);
                this.SetAngularVel(0f);
                this.Col.enabled = false;
                this.Trig.enabled = false;
                if (this.hidden) { this.SetPos(100000f * Vector2.up); }
                if (this.Renderer.color.a != 1f)
                {
                    this.Renderer.color = new Color(this.Renderer.color.r, this.Renderer.color.g, this.Renderer.color.b, 1f);
                }
            }
            else
            {
                this.freeFor += TimeHandler.deltaTime;

                this.Rig.isKinematic = false;
                this.Col.enabled = true;
                this.Trig.enabled = true;
                if ((!OutOfBoundsUtils.IsInsideBounds(this.transform.position, out Vector3 normalizedPoint) && (normalizedPoint.y <= 0f)) || this.freeFor > MaxFreeTime)
                {
                    OutOfBoundsUtils.IsInsideBounds(GetFarthestSpawnFromPlayers(), out Vector3 newSpawn);
                    this.Spawn(newSpawn);
                    this.respawns++;
                }
                if (normalizedPoint.x <= 0f)
                {
                    this.Rig.velocity = new Vector2(Mathf.Abs(this.Rig.velocity.x), this.Rig.velocity.y);
                }
                else if (normalizedPoint.x >= 1f)
                {
                    this.Rig.velocity = new Vector2(-Mathf.Abs(this.Rig.velocity.x), this.Rig.velocity.y);
                }

                float a;
                if (this.fadeInTime < FadeInTime)
                {
                    a = Mathf.Lerp(0f, 1f, this.fadeInTime / FadeInTime);
                    this.fadeInTime += TimeHandler.deltaTime;
                }
                else if (MaxFreeTime >= this.freeFor && MaxFreeTime - this.freeFor <= FadeOutTime)
                {
                    a = Mathf.Lerp(0f, 1f, (MaxFreeTime - this.freeFor) / FadeOutTime);
                }
                else if (MaxFreeTime >= this.freeFor)
                {
                    a = 1f;
                }
                else
                {
                    a = 0f;
                }
                this.Renderer.color = new Color(this.Renderer.color.r, this.Renderer.color.g, this.Renderer.color.b, a);

            }
        }

        void LateUpdate()
        {
            if (this.currentCrownHolder == -1 || this.previousCrownHolder == -1)
            {
                return;
            }
            Vector3 position = Vector3.LerpUnclamped((Vector3)PlayerManager.instance.players[this.previousCrownHolder].data.InvokeMethod("GetCrownPos"), (Vector3)PlayerManager.instance.players[this.currentCrownHolder].data.InvokeMethod("GetCrownPos"), this.crownPos);
            base.transform.position = position;
        }

        public void AddRandomAngularVelocity(float min = -MaxAngularSpeed, float max = MaxAngularSpeed)
        {
            if (this.View.IsMine) { this.View.RPC(nameof(RPCA_AddAngularVel), RpcTarget.All, UnityEngine.Random.Range(min, max)); }
        }
        [PunRPC]
        public void RPCA_AddAngularVel(float angVelToAdd)
        {
            this.Rig.angularVelocity += angVelToAdd;
        }

        public void GiveCrownToPlayer(int playerID)
        {
            if (this.View.IsMine && !this.hidden) { this.View.RPC(nameof(RPCA_GiveCrownToPlayer), RpcTarget.All, playerID); }
        }
        [PunRPC]
        private void RPCA_GiveCrownToPlayer(int playerID)
        {
            this.HeldFor = 0f;
            this.freeFor = 0f;
            this.previousCrownHolder = this.currentCrownHolder == -1 ? playerID : this.currentCrownHolder;
            this.currentCrownHolder = playerID;
            if (this.currentCrownHolder != -1 && !this.hidden) { base.StartCoroutine(this.IGiveCrownToPlayer()); }
        }
        private IEnumerator IGiveCrownToPlayer()
        {
            for (float i = 0f; i < this.transitionCurve.keys[this.transitionCurve.keys.Length - 1].time; i += Time.unscaledDeltaTime)
            {
                this.crownPos = Mathf.LerpUnclamped(0f, 1f, this.transitionCurve.Evaluate(i));
                yield return null;
            }
        }

        private const string SyncedRespawnsKey = "Crown_Respawns";
        private const string SyncedHeldForKey = "Crown_Held_For";
        private const string SyncedFreeForKey = "Crown_Free_For";

        protected override void SetDataToSync()
        {
            this.SetSyncedInt(SyncedRespawnsKey, this.respawns);
            this.SetSyncedFloat(SyncedHeldForKey, this.HeldFor);
            this.SetSyncedFloat(SyncedFreeForKey, this.freeFor);
        }
        protected override void ReadSyncedData()
        {
            this.respawns = this.GetSyncedInt(SyncedRespawnsKey, this.respawns);
            this.HeldFor = this.GetSyncedFloat(SyncedHeldForKey, this.HeldFor);
            this.freeFor = this.GetSyncedFloat(SyncedFreeForKey, this.freeFor);
        }
    }
}

