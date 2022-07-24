using System;
using Ebla.Libraries;

namespace Ebla.Models
{
    [Serializable]
    public class HeroConfig : CharacterConfig
    {
        public override string BaseName => "Untitled Hero";
        protected override void RemoveConfigFromLibrary()
        {
            HeroLibrarian.Instance.Remove(this);
        }
    }
}
