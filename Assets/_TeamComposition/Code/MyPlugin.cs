using BepInEx;
using System.Collections;
using System.Collections.Generic;
using HarmonyLib;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnboundLib.Utils.UI;
using UnityEngine;
using TMPro;
using System.Linq;
using TeamComposition2;
using TeamComposition2.GameModes;
using TeamComposition2.Patches;
using TeamComposition2.GameModes.Physics;
using TeamComposition2.CardRoles;
using TeamComposition2.Stats;

namespace TeamComposition2
{
	[BepInDependency("com.willis.rounds.unbound")]
	[BepInDependency("pykess.rounds.plugins.moddingutils")]
	[BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
	[BepInDependency("pykess.rounds.plugins.mapembiggener")]
	[BepInDependency("io.olavim.rounds.mapsextended")]
	[BepInDependency("io.olavim.rounds.rwf")]
	[BepInPlugin("com.adamklein.teamcomposition", "TeamComposition2", "0.0.0")]
	[BepInProcess("Rounds.exe")]
	public class MyPlugin: BaseUnityPlugin{
		internal static string modInitials = "TC";
		internal static AssetBundle asset;
		void Awake(){
		UnityEngine.Debug.Log("here!");
		asset = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("teamcomposition2", typeof(MyPlugin).Assembly);
		UnityEngine.Debug.Log("asset is null? " + (asset == null ? "true" : "false"));

		// Initialize card toggles
		TeamComposition2.CardToggleManager.Initialize();

		// Initialize friendly fire settings and patches
		TeamComposition2.FriendlyFireManager.Initialize(Config);

		// Initialize respawn invulnerability settings and patches
		TeamComposition2.RespawnInvulnerabilityManager.Initialize(Config);

		// Initialize stat modifier settings for Point Control mode
		TeamComposition2.GameModes.StatModifierSettings.Initialize(Config);

		// Register stat modifier application hooks
		TeamComposition2.GameModes.StatModifierApplicator.RegisterHooks();

		// Initialize team spawn persistence patches
		new Harmony("com.adamklein.teamcomposition.teamspawns").PatchAll(typeof(TeamSpawnAssignmentPatch));

		// Disable card selection input while game is paused
		new Harmony("com.adamklein.teamcomposition.cardchoicepause").PatchAll(typeof(TeamComposition2.Patches.CardChoicePausePatch));

		// Card role visual patches (text and icon indicators)
		new Harmony("com.adamklein.teamcomposition.cardroletext").PatchAll(typeof(TeamComposition2.CardRoles.CardInfoAwakePatch));
		new Harmony("com.adamklein.teamcomposition.cardroleicon").PatchAll(typeof(TeamComposition2.CardRoles.CardInfoIconAwakePatch));

		// Healing effectiveness patches (for pure healing effects like Healing Field and Christmas Cheer)
		new Harmony("com.adamklein.teamcomposition.healingeffectiveness").PatchAll(typeof(TeamComposition2.Patches.HealingEffectivenessPatches));

		// Initialize bot manager and patches
		TeamComposition2.Bots.BotManager.Initialize(Config, asset);

		// Register cleanup hooks
		GameModeManager.AddHook(GameModeHooks.HookGameEnd, ResetEffects);
		GameModeManager.AddHook(GameModeHooks.HookGameStart, ResetEffects);
		GameModeManager.AddHook(GameModeHooks.HookPointEnd, PhysicsItemRemover.RemoveItemsOnPointEnd);
		GameModeManager.AddHook(GameModeHooks.HookPointStart, MapControlPointSpawner.EnsureControlPointExists);
		// Apply card toggles before each pick phase (HookPickStart fires when card selection UI appears)
		GameModeManager.AddHook(GameModeHooks.HookGameStart, ApplyCardTogglesBeforePick);
		GameModeManager.AddHook(GameModeHooks.HookPickStart, ApplyCardTogglesBeforePick);

		// Register card role tracking hooks
		GameModeManager.AddHook(GameModeHooks.HookGameStart, PlayerCardRolesExtension.ResetAllPlayerCardRoles);
		GameModeManager.AddHook(GameModeHooks.HookPlayerPickEnd, PlayerCardRolesExtension.RecalculateAllPlayerCardRoles);

		// Register healing effectiveness reset hook
		GameModeManager.AddHook(GameModeHooks.HookGameStart, ResetHealingEffectiveness);
		}
		void Start(){
		UnityEngine.Debug.Log("before load asset!");
		asset.LoadAsset<GameObject>("ModCards").GetComponent<CardHolder>().RegisterCards();

		// Register Mistletoe card
		CustomCard.BuildCard<MistletoeCard>();
        // Register Empty Zen card
        CustomCard.BuildCard<EmptyZenCard>();
        // Register Float Like a Butterfly card
        CustomCard.BuildCard<FloatLikeAButterflyCard>();
        // Register Self-Sufficient card
        CustomCard.BuildCard<SelfSufficientCard>();
		GameModeManager.AddHandler<GM_CrownControl>(CrownControlHandler.GameModeID, new CrownControlHandler());
		GameModeManager.AddHandler<GM_CrownControl>(TeamCrownControlHandler.GameModeID, new TeamCrownControlHandler());

		// Register card toggle menu
		Unbound.RegisterMenu("Toggle TC Cards", () => { }, BuildCardToggleMenu, null, false);

		// Register friendly fire menu
		TeamComposition2.FriendlyFireManager.RegisterMenu();

		// Register respawn invulnerability menu (pause menu)
		TeamComposition2.RespawnInvulnerabilityManager.RegisterMenu();

		// Register bot menu and handlers
		TeamComposition2.Bots.BotManager.RegisterMenuAndHandshake(Config, asset);

		// Register Point Control settings menu (main menu only)
		TeamComposition2.GameModes.CrownControlMenu.RegisterMenu();

		// Register in-game stat settings menu (pause menu only)
		TeamComposition2.GameModes.InGameStatMenu.RegisterMenu();

		UnityEngine.Debug.Log("after load asset!");
		}

		private void BuildCardToggleMenu(GameObject menu)
		{
		MenuHandler.CreateText("TeamComposition Card Toggle", menu, out var _, 60);
		MenuHandler.CreateText("Apply card toggles from enableCards.txt", menu, out var _, 30);
		var applyButton = MenuHandler.CreateButton("Apply Card Toggles", menu, null);
		var buttonText = applyButton.GetComponentInChildren<TextMeshProUGUI>();
		applyButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
			TeamComposition2.CardToggleManager.ApplyCardToggles();
			buttonText.text = "APPLIED!";
		});
		}
		
		private IEnumerator ResetEffects(IGameModeHandler gm)
		{
		DestroyAll<MistletoeMono>();
		DestroyAll<FrozenMono>();
		DestroyAll<IceRing>();
		yield break;
		}

		private IEnumerator ApplyCardTogglesBeforePick(IGameModeHandler gm)
		{
		TeamComposition2.CardToggleManager.ApplyCardToggles();
		yield break;
		}

		private IEnumerator ResetHealingEffectiveness(IGameModeHandler gm)
		{
		HealingEffectivenessExtension.ResetAllPlayerHealingEffectiveness();
		yield break;
		}
		
		private void DestroyAll<T>() where T : UnityEngine.Object
		{
		T[] array = UnityEngine.Object.FindObjectsOfType<T>();
		for (int i = array.Length - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(array[i]);
		}
		}
	}

	/// <summary>
	/// Rare card that lets a player's own healing fields heal themselves.
	/// </summary>
	public class SelfSufficientCard : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			// No direct stat changes; effect handled via OnAdd/OnRemove.
		}

		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			if (player != null)
			{
				player.SetCanHealSelfWithHealingFields(true);
			}
		}

		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			if (player == null)
			{
				return;
			}

			// Keep the flag enabled if another copy of the card remains.
			bool hasAnotherCopy = player.data?.currentCards?.Any(c => c != null && c.cardName == GetTitle()) == true;
			if (!hasAnotherCopy)
			{
				player.SetCanHealSelfWithHealingFields(false);
			}
		}

		protected override string GetTitle() => "Self-Sufficient";

		protected override string GetDescription() => "Your healing fields now heal you too.";

		protected override GameObject GetCardArt() => MyPlugin.asset?.LoadAsset<GameObject>("C_HealingField");

		protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Rare;

		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Healing fields",
					amount = "Can heal yourself",
					simepleAmount = CardInfoStat.SimpleAmount.aLotOf
				}
			};
		}

		protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.DefensiveBlue;

		public override string GetModName() => MyPlugin.modInitials;
	}
}

