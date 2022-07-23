using System;

namespace Ebla.Models
{
    [Serializable]
    public class HeroConfig : CharacterConfig
    {
        public override string BaseName => "Untitled Hero";
        protected override void RemoveConfigFromLibrary()
        {
            throw new NotImplementedException();
        }
    }
}
