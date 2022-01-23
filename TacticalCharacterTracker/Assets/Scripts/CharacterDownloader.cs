using Newtonsoft.Json;
using Unity.RemoteConfig;
using UnityEngine;

public class CharacterDownloader : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }
    
    private appAttributes appParams = new appAttributes();

    private CharacterListConfig characterListConfig;
    
    public void GetRemoteConfigSettings()
    {
        ConfigManager.FetchCompleted += LoadCharacters;
        ConfigManager.FetchConfigs(new userAttributes(), appParams);
    }

    private void LoadCharacters(ConfigResponse response)
    {
        string characterListJson = ConfigManager.appConfig.GetJson(RemoteConfigKeys.CHARACTER_LIST_KEY);
        characterListConfig = JsonConvert.DeserializeObject<CharacterListConfig>(characterListJson);
        MessageCenter.InvokeCharacterListReceived(characterListConfig);
        ConfigManager.FetchCompleted -= LoadCharacters;
    }
}
