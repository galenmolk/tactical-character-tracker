using System;

[Serializable]
public class CooldownAbilityConfig : AbilityConfig
{
    public int cooldown;
    public bool isInterrupt;
    
    private const string TURNS_PLURAL = "Turns";
    private const string TURNS_SINGULAR = "Turn";
    
    private string GetTurnText(int _cooldown)
    {
        return _cooldown == 1 ? TURNS_SINGULAR : TURNS_PLURAL;
    }
    
    public override string GetCooldownDescription()
    {
        return $"{cooldown} {GetTurnText(cooldown)}";
    }

    public string GetCurrentCooldownDescription(int currentCooldown)
    {
        return $"{currentCooldown} {GetTurnText(currentCooldown)} Left";
    }
}
