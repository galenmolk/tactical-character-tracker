using System;
using HexedHeroes.Utils;
using Newtonsoft.Json;

namespace HexedHeroes.Creator
{
    [Serializable]
    public class EnemyTypeConfig
    {
        // public EnemyConfig(string name = null)
        // {
        //     character = new CharacterConfig(name);
        //     quantity = DEFAULT_QUANTITY;
        // }
    
        public EnemyTypeConfig(CharacterConfig type)
        {
            character = type;
            quantity = DEFAULT_QUANTITY;
        }
    
        [JsonIgnore] 
        public const int DEFAULT_QUANTITY = 1;
    
        [JsonProperty(ConfigKeys.ENEMY_TYPE_CHARACTER_KEY)]
        public CharacterConfig character;
    
        [JsonProperty(ConfigKeys.ENEMY_TYPE_QUANTITY_KEY)]
        public int quantity;
    }
}
