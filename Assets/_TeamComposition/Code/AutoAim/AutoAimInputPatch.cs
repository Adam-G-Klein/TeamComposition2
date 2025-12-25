using System;
using System.Reflection;
using HarmonyLib;
using InControl;

namespace TeamComposition2.AutoAim
{
    // Patch PlayerActions constructor to add the toggle auto-aim action
    [HarmonyPatch(typeof(PlayerActions))]
    [HarmonyPatch(MethodType.Constructor)]
    [HarmonyPatch(new Type[] { })]
    public class AutoAimPlayerActionsConstructorPatch
    {
        [HarmonyPostfix]
        public static void Postfix(PlayerActions __instance)
        {
            __instance.GetAutoAimData().toggleAutoAim = (PlayerAction)typeof(PlayerActions).InvokeMember("CreatePlayerAction",
                BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic,
                null, __instance, new object[] { "Toggle Auto-Aim" });
        }
    }

    // Patch to add controller bindings (Action4 = Y on Xbox / Triangle on PlayStation = top/north button)
    [HarmonyPatch(typeof(PlayerActions), "CreateWithControllerBindings")]
    public class AutoAimControllerBindingsPatch
    {
        [HarmonyPostfix]
        public static void Postfix(ref PlayerActions __result)
        {
            __result.GetAutoAimData().toggleAutoAim.AddDefaultBinding(InputControlType.Action4);
        }
    }

    // Patch to add keyboard bindings (F key)
    [HarmonyPatch(typeof(PlayerActions), "CreateWithKeyboardBindings")]
    public class AutoAimKeyboardBindingsPatch
    {
        [HarmonyPostfix]
        public static void Postfix(ref PlayerActions __result)
        {
            __result.GetAutoAimData().toggleAutoAim.AddDefaultBinding(Key.F);
        }
    }
}
