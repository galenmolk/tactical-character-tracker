using System.Runtime.Serialization;
using Newtonsoft.Json;

public abstract class AbilityConfig
{
    public string name;
    public string description;

    [JsonIgnore]
    public string colorCodedDescription;
    
    public abstract string GetCooldownDescription();
    public abstract string GetCurrentCooldownDescription(int currentCooldown);
    
    [OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
        if (string.IsNullOrWhiteSpace(description))
            return;

        colorCodedDescription = ColorCoder.GetColorCodedText(description);
    }
}
