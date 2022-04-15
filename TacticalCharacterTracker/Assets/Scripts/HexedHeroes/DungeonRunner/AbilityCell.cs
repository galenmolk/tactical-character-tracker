using System;

namespace HexedHeroes.DungeonRunner
{
    public class AbilityCell : NumberCell
    {
        private AbilityConfig config;

        private bool isCooldownActive;
        
        public void SetAbility(AbilityConfig abilityConfig)
        {
            config = abilityConfig;
            
            SetInt(config.cooldown);
        }

        public void Activate()
        {
            isCooldownActive = true;
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }
    }
}
