using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using telescope;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.TelescopeLabs.Scripts
{
    public class TelescopeNetwork : ScriptableObject
    {
        private static string Url;
        private static int _retryCount = 0;
        private static DateTime _retryTime;

        internal static void Initialize()
        {
            Url = "https://api.telescopelabs.io/api/eventrouter?apikey=" + Config.ApiKey;
        }
        // List<TelescopeGenericTrack> or TelescopeGenericTrack
        //internal static void SendTrack<T>(T batch)
        //{
        //    string body = JsonConvert.SerializeObject(batch);
        //    _ = PostRequestTask(body);
        //}


        // using for buffered data and batch send.
        internal static void SendBatchedTrack(List<TelescopeGenericTrack> batch)
        {
            _ = DequeueAndPostRequestTask(batch);
        }

        public static void Track(TelescopeGenericTrack te)
        {
            TelescopeBuffer.EnqueueTrackingData(te);
        }

        public static void Track(List<TelescopeGenericTrack> tes)
        {
            TelescopeBuffer.EnqueueTrackingData(tes);
        }

        private static async Task DequeueAndPostRequestTask(List<TelescopeGenericTrack> batch)
        {

            if (_retryTime > DateTime.Now && _retryCount > 0) return;

            while (batch.Count > 0)
            {

                string body = JsonConvert.SerializeObject(batch);

                byte[] payload = new System.Text.UTF8Encoding().GetBytes(body);

                var req = new UnityWebRequest(Url, "POST");

                req.uploadHandler = new UploadHandlerRaw(payload);
                req.downloadHandler = new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");

                //Send the request then wait here until it returns
                req.SendWebRequest();
                while (!req.isDone)
                {
                    await Task.Yield();
                }

#if UNITY_2020_1_OR_NEWER
                if (req.result != UnityWebRequest.Result.Success)
#else
                if (request.isHttpError || request.isNetworkError)
#endif
                {
                    Telescope.LogError("API request has failed with reason " + req.error);
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        Telescope.LogError("Error. Check internet connection!");
                    }
                    double retryIn = Math.Pow(2, _retryCount - 1) * 60;
                    retryIn = Math.Min(retryIn, 10 * 60); // limit 10 min
                    _retryTime = DateTime.Now;
                    _retryTime = _retryTime.AddSeconds(retryIn);
                    TelescopeBuffer.EnqueueTrackingData(batch, false);
                    Telescope.Log("Retrying request in " + retryIn + " seconds (retryCount=" + _retryCount + ")");
                    req.Dispose();
                    return;
                }
                else
                {
                    _retryCount = 0;
                    batch = TelescopeBuffer.DequeueBatchTrackingData(Config.BatchSize);
                    Telescope.Log("\nReceived: " + req.downloadHandler.text);
                }
                req.Dispose();
            }
        }
    }
}