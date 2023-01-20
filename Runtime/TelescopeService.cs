using Assets.TelescopeLabs.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace telescope
{

    internal class TelescopeService : MonoBehaviour
    {
        private int sessionLastTime = (int)Util.CurrentTimeInSeconds();
        private bool sessionActive = false;

        #region Singleton

        private static TelescopeService _instance;
        protected GameObject _gameObject;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeBeforeSceneLoad()
        {
            TelescopeSettings.LoadSettings();
            //if (Config.ManualInitialization) return;
            Initialize();
        }

        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        //private static void InitializeAfterSceneLoad()
        //{
        //}

        internal static void Initialize()
        {
            // Copy over any runtime changes that happened before initialization from settings instance to the config.
            TelescopeSettings.Instance.ApplyToConfig();
            TelescopeNetwork.Initialize();
            GetInstance();
        }

        internal static bool IsInitialized()
        {
            return _instance != null;
        }

        internal static void Disable()
        {
            if (_instance != null)
            {
                Destroy(_instance);
            }
        }

        internal static TelescopeService GetInstance()
        {
            if (_instance == null)
            {
                GameObject g = new GameObject("Telescope");
                _instance = g.AddComponent<TelescopeService>();
                _instance._gameObject = g;
                DontDestroyOnLoad(g);
            }
            return _instance;
        }
        #endregion


        #region GameLifeCycle
        private void Start()
        {
            Telescope.Log("Telescope event tracking started");
            StartSession();
            StartCoroutine(WaitAndFlush());
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                sessionActive = false;
                sessionLastTime = (int)Util.CurrentTimeInSeconds();
            }
            else
            {
                if (!sessionActive && Util.CurrentTimeInSeconds() - sessionLastTime > Config.SessionTimeout)
                {
                    RestartSession();
                    return;
                }
                sessionActive = true;
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                sessionActive = false;
                sessionLastTime = (int)Util.CurrentTimeInSeconds();
            }
            else
            {
                sessionActive = true;
            }
        }


        private void OnDestroy()
        {

            EndSession();
            // try send.
            DoFlush();
            Telescope.Log("Telescope Service Destroying.");

        }

        #endregion

        private IEnumerator WaitAndFlush()
        {
            // Send first events.
            DoFlush();
            sessionActive = true;
            while (true)
            {
                yield return new WaitForSecondsRealtime(Config.FlushInterval);
                Telescope.Track(TelescopeGenericTrack.GameRunningEvent(sessionActive));
                DoFlush();
            }
        }

        internal void DoFlush()
        {
            TelescopeNetwork.SendBatchedTrack(TelescopeBuffer.DequeueBatchTrackingData(Config.BatchSize));
        }


        // Send immidiately
        private static void StartSession()
        {
            Metadata.InitSession();
            TelescopeNetwork.Track(new List<TelescopeGenericTrack>() { TelescopeGenericTrack.StartSessionEvent(), TelescopeGenericTrack.ClientDeviceEvent() });
        }

        // Send immidiately
        private static void EndSession(int? timeInSecond = null)
        {
            TelescopeNetwork.Track(TelescopeGenericTrack.EndSessionEvent(timeInSecond));
        }

        private void RestartSession()
        {
            EndSession(sessionLastTime);
            StartSession();
            sessionLastTime = (int)Util.CurrentTimeInSeconds();
            sessionActive = true;
        }

    }

}