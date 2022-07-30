using System;
using Ebla.Libraries;

namespace Ebla.Models
{
    public class HeroConfig : CharacterConfig
    {
        public static event Action<HeroConfig> OnLoadIntoFolder;
        
        public override string BaseName => "Untitled Hero";
        protected override void RemoveConfigFromLibrary()
        {
            HeroLibrarian.Instance.Remove(this);
        }
    }
}
