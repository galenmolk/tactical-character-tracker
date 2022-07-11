using System.Collections;
using System.Collections.Generic;
using HexedHeroes.Utils;
using Newtonsoft.Json;
using Unity.RemoteConfig;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexedHeroes.Player
{
    public class CharacterDownloader : MonoBehaviour
    {
        [SerializeField] private bool loadLocalConfig;
        [SerializeField] private TextAsset fallbackCharacterListConfig;
        [SerializeField] private ProgressBar progressBar;
    
        private Coroutine progressFill;
    
        private struct UserAttributes { }

        private struct AppAttributes { }
    
        private readonly AppAttributes appParams = new();

        private void Awake()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable || loadLocalConfig)
            {
                LoadCharacterConfigs();
                return;
            }
        
            GetRemoteConfigSettings();
        }
    
        private void GetRemoteConfigSettings()
        {
            progressFill = StartCoroutine(progressBar.Fill());
            ConfigManager.FetchCompleted += ParseResponse;
            ConfigManager.FetchConfigs(new UserAttributes(), appParams);
        }
    
        private void ParseResponse(ConfigResponse response)
        {
            ConfigManager.FetchCompleted -= ParseResponse;
            StartCoroutine(WaitAndLoad());
        }

        private IEnumerator WaitAndLoad()
        {
            yield return progressFill;
            LoadCharacterConfigs();
        }

        private void LoadCharacterConfigs()
        {
            ActiveSession.AvailableCharacters = GetConfigOrFallback();
            SceneManager.LoadScene(SceneKeys.CHARACTER_SELECT);
        }

        private List<CharacterConfig> GetCharacterListConfigForJson(string json)
        {
            return JsonConvert.DeserializeObject<List<CharacterConfig>>(json);
        }

        private List<CharacterConfig> GetConfigOrFallback()
        {
            if (loadLocalConfig)
                return GetCharacterListConfigForJson(fallbackCharacterListConfig.text);
        
            return GetCharacterListConfigForJson(GetCharacterListJson()) ?? 
                   GetCharacterListConfigForJson(fallbackCharacterListConfig.text);
        }
    
        private string GetCharacterListJson()
        {
            return ConfigManager.appConfig.GetJson(RemoteConfigKeys.HERO_LIST_KEY);
        }
    }
}
