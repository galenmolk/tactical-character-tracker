using System;
using Ebla.Libraries;

namespace Ebla.Models
{
    public class EnemyConfig : CharacterConfig
    {
        public static event Action<EnemyConfig> OnLoadIntoFolder;

        public override string BaseName => "Untitled Enemy";
        protected override void RemoveConfigFromLibrary()
        {
            EnemyLibrarian.Instance.Remove(this);
        }
    }
}
