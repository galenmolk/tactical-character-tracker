using System;
using System.Collections.Generic;

[Serializable]
public class DungeonConfig
{
    public const string UNTITLED_NAME_PREFIX = "Untitled Dungeon #";
    
    public string name;
    public List<EnemyConfig> enemies;
    
    public DungeonConfig(string _name)
    {
        name = _name;
        enemies = new List<EnemyConfig>();
    }
}
