using System.Collections;
using Newtonsoft.Json;
using Unity.RemoteConfig;
using UnityEngine;

public class CharacterDownloader : Singleton<CharacterDownloader>
{
    public readonly CharacterListEvent CharactersDownloaded = new();
    public CharacterListConfig CharacterListConfig { get; private set; }
    
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
        
        Debug.Log("Internet detected");
        GetRemoteConfigSettings();
    }
    
    private void GetRemoteConfigSettings()
    {
        progressFill = StartCoroutine(progressBar.Fill());
        ConfigManager.FetchCompleted += ParseResponse;
        Debug.Log("Fetching Configs");
        ConfigManager.FetchConfigs(new UserAttributes(), appParams);
    }
    
    private void ParseResponse(ConfigResponse response)
    {
        ConfigManager.FetchCompleted -= ParseResponse;
        Debug.Log("Done Fetching Configs");
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        Debug.Log("Waiting For Fill");
        yield return progressFill;
        Debug.Log("Done");
        LoadCharacterConfigs();
    }

    private void LoadCharacterConfigs()
    {
        CharacterListConfig = GetConfigOrFallback();
        CharactersDownloaded.Invoke(CharacterListConfig);
        SceneLoadManager.Instance.LoadScene(Scenes.CHARACTER_SELECT);
    }

    private CharacterListConfig GetCharacterListConfigForJson(string json)
    {
        return JsonConvert.DeserializeObject<CharacterListConfig>(json);
    }

    private CharacterListConfig GetConfigOrFallback()
    {
        return GetCharacterListConfigForJson(GetCharacterListJson()) ?? 
               GetCharacterListConfigForJson(fallbackCharacterListConfig.text);
    }
    
    private string GetCharacterListJson()
    {
        return ConfigManager.appConfig.GetJson(RemoteConfigKeys.CHARACTER_LIST_KEY);
    }
}
