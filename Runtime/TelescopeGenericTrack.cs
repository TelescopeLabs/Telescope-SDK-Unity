
using Newtonsoft.Json;
using System.Collections.Generic;

namespace telescope
{
    public class TelescopeGenericTrack
    {
        public string entityName;
        public string type = "insert";
        public string id
        {
            get => TelescopeBuffer.DistinctId;
            internal set { id = value; }
        }
        public Dictionary<string, object> value = new();

        public TelescopeGenericTrack()
        {
        }

        public TelescopeGenericTrack(string entityName, Dictionary<string, object> value)
        {
            this.entityName = entityName;
            this.value = value;
        }

        internal object this[string key]
        {
            get { return value[key]; }
            set { this.value[key] = value; }
        }

        #region StandartEvents

        internal static TelescopeGenericTrack StartSessionEvent()
        {
            return new TelescopeGenericTrack()
            {
                entityName = "session_start"
            };
        }

        internal static TelescopeGenericTrack GameRunningEvent(bool isFocused)
        {
            return new TelescopeGenericTrack()
            {
                entityName = "game_running",
                value = new Dictionary<string, object>()
                {
                    { "$tl_game_is_focused", isFocused }
                }
            };
        }

        internal static TelescopeGenericTrack ClientDeviceEvent()
        {
            return new TelescopeGenericTrack()
            {
                entityName = "client_device"
            };
        }

        internal static TelescopeGenericTrack EndSessionEvent(int? timeInSecond = null)
        {
            return new TelescopeGenericTrack()
            {
                entityName = "session_end",
                value = Metadata.GetEndSessionMetadata(timeInSecond)
            };
        }

        #endregion
    }

}
