using System.Collections.Generic;
using System;

using UnityEngine.Device;
using System.Globalization;

namespace telescope
{
    internal static class Metadata
    {
        private static Int32 _eventCounter = 0;
        private static int _sessionStartEpoch;
        private static String _sessionID;
        private static System.Random _random = new System.Random(Guid.NewGuid().GetHashCode());
        private static string _clientVersion;
        private static string _projectId;
        private static string _gameBundleID;
        private static string _platform;
        private static string _buildGuuid;
        private static string _idfv;

        private static string _operatingSystem;
        private static string _operatingSystemFamily;
        private static bool _isDebugDevice;
        private static string _deviceModel;
        private static string _processorType = SystemInfo.processorType;
        private static string _graphicsDeviceName = SystemInfo.graphicsDeviceName;
        private static int _processorCount = SystemInfo.processorCount;
        private static int _systemMemorySize = SystemInfo.systemMemorySize;

        internal static void InitSession()
        {
            _eventCounter = 0;
            _sessionID = Convert.ToString(_random.Next(0, Int32.MaxValue), 16);
            _sessionStartEpoch = (int)Util.CurrentTimeInSeconds();
            _clientVersion = Application.version;
            _projectId = Application.cloudProjectId;
            _gameBundleID = Application.identifier;
            _platform = Runtime.Name();
            _buildGuuid = Application.buildGUID;
            _idfv = SystemInfo.deviceUniqueIdentifier;
            _operatingSystem = SystemInfo.operatingSystem;
            _isDebugDevice = DebugDevice.IsDebugDevice();
            _deviceModel = SystemInfo.deviceModel;
            _processorType = SystemInfo.processorType;
            _graphicsDeviceName = SystemInfo.graphicsDeviceName;
            _processorCount = SystemInfo.processorCount;
            _systemMemorySize = SystemInfo.systemMemorySize;
            _operatingSystemFamily = SystemInfo.operatingSystemFamily.ToString();
        }
        internal static Dictionary<string, object> GetEventMetadata()
        {
            Dictionary<string, object> eventMetadata = new()
                {
                    {"$tl_event_id", Convert.ToString(_random.Next(0, Int32.MaxValue), 16)},
                    {"$tl_session_id", _sessionID},
                    {"$tl_session_seq_id", _eventCounter},
                    {"$tl_session_start_sec", _sessionStartEpoch},
                    {"$tl_lib", "unity"},
                    {"$tl_lib_version", Telescope.TelescopeUnitySDKVersion },
                    {"$tl_client_version", _clientVersion },
                    {"$tl_project_id", _projectId },
                    {"$tl_game_bundle_id", _gameBundleID },
                    {"$tl_platform", _platform },
                    {"$tl_build_guuid", _buildGuuid },
                    {"$tl_idfv", _idfv },
                    {"$tl_locale", Locale.AnalyticsRegionLanguageCode() },
                    {"$tl_eventtime", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) },

                    // -- these can be changed while game running
                    {"$tl_screen_width", Screen.width },
                    {"$tl_screen_height", Screen.height },
                    {"$tl_screen_dpi", Screen.dpi },
                    {"$tl_device_wifi", Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork},
                    {"$tl_device_radio", Util.GetRadio()},
                    {"$tl_device_volume", DeviceVolumeProvider.GetDeviceVolume() },
                };
            _eventCounter++;
            return eventMetadata;
        }

        internal static Dictionary<string, object> GetEndSessionMetadata(int? timeInSecond = null)
        {
            int _sessionEndEpoch = timeInSecond ?? (int)Util.CurrentTimeInSeconds();
            return new Dictionary<string, object>()
                {
                    { "$tl_session_end_sec", _sessionEndEpoch },
                    { "$tl_session_duration", _sessionEndEpoch - _sessionStartEpoch }
                };
        }

        internal static Dictionary<string, object> GetClientDeviceMetaData()
        {
            return new Dictionary<string, object> {
                {"$tl_operating_system", _operatingSystem},
                {"$tl_operating_system_family", _operatingSystemFamily},
                {"$tl_is_debug_device", _isDebugDevice},
                {"$tl_device_model", _deviceModel},
                {"$tl_processor_type", _processorType},
                {"$tl_graphics_device_name", _graphicsDeviceName},
                {"$tl_processor_count", _processorCount},
                {"$tl_system_memory_size", _systemMemorySize}
            };
        }
    }
}