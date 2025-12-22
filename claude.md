I am creating a mod for the game Rounds, introducing a lot of sweeping changes to the gameplay.

- Assets\_TeamComposition\Code\MyPlugin.cs is the main plugin for the mod. Hooks are added there that will run at different points throughout gameplay and setup
- You can always refer to source code in OtherMods/ to see how other mods are performing similar tasks. Some extra important mods:
	- UnboundLib is in there and is very important, it provides a lot of API scaffolding for all of the other mods.
	- RoundsWithFriends is a very key mod and is highly stable. The whole modding community exists because of this mod.
- When adding new files to the project, always remember to update the .csproj file with references to the new file so that we avoid build errors.
- If you're copying an existing mod into this codebase, remember to copy over all of the unity assets and mark them with the correct asset bundle name "teamcomposition2". If you've done this, notify me at the end of the task so that I can rebuild my assetBundle and the assets will exist when you're done porting the mod


 
