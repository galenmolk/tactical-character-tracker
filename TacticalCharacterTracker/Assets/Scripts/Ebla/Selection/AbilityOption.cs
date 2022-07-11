using System;
using Ebla.Models;
using Ebla.Utils;
using TMPro;
using UnityEngine;

namespace Ebla.Selection
{
    public class AbilityOption : BaseOption<AbilityConfig, AbilityOption>
    {
        [SerializeField] private TMP_Text cooldownText;
        [SerializeField] private TMP_Text passiveText;
        [SerializeField] private TMP_Text interruptText;

        public override event Action<AbilityOption> OnReleaseObject;
        
        public override void ReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }

        protected override void PopulateOption()
        {
            cooldownText.text = Config.CooldownTurns.ToString();
            passiveText.text = OptionUtils.GetStringForBool(Config.IsPassive);
            interruptText.text = OptionUtils.GetStringForBool(Config.IsInterrupt);
        }
    }
}
