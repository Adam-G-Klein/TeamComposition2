namespace TeamComposition2.Bots.Utils
{
    public static class BotLoggerUtils
    {
        public static void Log(string message)
        {
            if (BotMenu.DebugMode.Value)
            {
                UnityEngine.Debug.Log($"[TC2-Bots] {message}");
            }
        }

        public static void LogWarning(string message)
        {
            if (BotMenu.DebugMode.Value)
            {
                UnityEngine.Debug.LogWarning($"[TC2-Bots] {message}");
            }
        }

        public static void Error(string message)
        {
            if (BotMenu.DebugMode.Value)
            {
                UnityEngine.Debug.LogError($"[TC2-Bots] {message}");
            }
        }
    }
}
