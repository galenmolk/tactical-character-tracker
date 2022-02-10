using System;
using Newtonsoft.Json;

[Serializable]
public class EnemyConfig
{
    // public EnemyConfig(string name = null)
    // {
    //     characterType = new CharacterConfig(name);
    //     quantity = DEFAULT_QUANTITY;
    // }
    
    public EnemyConfig(CharacterConfig type)
    {
        characterType = type;
        quantity = DEFAULT_QUANTITY;
    }
    
    [JsonIgnore] public const int DEFAULT_QUANTITY = 1;
    
    public CharacterConfig characterType;
    public int quantity;
}
