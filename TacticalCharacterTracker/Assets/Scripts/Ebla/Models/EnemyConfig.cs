using System;
using Ebla.Libraries;

namespace Ebla.Models
{
    public class EnemyConfig : CharacterConfig
    {
        public override string BaseName => "Untitled Enemy";
        protected override void RemoveConfigFromLibrary()
        {
            EnemyLibrarian.Instance.Remove(this);
        }
    }
}
