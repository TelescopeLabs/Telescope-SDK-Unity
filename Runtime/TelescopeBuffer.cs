using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace telescope
{
    public class TelescopeBuffer : ScriptableObject
    {

        #region DistinctId

        private const string DistinctIdName = "Telescope.DistinctId";

        private static string _distinctId;

        public static string DistinctId
        {
            get
            {
                if (!string.IsNullOrEmpty(_distinctId)) return _distinctId;
                if (PlayerPrefs.HasKey(DistinctIdName))
                {
                    _distinctId = PlayerPrefs.GetString(DistinctIdName);
                }
                // Generate a Unique ID for this client if still null or empty
                // https://devblogs.microsoft.com/oldnewthing/?p=21823
                if (string.IsNullOrEmpty(_distinctId)) DistinctId = Guid.NewGuid().ToString();
                return _distinctId;
            }
            set
            {
                _distinctId = value;
                PlayerPrefs.SetString(DistinctIdName, _distinctId);
            }
        }

        #endregion

        #region Track

        private const string EventAutoIncrementingIdName = "TL_EventAutoIncrementingID";

        // For performance, we can store the lowest unsent event ID to prevent searching from 0.
        private const string EventStartIndexName = "TL_EventStartIndex";

        internal static void EnqueueTrackingData(TelescopeGenericTrack data)
        {
            data.value = Telescope.MergeValues(data.value, Metadata.GetEventMetadata());
            EnqueueTrackingDataCore(data);
        }

        internal static void EnqueueTrackingData(List<TelescopeGenericTrack> data)
        {
            foreach (TelescopeGenericTrack te in data)
                te.value = Telescope.MergeValues(te.value, Metadata.GetEventMetadata());
            EnqueueTrackingDataCore(data);
        }

        private static void EnqueueTrackingDataCore<T>(T data)
        {
            int eventId = EventAutoIncrementingID();
            string trackingKey = "TL_Event" + eventId.ToString();
            //data["$tl_pref_event_id"] = trackingKey;
            PlayerPrefs.SetString(trackingKey, JsonConvert.SerializeObject(data));
            IncreaseTrackingDataID();
        }

        internal static int EventAutoIncrementingID()
        {
            return PlayerPrefs.GetInt(EventAutoIncrementingIdName, 0);
        }

        internal static int EventStartIndex()
        {
            return PlayerPrefs.GetInt(EventStartIndexName, 0);
        }

        private static void IncreaseTrackingDataID()
        {
            int id = EventAutoIncrementingID();
            id += 1;
            String trackingIdKey = EventAutoIncrementingIdName;
            PlayerPrefs.SetInt(trackingIdKey, id);
        }

        internal static List<TelescopeGenericTrack> DequeueBatchTrackingData(int batchSize)
        {
            List<TelescopeGenericTrack> batch = new List<TelescopeGenericTrack>();
            string startIndexKey = EventStartIndexName;
            int oldStartIndex = EventStartIndex();
            int newStartIndex = oldStartIndex;
            int dataIndex = oldStartIndex;
            int maxIndex = EventAutoIncrementingID() - 1;
            while (batch.Count < batchSize && dataIndex <= maxIndex)
            {
                string trackingKey = "TL_Event" + dataIndex.ToString();
                if (PlayerPrefs.HasKey(trackingKey))
                {
                    try
                    {
                        var strData = PlayerPrefs.GetString(trackingKey);
                        if (strData[0] == '[')
                        {
                            var data = JsonConvert.DeserializeObject<List<TelescopeGenericTrack>>(strData);
                            batch = Enumerable.Union(batch, data).ToList();
                        }
                        else
                        {
                            var data = JsonConvert.DeserializeObject<TelescopeGenericTrack>(strData);
                            batch.Add(data);
                        }
                    }
                    catch (Exception e)
                    {
                        Telescope.LogError($"There was an error processing '{trackingKey}' from the internal object pool: " + e);
                        PlayerPrefs.DeleteKey(trackingKey);

                        if (batch.Count == 0)
                        {
                            // Only update if we didn't find a key prior to deleting this key, since the prior key would be a lower valid index.
                            newStartIndex = Math.Min(dataIndex + 1, maxIndex);
                        }
                    }
                }
                else if (batch.Count == 0)
                {
                    // Keep updating the start index as long as we haven't found anything for our batch yet -- we're looking for the minimum index.
                    newStartIndex = Math.Min(dataIndex + 1, maxIndex);
                }
                dataIndex++;
            }

            if (newStartIndex != oldStartIndex)
            {
                PlayerPrefs.SetInt(startIndexKey, newStartIndex);
            }

            return batch;
        }

        internal static void DeleteBatchTrackingData(int batchSize)
        {
            int deletedCount = 0;
            string startIndexKey = EventStartIndexName;
            int oldStartIndex = EventStartIndex();
            int newStartIndex = oldStartIndex;
            int dataIndex = oldStartIndex;
            int autoIncrementId = EventAutoIncrementingID();
            int maxIndex = autoIncrementId - 1;
            while (deletedCount < batchSize && dataIndex <= maxIndex)
            {
                string trackingKey = "TL_Event" + dataIndex.ToString();
                if (PlayerPrefs.HasKey(trackingKey))
                {
                    PlayerPrefs.DeleteKey(trackingKey);
                    deletedCount++;
                }
                newStartIndex = Math.Min(dataIndex + 1, maxIndex);
                dataIndex++;
            }

            if (dataIndex > maxIndex && deletedCount == (autoIncrementId - oldStartIndex ) )
            {
                // We want to avoid maxIndex from getting too high while having large "empty gaps" stored in PlayerPrefs, otherwise
                // there can be a large number of string concatenation and PlayerPrefs API calls (in extreme cases, 100K+).
                // At this point, we should have iterated through all possible event IDs and can assume that there are no other events
                // stored in preferences (since we deleted them all).
                string idKey = EventAutoIncrementingIdName;
                PlayerPrefs.SetInt(idKey, 0);
                PlayerPrefs.SetInt(startIndexKey, 0);
            }
            else if (newStartIndex != oldStartIndex)
            {
                // There are unsent batches, store the index of where to resume searching for next time.
                PlayerPrefs.SetInt(startIndexKey, newStartIndex);
            }
        }

        //internal static void DeleteBatchTrackingData(List<TelescopeGenericTrack> batch)
        //{
        //    foreach (TelescopeGenericTrack data in batch)
        //    {
        //        String id = (string)data["$tl_event_id"];
        //        if (id != null && PlayerPrefs.HasKey(id))
        //        {
        //            PlayerPrefs.DeleteKey(id);
        //        }
        //    }
        //}

        internal static void DeleteAllTrackingData()
        {
            DeleteBatchTrackingData(int.MaxValue);
        }

        #endregion

    }
}