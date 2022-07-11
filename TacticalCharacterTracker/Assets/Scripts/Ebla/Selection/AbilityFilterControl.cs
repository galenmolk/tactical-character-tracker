using Ebla.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.Selection
{
    public class AbilityFilterControl : FilterControl<AbilityConfig>
    {
        public int Cooldown => int.Parse(cooldownFilterInputField.text);
        public bool IsPassive => passiveFilterToggle.isOn;
        public bool IsInterrupt => interruptFilterToggle.isOn;
        
        [SerializeField] private TMP_InputField cooldownFilterInputField;
        [SerializeField] private Toggle passiveFilterToggle;
        [SerializeField] private Toggle interruptFilterToggle;

        public override bool IsValid(AbilityConfig config)
        {
            Debug.Log("AbilityFilter Is Valid");
            if (!base.IsValid(config))
                return false;

            Debug.Log("AbilityFilter Name Is Valid");

            bool isCooldownValid = string.IsNullOrWhiteSpace(cooldownFilterInputField.text) ||
                                   Cooldown == config.CooldownTurns;

            Debug.Log("AbilityFilter isCooldownValid: " + isCooldownValid);

            
            bool isPassiveValid = IsPassive == config.IsPassive;
            Debug.Log("AbilityFilter isPassiveValid: " + isPassiveValid);

            bool isInterruptValid = IsInterrupt == config.IsInterrupt;
            Debug.Log("AbilityFilter isInterruptValid: " + isInterruptValid);

            return isCooldownValid && isPassiveValid && isInterruptValid;
        }
    }
}
