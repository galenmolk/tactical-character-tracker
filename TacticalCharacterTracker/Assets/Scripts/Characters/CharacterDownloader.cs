using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.RemoteConfig;
using UnityEngine;

public class CharacterDownloader : MonoBehaviour
{
    [SerializeField] private TextAsset fallbackCharacterListConfig;
    [SerializeField] private ProgressBar progressBar;

    private Coroutine progressFill;
    
    private struct UserAttributes { }

    private struct AppAttributes { }
    
    private readonly AppAttributes appParams = new();

    private void Awake()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
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
        SceneLoadManager.Instance.LoadScene(Scenes.CHARACTER_SELECT);
    }

    private List<CharacterConfig> GetCharacterListConfigForJson(string json)
    {
        return JsonConvert.DeserializeObject<List<CharacterConfig>>(json);
    }

    private List<CharacterConfig> GetConfigOrFallback()
    {
        return GetCharacterListConfigForJson(GetCharacterListJson()) ?? 
               GetCharacterListConfigForJson(fallbackCharacterListConfig.text);
    }
    
    private string GetCharacterListJson()
    {
        return ConfigManager.appConfig.GetJson(RemoteConfigKeys.HERO_LIST_KEY);
    }
}
