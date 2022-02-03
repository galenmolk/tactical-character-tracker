using System;
using System.Collections.Generic;

[Serializable]
public class CharacterConfig
{
    public CharacterConfig()
    {
        abilities = new List<AbilityConfig>();
    }
    
    public string name;
    public string nameButtonColor;
    
    public int defense;
    public int health;
    public int speed;
    
    public List<AbilityConfig> abilities;
}
