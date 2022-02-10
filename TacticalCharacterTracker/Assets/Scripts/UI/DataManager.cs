using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.RemoteConfig;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public List<CharacterConfig> Heroes { get; private set; }
    public CharacterListConfig Enemies { get; private set; }
    public List<DungeonConfig> Dungeons { get; private set; }
    public List<AbilityConfig> Abilities { get; private set; }

    [SerializeField] private TextAsset fallbackHeroes;
    [SerializeField] private TextAsset fallbackEnemies;
    [SerializeField] private TextAsset fallbackDungeons;
    [SerializeField] private TextAsset fallbackAbilities;

    private struct UserAttributes { }
    private struct AppAttributes { }
    private readonly AppAttributes appParams = new();

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
        //Heroes = GetHeroes();
        Enemies = GetEnemies();
        //Dungeons = GetDungeons();
        //Abilities = GetAbilities();
    }

    private List<CharacterConfig> GetHeroes()
    {
        return JsonConvert.DeserializeObject<List<CharacterConfig>>(GetHeroListJson()); 
    }

    private string GetHeroListJson()
    {
        return ConfigManager.appConfig.GetJson(RemoteConfigKeys.HERO_LIST_KEY) ?? fallbackHeroes.text;
    }
    
    private CharacterListConfig GetEnemies()
    {
        return JsonConvert.DeserializeObject<CharacterListConfig>(GetEnemyListJson()); 
    }

    private string GetEnemyListJson()
    {
        Debug.Log(fallbackEnemies.text);
        return fallbackEnemies.text;
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
