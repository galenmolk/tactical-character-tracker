using System.Collections.Generic;
using System.IO;
using HexedHeroes.Utils;
using Newtonsoft.Json;
using Unity.RemoteConfig;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public List<CharacterConfig> Characters { get; private set; }
    public List<DungeonConfig> Dungeons { get; private set; }
    public List<AbilityConfig> Abilities { get; private set; }

    [SerializeField] private TextAsset fallbackCharacters;
    [SerializeField] private TextAsset fallbackDungeons;
    [SerializeField] private TextAsset fallbackAbilities;

    private struct UserAttributes { }
    private struct AppAttributes { }
    private readonly AppAttributes appParams = new();

    public void SaveCharacters()
    {
        var charactersJson = JsonConvert.SerializeObject(Characters);
        fallbackCharacters = new TextAsset(charactersJson);
    }
    
    private void Awake()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            LoadConfigs();
            return;
        }
        
        GetRemoteConfigs();
    }
    
    private void GetRemoteConfigs()
    {
        ConfigManager.FetchCompleted += ParseResponse;
        ConfigManager.FetchConfigs(new UserAttributes(), appParams);
    }
    
    private void ParseResponse(ConfigResponse response)
    {
        ConfigManager.FetchCompleted -= ParseResponse;
        LoadConfigs();
    }

    private void LoadConfigs()
    {
        Debug.Log("parsing");
        Characters = GetCharacters();
        CharacterDisplay.Instance.DisplayCharacters(Characters);
        
        //Dungeons = GetDungeons();
        //Abilities = GetAbilities();
    }

    private List<CharacterConfig> GetCharacters()
    {
        return JsonConvert.DeserializeObject<List<CharacterConfig>>(GetCharacterListJson()); 
    }
    
    private string GetCharacterListJson()
    {
        return /*ConfigManager.appConfig.GetJson(RemoteConfigKeys.HERO_LIST_KEY) ?? */ fallbackCharacters.text;
    }
    
    private List<DungeonConfig> GetDungeons()
    {
        return JsonConvert.DeserializeObject<List<DungeonConfig>>(GetDungeonListJson()); 
    }

    private string GetDungeonListJson()
    {
        return ConfigManager.appConfig.GetJson(RemoteConfigKeys.DUNGEON_LIST_KEY) ?? fallbackDungeons.text;
    }
    
    private List<AbilityConfig> GetAbilities()
    {
        return JsonConvert.DeserializeObject<List<AbilityConfig>>(GetAbilityListJson()); 
    }

    private string GetAbilityListJson()
    {
        return ConfigManager.appConfig.GetJson(RemoteConfigKeys.ABILITY_LIST_KEY) ?? fallbackAbilities.text;
    }
}
