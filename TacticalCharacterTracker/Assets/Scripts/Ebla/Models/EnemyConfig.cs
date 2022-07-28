using System;
using Ebla.Libraries;

namespace Ebla.Models
{
    [Serializable]
    public class EnemyConfig : CharacterConfig
    {
        public EnemyConfig(FolderConfig parent) : base(parent)
        {
            
        }
        
        public override string BaseName => "Untitled Enemy";
        protected override void RemoveConfigFromLibrary()
        {
            EnemyLibrarian.Instance.Remove(this);
        }
    }
}
