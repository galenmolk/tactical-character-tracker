using System;
using Ebla.Libraries;
using JetBrains.Annotations;

namespace Ebla.Models
{
    public class EnemyConfig : CharacterConfig
    {
        [UsedImplicitly]
        public static event Action<EnemyConfig> OnLoadIntoFolder;

        public override string BaseName => "Untitled Enemy";

        protected override void RemoveConfigFromLibrary()
        {
            EnemyLibrarian.Instance.Remove(this);
        }
    }
}
