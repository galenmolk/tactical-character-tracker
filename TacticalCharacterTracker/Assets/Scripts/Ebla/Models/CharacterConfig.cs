using System;
using System.Collections.Generic;

namespace Ebla.Models
{
    public abstract class CharacterConfig : BaseConfig
    {
        public int Health { get; private set; }
        public int Defense { get; private set; }
        public int Speed { get; private set; }
        public List<AbilityConfig> Abilities { get; private set; }

        public void UpdateHealth(int health)
        {
            Health = health;
            InvokeConfigModified();
        }

        public void UpdateDefense(int defense)
        {
            Defense = defense;
            InvokeConfigModified();
        }

        public void UpdateSpeed(int speed)
        {
            Speed = speed;
            InvokeConfigModified();
        }

        public void UpdateAbilities(List<AbilityConfig> abilityConfigs)
        {
            Abilities = abilityConfigs;
            InvokeConfigModified();
        }
    }
}
