using BepInEx;
using System.Collections;
using System.Collections.Generic;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;
using TeamComposition2;
using TeamComposition2.GameModes;
using TeamComposition2.GameModes.Physics;

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
		
		// Register cleanup hooks
		GameModeManager.AddHook(GameModeHooks.HookGameEnd, ResetEffects);
		GameModeManager.AddHook(GameModeHooks.HookGameStart, ResetEffects);
		GameModeManager.AddHook(GameModeHooks.HookPointEnd, PhysicsItemRemover.RemoveItemsOnPointEnd);
		GameModeManager.AddHook(GameModeHooks.HookPointStart, MapControlPointSpawner.EnsureControlPointExists);
	}
	void Start(){
		UnityEngine.Debug.Log("before load asset!");
		asset.LoadAsset<GameObject>("ModCards").GetComponent<CardHolder>().RegisterCards();
		
		// Register Mistletoe card
		CustomCard.BuildCard<MistletoeCard>();
		GameModeManager.AddHandler<GM_CrownControl>(CrownControlHandler.GameModeID, new CrownControlHandler());
		GameModeManager.AddHandler<GM_CrownControl>(TeamCrownControlHandler.GameModeID, new TeamCrownControlHandler());

		UnityEngine.Debug.Log("after load asset!");
	}
	
	private IEnumerator ResetEffects(IGameModeHandler gm)
	{
		DestroyAll<MistletoeMono>();
		DestroyAll<FrozenMono>();
		DestroyAll<IceRing>();
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

