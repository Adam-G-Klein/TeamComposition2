using System;
using System.Runtime.CompilerServices;
using InControl;

namespace TeamComposition2.AutoAim
{
    public class AutoAimInputData
    {
        public PlayerAction toggleAutoAim;
        public bool toggleAutoAimWasPressed = false;
    }

    public static class AutoAimInputExtension
    {
        public static readonly ConditionalWeakTable<PlayerActions, AutoAimInputData> data =
            new ConditionalWeakTable<PlayerActions, AutoAimInputData>();

        public static AutoAimInputData GetAutoAimData(this PlayerActions playerActions)
        {
            return data.GetOrCreateValue(playerActions);
        }

        public static void AddAutoAimData(this PlayerActions playerActions, AutoAimInputData value)
        {
            try
            {
                data.Add(playerActions, value);
            }
            catch (Exception) { }
        }

        public static bool ToggleAutoAimWasPressed(this PlayerActions playerActions) => playerActions.GetAutoAimData().toggleAutoAimWasPressed;
    }
}
