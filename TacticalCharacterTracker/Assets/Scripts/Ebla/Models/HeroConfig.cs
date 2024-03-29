using System;
using Ebla.Libraries;
using JetBrains.Annotations;

namespace Ebla.Models
{
    public class HeroConfig : CharacterConfig
    {
        [UsedImplicitly]
        public static event Action<HeroConfig> OnLoadIntoFolder;
        
        public override string BaseName => "Untitled Hero";
        protected override void RemoveConfigFromLibrary()
        {
            HeroLibrarian.Instance.Remove(this);
        }
    }
}
