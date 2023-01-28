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
                    {"$tlv_user_id", TelescopeBuffer.DistinctId },
                    {"$tlv_event_id", Convert.ToString(_random.Next(0, Int32.MaxValue), 16)},
                    {"$tlv_session_id", _sessionID},
                    {"$tlv_session_seq_id", _eventCounter},
                    {"$tlv_session_start_sec", _sessionStartEpoch},
                    {"$tlv_lib", "unity"},
                    {"$tlv_lib_version", Telescope.TelescopeUnitySDKVersion },
                    {"$tlv_client_version", _clientVersion },
                    {"$tlv_project_id", _projectId },
                    {"$tlv_game_bundle_id", _gameBundleID },
                    {"$tlv_platform", _platform },
                    {"$tlv_build_guuid", _buildGuuid },
                    {"$tlv_idfv", _idfv },
                    {"$tlv_locale", Locale.AnalyticsRegionLanguageCode() },
                    {"$tlv_eventtime", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) },

                    // -- these can be changed while game running
                    {"$tlv_screen_width", Screen.width },
                    {"$tlv_screen_height", Screen.height },
                    {"$tlv_screen_dpi", Screen.dpi },
                    {"$tlv_device_wifi", Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork},
                    {"$tlv_device_radio", Util.GetRadio()},
                    {"$tlv_device_volume", DeviceVolumeProvider.GetDeviceVolume() },
                };
            _eventCounter++;
            return eventMetadata;
        }

        internal static Dictionary<string, object> GetEndSessionMetadata(int? timeInSecond = null)
        {
            int _sessionEndEpoch = timeInSecond ?? (int)Util.CurrentTimeInSeconds();
            return new Dictionary<string, object>()
                {
                    { "$tlv_session_end_sec", _sessionEndEpoch },
                    { "$tlv_session_duration", _sessionEndEpoch - _sessionStartEpoch }
                };
        }

        internal static Dictionary<string, object> GetClientDeviceMetaData()
        {
            return new Dictionary<string, object> {
                {"$tlv_operating_system", _operatingSystem},
                {"$tlv_operating_system_family", _operatingSystemFamily},
                {"$tlv_is_debug_device", _isDebugDevice},
                {"$tlv_device_model", _deviceModel},
                {"$tlv_processor_type", _processorType},
                {"$tlv_graphics_device_name", _graphicsDeviceName},
                {"$tlv_processor_count", _processorCount},
                {"$tlv_system_memory_size", _systemMemorySize}
            };
        }
    }
}