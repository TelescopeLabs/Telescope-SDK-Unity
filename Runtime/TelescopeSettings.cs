using System;
using System.IO;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace telescope
{
    public class TelescopeSettings : ScriptableObject
    {
        //TODO: Convert to log level
        [Tooltip("If true will print helpful debugging messages")] 
        public bool ShowDebug;
        //[Tooltip("The api host of where to send the requests to. Useful when you need to proxy all the request to somewhere else.'")]
        //public string APIHostAddress = "https://api.telescopelabs.io";
        [Tooltip("The token of the Telescope project. Telescope Api won't be enabled if you don't enter your api key.")]
        public string ApiKey = "";
        [Tooltip("Api won't send data If not checked ")]
        public bool Enabled = true;
        [Tooltip("Telescope will send events periodically")]
        public int FlushInterval = 60;
        [Tooltip("The session will be restarted when the game stays in the background for the specified time.")]
        public int SessionTimeout = 600;

        internal string Token {
            get {
                return ApiKey;
            }
        }

        public void ApplyToConfig()
        {
            Config.ShowDebug = ShowDebug;
            Config.ApiKey = this.Token;
            //Config.Url= this.APIHostAddress;
            Config.Enabled = this.Enabled;
            if (this.FlushInterval > 300)
                this.FlushInterval = 300;
            else if (this.FlushInterval< 15)
                this.FlushInterval = 15;
            Config.FlushInterval = this.FlushInterval;
            if (this.SessionTimeout < 60)
                this.SessionTimeout= 60;
            Config.SessionTimeout = this.SessionTimeout;
            if (Config.ApiKey == null || Config.ApiKey == "")
            {
                Config.Enabled = false;
            }
        }

        #region static
        private static TelescopeSettings _instance;

        public static void LoadSettings()
        {
            if (!_instance)
            {
                _instance = FindOrCreateInstance();
                _instance.ApplyToConfig();
            }
        }

        public static TelescopeSettings Instance {
            get {
                LoadSettings();
                return _instance;
            }
        }

        private static TelescopeSettings FindOrCreateInstance()
        {
            TelescopeSettings instance = null;
            instance = instance ? null : Resources.Load<TelescopeSettings>("Telescope");
            instance = instance ? instance : Resources.LoadAll<TelescopeSettings>(string.Empty).FirstOrDefault();
            instance = instance ? instance : CreateAndSave<TelescopeSettings>();
            if (instance == null) throw new Exception("Could not find or create settings for Telescope");
            return instance;
        }

        private static T CreateAndSave<T>() where T : ScriptableObject
        {
            T instance = CreateInstance<T>();
#if UNITY_EDITOR
            //Saving during Awake() will crash Unity, delay saving until next editor frame
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorApplication.delayCall += () => SaveAsset(instance);
            }
            else
            {
                SaveAsset(instance);
            }
#endif
            return instance;
        }

#if UNITY_EDITOR
        private static void SaveAsset<T>(T obj) where T : ScriptableObject
        {

            string dirName = "Assets/Resources";
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            AssetDatabase.CreateAsset(obj, "Assets/Resources/Telescope.asset");
            AssetDatabase.SaveAssets();
        }
#endif
        #endregion
    }
}
