using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

[Serializable]
public class AbilityConfig
{
    public AbilityConfig()
    {
        name = string.Empty;
        description = string.Empty;
        cooldown = 0;
        isPassive = false;
    }
    
    public const string PASSIVE_TEXT = "[Passive]";
    
    public int cooldown;
    public bool isInterrupt;
    public bool isPassive;
    
    private const string TURNS_PLURAL = "Turns";
    private const string TURNS_SINGULAR = "Turn";
    
    public string name;
    public string description;

    [JsonIgnore]
    public string colorCodedDescription;
    
    [OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
        if (string.IsNullOrWhiteSpace(description))
            return;

        colorCodedDescription = ColorCoder.GetColorCodedText(description);
    }
    
    private string GetTurnText(int _cooldown)
    {
        return _cooldown == 1 ? TURNS_SINGULAR : TURNS_PLURAL;
    }
    
    public string GetCooldownDescription()
    {
        return isPassive ? PASSIVE_TEXT : $"{cooldown} {GetTurnText(cooldown)}";
    }

    public string GetCurrentCooldownDescription(int currentCooldown)
    {
        return isPassive ? PASSIVE_TEXT :  $"{currentCooldown} {GetTurnText(currentCooldown)} Left";
    }
}
