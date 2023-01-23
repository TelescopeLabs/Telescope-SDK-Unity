using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using telescope;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.TelescopeLabs.Scripts
{
    public class TelescopeNetwork : ScriptableObject
    {
        private static string Url;

        internal static void Initialize()
        {
            Url = "https://api.telescopelabs.io/api/eventrouter?apikey=" + Config.ApiKey;
        }
        // List<TelescopeGenericTrack> or TelescopeGenericTrack
        internal static void SendTrack<T>(T batch)
        {
            string body = JsonConvert.SerializeObject(batch);
            _ = PostRequestTask(body);
        }


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
            if (!Config.Enabled) return;
            int resendCounter = 0;
            while (batch.Count > 0)
            {
                // protect against too much network usage before next interval.
                if (resendCounter > 1 && batch.Count < Config.BatchSize) return;
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
                    Telescope.LogError("Error While Sending: " + req.error);
                    if (Application.internetReachability == NetworkReachability.NotReachable)
                    {
                        Telescope.LogError("Error. Check internet connection! Telescope will try " + Config.FlushInterval + " seconds later again.");
                    }
                    req.Dispose();
                    return;
                }
                else
                {
                    resendCounter++;
                    TelescopeBuffer.DeleteBatchTrackingData(batch.Count);
                    batch = TelescopeBuffer.DequeueBatchTrackingData(Config.BatchSize);
                    Telescope.Log("\nReceived: " + req.downloadHandler.text);
                }
                req.Dispose();
            }
        }

        private static async Task PostRequestTask(string body)
        {
            if (!Config.Enabled) return;

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
                Telescope.LogError("Error While Sending: " + req.error);
            }
            else
            {
                Telescope.Log("\nReceived: " + req.downloadHandler.text);
            }
            req.Dispose();
        }

    }
}