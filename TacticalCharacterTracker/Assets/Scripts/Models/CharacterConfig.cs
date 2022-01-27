using System;

[Serializable]
public class CharacterConfig
{
    public string name;
    
    public int defense;
    public int health;
    public int speed;
    
    public PassiveAbilityConfig[] passiveAbilities;
    public CooldownAbilityConfig[] cooldownAbilities;
}