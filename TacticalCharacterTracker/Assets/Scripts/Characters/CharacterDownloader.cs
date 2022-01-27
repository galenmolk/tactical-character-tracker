using System;
using System.Collections;
using Newtonsoft.Json;
using Unity.RemoteConfig;
using UnityEngine;

public class CharacterDownloader : MonoBehaviour
{
    [SerializeField] private LoadingText loadingText;
    [SerializeField] private TextAsset fallbackCharacterListConfig;
    
    private struct UserAttributes { }

    private struct AppAttributes { }
    
    private readonly AppAttributes appParams = new AppAttributes();

    [NonSerialized] private CharacterListConfig characterListConfig;

    private void GetRemoteConfigSettings()
    {
        ConfigManager.FetchCompleted += ParseResponse;
        ConfigManager.FetchConfigs(new UserAttributes(), appParams);
    }

    private void Awake()
    {
        GetRemoteConfigSettings();
    }

    private void ParseResponse(ConfigResponse response)
    {
        ConfigManager.FetchCompleted -= ParseResponse;

        if (ConfigManager.requestStatus != ConfigRequestStatus.Success)
        {
            StartCoroutine(DisplayError());
            return;
        }

        LoadConfig(GetCharacterListConfigForJson(GetAppConfigJson()));
    }

    private void LoadConfig(CharacterListConfig characterListConfig)
    {
        MessageCenter.InvokeCharacterListReceived(characterListConfig);
        loadingText.StopAnimation();
    }
    
    private IEnumerator DisplayError()
    {
        CharacterListConfig characterListConfig = GetCharacterListConfigForJson(GetAppConfigJson()) ??
                                                  GetCharacterListConfigForJson(fallbackCharacterListConfig.text);

        yield return StartCoroutine(loadingText.DisplayError());
        LoadConfig(characterListConfig);
    }
    
    private CharacterListConfig GetCharacterListConfigForJson(string json)
    {
        return JsonConvert.DeserializeObject<CharacterListConfig>(json);
    }

    private string GetAppConfigJson()
    {
        return ConfigManager.appConfig.GetJson(RemoteConfigKeys.CHARACTER_LIST_KEY);
    }
}
