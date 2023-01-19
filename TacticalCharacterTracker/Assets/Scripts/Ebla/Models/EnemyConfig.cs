using System;
using System.Collections.Generic;
using Ebla.Libraries;
using JetBrains.Annotations;

namespace Ebla.Models
{
    public class EnemyConfig : CharacterConfig, ICloneable
    {
        [UsedImplicitly]
        public static event Action<EnemyConfig> OnLoadIntoFolder;

        public override string BaseName => "Untitled Enemy";

        public object Clone()
        {
            return new EnemyConfig(this);
        }

        public EnemyConfig()
        {

        }

        public EnemyConfig(EnemyConfig character)
        {
            Name = character.Name;
            Health = character.Health;
            Defense = character.Defense;
            Speed = character.Speed;

            Abilities = new List<AbilityConfig>();

            foreach (AbilityConfig ability in character.Abilities)
            {
                Abilities.Add((AbilityConfig)ability.Clone());
            }

            CharacterInstances = new List<CharacterInstanceConfig>();

            foreach (CharacterInstanceConfig instance in character.CharacterInstances)
            {
                CharacterInstances.Add((CharacterInstanceConfig)instance.Clone());
            }
        }

        protected override void RemoveConfigFromLibrary()
        {
            EnemyLibrarian.Instance.Remove(this);
        }
    }
}
