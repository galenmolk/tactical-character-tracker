using System;
using System.Runtime.Serialization;
using HexedHeroes.Utils;
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
        isInterrupt = false;
    }
    
    public const string PASSIVE_TEXT = "[Passive]";
    
    [JsonProperty(ConfigKeys.ABILITY_COOLDOWN_KEY)]
    public int cooldown;

    [JsonProperty(ConfigKeys.ABILITY_IS_INTERRUPT_KEY)]
    public bool isInterrupt;
    
    [JsonProperty(ConfigKeys.ABILITY_IS_PASSIVE_KEY)]
    public bool isPassive;
    
    private const string TURNS_PLURAL = "Turns";
    private const string TURNS_SINGULAR = "Turn";
    
    [JsonProperty(ConfigKeys.ABILITY_NAME_KEY)]
    public string name;
    
    [JsonProperty(ConfigKeys.ABILITY_DESCRIPTION_KEY)]
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
