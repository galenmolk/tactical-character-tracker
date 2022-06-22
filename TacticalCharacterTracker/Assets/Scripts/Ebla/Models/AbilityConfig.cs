using System;

namespace Ebla.Models
{
    [Serializable]
    public class AbilityConfig : BaseConfig
    {
        public override string BaseName => "Untitled Ability";
        
        public AbilityConfig()
        {

        }

        public int cooldown;
    }
}
