using System;
using System.Collections.Generic;
using HexedHeroes.Utils;
using Newtonsoft.Json;

[Serializable]
public class DungeonConfig
{
    public const string UNTITLED_NAME_PREFIX = "Untitled Dungeon #";
    
    [JsonProperty(ConfigKeys.DUNGEON_NAME_KEY)]
    public string name;
    
    [JsonProperty(ConfigKeys.DUNGEON_ENEMY_TYPES_KEY)]
    public List<EnemyTypeConfig> enemyTypes;

    private List<CharacterConfig> enemyTypeCharacters = new List<CharacterConfig>();
    
    public DungeonConfig(string _name)
    {
        name = _name;
        enemyTypes = new List<EnemyTypeConfig>();
    }

    public List<CharacterConfig> GetEnemyTypes()
    {
        enemyTypeCharacters.Clear();

        foreach (var enemyConfig in enemyTypes)
        {
            enemyTypeCharacters.Add(enemyConfig.character);
        }

        return enemyTypeCharacters;
    }
}
