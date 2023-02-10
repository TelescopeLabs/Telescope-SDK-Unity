using Assets.TelescopeLabs.Scripts;
using System.Collections.Generic;
using System.Linq;

//TODO: common parametreler icin bir yapi dusun.

namespace telescope
{
    public static partial class Telescope
    {
        internal const string TelescopeUnitySDKVersion = "1";

        public static void Track(TelescopeGenericTrack te, bool manualMapping = false)
        {
            if (!Config.Enabled) return;
            if (manualMapping) te.value.Add("$tlv_custom_event_map", manualMapping);
            TelescopeNetwork.Track(te);
        }
        public static void Track(List<TelescopeGenericTrack> tes, bool manualMapping = false)
        {
            if (!Config.Enabled) return;
            foreach(TelescopeGenericTrack te in tes) te.value.Add("$tlv_custom_event_map", manualMapping);
            TelescopeNetwork.Track(tes);
        }

        public static void Track(string entityName, Dictionary<string, object> value, bool manualMapping = false)
        {
            if (!Config.Enabled) return;
            if (manualMapping) value.Add("$tlv_custom_event_map", manualMapping);
            TelescopeNetwork.Track(new TelescopeGenericTrack(entityName, value));
        }

        // You must call the function in awake to use your userId.
        public static void SetCustomUserID(string userId)
        {
            TelescopeBuffer.DistinctId = userId;
        }

        #region TelescopeAPIRelatedUtils
        internal static Dictionary<string, object> MergeValues(Dictionary<string, object> val1, Dictionary<string, object> val2)
        {
            return val1.Concat(val2).ToDictionary(e => e.Key, e => e.Value);
        }

        internal static Dictionary<string, object> MergeValues(Dictionary<string, object> val1, Dictionary<string, object> val2, Dictionary<string, object> val3)
        {
            return val1.Concat(val2).Concat(val3).ToDictionary(e => e.Key, e => e.Value);
        }

        internal static Dictionary<string, object> MergeValues(Dictionary<string, object> val1, Dictionary<string, object> val2, Dictionary<string, object> val3, Dictionary<string, object> val4)
        {
            return val1.Concat(val2).Concat(val3).Concat(val4).ToDictionary(e => e.Key, e => e.Value);
        }
        #endregion

    }

}