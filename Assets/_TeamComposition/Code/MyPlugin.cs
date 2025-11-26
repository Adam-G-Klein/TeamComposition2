using BepInEx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BepInDependency("com.willis.rounds.unbound")]
[BepInDependency("pykess.rounds.plugins.moddingutils")]
[BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
[BepInPlugin("com.adamklein.teamcomposition", "TeamComposition2", "0.0.0")]
[BepInProcess("Rounds.exe")]
public class MyPlugin: BaseUnityPlugin{
	internal static string modInitials = "TC";
	internal static AssetBundle asset;
	void Awake(){
		UnityEngine.Debug.Log("here!");
		asset = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("teamcomposition2", typeof(MyPlugin).Assembly);
		UnityEngine.Debug.Log("asset is null? " + asset == null ? "true" : "false");
		
	}
	void Start(){
		UnityEngine.Debug.Log("before load asset!");
		asset.LoadAsset<GameObject>("ModCards").GetComponent<CardHolder>().RegisterCards();
		UnityEngine.Debug.Log("after load asset!");
		
	}
}

