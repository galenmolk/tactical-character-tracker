using System;

namespace Ebla.Models
{
    [Serializable]
    public class HeroConfig : CharacterConfig
    {
        public override Type ConfigType => Type.Hero;

        public override string BaseName => "Untitled Hero";
    }
}
