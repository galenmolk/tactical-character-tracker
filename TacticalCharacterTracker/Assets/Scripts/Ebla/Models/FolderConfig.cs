using System;

namespace Ebla.Models
{
    [Serializable]
    public class FolderConfig : BaseConfig
    {
        public FolderConfig(string name) : base(name)
        {
            
        }
        
        public override string BaseName => "untitled folder";

        public FolderConfig ParentFolder { get; set; }

        protected override void RemoveConfigFromLibrary()
        {
            throw new NotImplementedException();
        }
    }
}
