using System;
using System.Collections.Generic;
using UnityEngine;

namespace BonkIncStandard.Utils.Http
{
    [CreateAssetMenu(fileName = "ApiConfigManager", menuName = "http/api config manager")]
    public class ApiConfigManager : ScriptableObject
    {
        [SerializeField] 
        private LoadOptions loadOption = LoadOptions.ONLY_DEV_IN_EDITOR;

        [SerializeField] 
        private API[] configEntries;

        private Dictionary<string, ApiConfig> configs;
        public static ApiConfigManager Instance { get; private set; }

        public ApiConfig GetApiConfig(string name)
        {
            return configs[name];
        }

        private void OnEnable()
        {
            Debug.Log("Enabling");
            if (Instance != null)
            {
                Destroy(this);
                throw new System.Exception("ApiConfigManager not only instance");
            }

            Instance = this;
            switch (loadOption)
            {
                case LoadOptions.ALWAYS_DEV:
                    LoadDevConfigs();
                    break;
                case LoadOptions.ALWAYS_PROD:
                    LoadProdConfigs();
                    break;
                case LoadOptions.ONLY_DEV_IN_EDITOR:
                    LoadConfigsDynamic();
                    break;
                default:
                    Debug.LogWarning("Uknown load option, loading dymanic");
                    LoadConfigsDynamic();
                    break;
            }
        }

        private void LoadProdConfigs()
        {
            configs = new Dictionary<string, ApiConfig>();
            foreach (var config in configEntries)
            {
                configs.Add(config.name, config.production);
            }
        }

        private void LoadDevConfigs()
        {
            configs = new Dictionary<string, ApiConfig>();
            foreach (var config in configEntries)
            {
                configs.Add(config.name, config.dev);
            }
        }

        private void LoadConfigsDynamic()
        {
#if UNITY_EDITOR
            LoadDevConfigs();
#else
            LoadProdConfigs();
#endif
        }

        [Serializable]
        private struct API
        {
            public string name;
            public ApiConfig production, dev;
        }

        private enum LoadOptions
        {
            ALWAYS_PROD,
            ALWAYS_DEV,
            ONLY_DEV_IN_EDITOR
        }
    }
}