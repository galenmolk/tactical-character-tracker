using System;
using Ebla.LibraryControllers;

namespace Ebla.Models
{
    [Serializable]
    public class EnemyConfig : CharacterConfig
    {
        public override string BaseName => "Untitled Enemy";
        protected override void RemoveConfigFromLibrary()
        {
            EnemyLibrarian.Instance.Remove(this);
        }
    }
}
