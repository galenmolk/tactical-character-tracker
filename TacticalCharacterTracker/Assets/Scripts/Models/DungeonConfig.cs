using System;
using System.Collections.Generic;

[Serializable]
public class DungeonConfig
{
    public const string UNTITLED_NAME_PREFIX = "Untitled Dungeon #";
    
    public string name;
    public List<EnemyConfig> enemies;

    private CharacterListConfig enemyTypes = new CharacterListConfig();
    
    public DungeonConfig(string _name)
    {
        name = _name;
        enemies = new List<EnemyConfig>();
    }

    public CharacterListConfig GetEnemyTypes()
    {
        enemyTypes.characters.Clear();

        foreach (var enemyConfig in enemies)
        {
            enemyTypes.characters.Add(enemyConfig.characterType);
        }

        return enemyTypes;
    }
}
