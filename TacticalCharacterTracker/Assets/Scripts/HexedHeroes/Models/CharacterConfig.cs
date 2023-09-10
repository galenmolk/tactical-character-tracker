using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using HexedHeroes.Models;
using HexedHeroes.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace HexedHeroes
{
    [Serializable]
    public class CharacterConfig
    {
        public const string UNTITLED_NAME_PREFIX = "Untitled Character #";
    
        public CharacterConfig(string _name = null)
        {
            name = _name; 
            abilityIds = new List<string>();
        }

        public CharacterConfig()
        {
            name = null;
            colorHex = null;
            defense = 0;
            health = 0;
            speed = 0;
            abilityIds = null;
        }

        [JsonProperty("id")]
        public string id;

        [JsonProperty(ConfigKeys.CHARACTER_NAME_KEY)]
        public string name;
    
        [JsonProperty(ConfigKeys.CHARACTER_COLOR_HEX_KEY)]
        public string colorHex;

        [JsonProperty(ConfigKeys.CHARACTER_DEFENSE_KEY)]
        public int defense;
    
        [JsonProperty(ConfigKeys.CHARACTER_HEALTH_KEY)]
        public int health;
    
        [JsonProperty(ConfigKeys.CHARACTER_SPEED_KEY)]
        public int speed;
    
        [JsonProperty(ConfigKeys.CHARACTER_ABILITY_IDS_KEY)]
        public List<string> abilityIds;

        public List<AbilityConfig> abilities { get; set; }
    }
}
