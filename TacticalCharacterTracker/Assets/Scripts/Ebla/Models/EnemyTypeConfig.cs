using System;
using System.Collections.Generic;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    [Serializable]
    public class EnemyTypeConfig : BaseConfig
    {
        public EnemyTypeConfig()
        {
            Enemy = new EnemyConfig();
        }
            
        [JsonProperty(ConfigKeys.ENEMY_KEY)]
        public EnemyConfig Enemy { get; private set; }
        
        public override string BaseName => "Untitled Enemy Type";

        public override string GetDeletionText()
        {
            return $"Delete all enemy instances of type \"{Enemy.Name}\"?";
        }
    }
}
