using System;

namespace Ebla.Models
{
    [Serializable]
    public class FolderConfig : BaseConfig
    {
        public string colorHex;
        public override string BaseName => "Untitled Folder";
        protected override void RemoveConfigFromLibrary()
        {
            throw new NotImplementedException();
        }
    }
}
