using System;

namespace Ebla.Models
{
    [Serializable]
    public class AbilityConfig : BaseConfig
    {
        public override string BaseName => "Untitled Ability";

        public int CooldownTurns { get; private set; }
        public bool IsPassive { get; private set; }
        public bool IsInterrupt { get; private set; }

        public AbilityConfig()
        {

        }

        public void UpdateCooldownTurns(int cooldownTurns)
        {
            CooldownTurns = cooldownTurns;
            InvokeConfigModified();
        }
        
        public void UpdateIsPassive(bool isPassive)
        {
            IsPassive = isPassive;
            InvokeConfigModified();
        }

        public void UpdateIsInterrupt(bool isInterrupt)
        {
            IsInterrupt = isInterrupt;
            InvokeConfigModified();
        }
    }
}
