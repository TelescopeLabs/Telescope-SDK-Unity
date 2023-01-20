namespace telescope
{
    internal static class Config
    {
        // Can be overriden by TelescopeSettings
        internal static bool ShowDebug = false;

        internal static string ApiKey = null;

        //internal static string Url = null;
        internal static bool Enabled = false;
        internal static int FlushInterval = 60;
        internal static int BatchSize = 100;
        internal static int SessionTimeout = 600;


    }
}