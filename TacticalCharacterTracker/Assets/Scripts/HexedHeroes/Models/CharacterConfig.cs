using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class CharacterConfig
{
    public const string UNTITLED_NAME_PREFIX = "Untitled Character #";
    
    public CharacterConfig(string _name = null)
    {
        name = _name; 
        abilities = new List<AbilityConfig>();
    }

    public CharacterConfig()
    {
        name = null;
        nameButtonColor = null;
        defense = 0;
        health = 0;
        speed = 0;
        abilities = null;
    }
    
    [JsonProperty(ConfigKeys.CHARACTER_NAME_KEY)]
    public string name;
    
    [JsonProperty(ConfigKeys.CHARACTER_NAME_BUTTON_COLOR_KEY)]
    public string nameButtonColor;

    [JsonProperty(ConfigKeys.CHARACTER_DEFENSE_KEY)]
    public int defense;
    
    [JsonProperty(ConfigKeys.CHARACTER_HEALTH_KEY)]
    public int health;
    
    [JsonProperty(ConfigKeys.CHARACTER_SPEED_KEY)]
    public int speed;
    
    [JsonProperty(ConfigKeys.CHARACTER_ABILITIES_KEY)]
    public List<AbilityConfig> abilities;
}
