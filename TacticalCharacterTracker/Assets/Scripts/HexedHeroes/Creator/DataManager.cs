using System.Collections.Generic;
using System.IO;
using HexedHeroes.Creator;
using HexedHeroes.Utils;
using Newtonsoft.Json;
using Unity.RemoteConfig;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public delegate void AbilityListEvent(List<AbilityConfig> abilityConfigs);

    public static event AbilityListEvent OnAbilitiesParsed;
    
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
        Characters = GetCharacters();
        Abilities = new List<AbilityConfig>();
        GetAbilitiesFromCharacters();
        CharacterDisplay.Instance.DisplayCharacters(Characters);
    }

    private List<CharacterConfig> GetCharacters()
    {
        return JsonConvert.DeserializeObject<List<CharacterConfig>>(GetCharacterListJson()); 
    }
    
    private string GetCharacterListJson()
    {
        return /*ConfigManager.appConfig.GetJson(RemoteConfigKeys.HERO_LIST_KEY) ?? */ fallbackCharacters.text;
    }

    private void GetAbilitiesFromCharacters()
    {
        foreach (var characterConfig in Characters)
        {
            foreach (var abilityConfig in characterConfig.abilities)
            {
                if (!Abilities.Contains(abilityConfig))
                    Abilities.Add(abilityConfig);
            }
        }
        
        OnAbilitiesParsed?.Invoke(Abilities);
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
