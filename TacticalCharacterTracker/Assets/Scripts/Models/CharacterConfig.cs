using System;
using System.Collections.Generic;

[Serializable]
public class CharacterConfig
{
    public CharacterConfig(string _name = null)
    {
        name = _name; 
        abilities = new List<AbilityConfig>();
    }
    
    public string name;
    public string nameButtonColor;
    
    public int defense;
    public int health;
    public int speed;
    
    public List<AbilityConfig> abilities;
}

[Serializable]
public class CharacterListConfig
{
    public List<CharacterConfig> characters;

    public CharacterListConfig()
    {
        characters = new List<CharacterConfig>();
    }
}

