using UnityEngine;

namespace telescope
{
    public static partial class Telescope
    {
        public static void Log(string s)
        {
            if (Config.ShowDebug)
            {
                Debug.Log("[Telescope] " + s);
            }
        }

        public static void LogError(string s)
        {
            if (Config.ShowDebug)
            {
                Debug.LogError("[Telescope] " + s);
            }
        }
    }
}
