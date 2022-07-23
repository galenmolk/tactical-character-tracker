using Ebla.Editing.Sections;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Editing
{
    public class AbilityControls : EditingControls<AbilityConfig>
    {
        [SerializeField] private TickerSection cooldownSection;
        [SerializeField] private BoolSection passiveSection;
        [SerializeField] private BoolSection interruptSection;

        public void TryUpdateCooldownTurns(int cooldownTurns)
        {
            if (cooldownTurns != ActiveConfig.CooldownTurns)
                ActiveConfig.UpdateCooldownTurns(cooldownTurns);
        }
        
        public void TryUpdateIsPassive(bool isPassive)
        {
            if (isPassive != ActiveConfig.IsPassive)
                ActiveConfig.UpdateIsPassive(isPassive);
        }

        public void TryUpdateIsInterrupt(bool isInterrupt)
        {
            if (isInterrupt != ActiveConfig.IsInterrupt)
                ActiveConfig.UpdateIsInterrupt(isInterrupt);
        }
        
        protected override void ApplyConfig(AbilityConfig config)
        {
            base.ApplyConfig(config);

            passiveSection.TrySetValue(config.IsPassive);
            cooldownSection.TrySetValue(config.CooldownTurns);
            interruptSection.TrySetValue(config.IsInterrupt);

            cooldownSection.gameObject.SetActive(!config.IsPassive);
        }
    }
}
