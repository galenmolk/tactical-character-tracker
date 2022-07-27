using System;
using System.Collections.Generic;
using System.IO;

namespace Ebla.Models
{
    [Serializable]
    public class FolderConfig : BaseConfig
    {
        public FolderConfig(string path) : base(ScopeController.GetLastNameFromPath(path))
        {
            Path = path;
        }
        
        public override string BaseName => "untitled folder";

        public List<BaseConfig> Files { get; private set; } = new();

        public void AddFile(BaseConfig config)
        {
            Files.Add(config);
            config.UpdateParent(this);
            InvokeConfigModified();
        }
        
        protected override void RemoveConfigFromLibrary()
        {
            throw new NotImplementedException();
        }
    }
}
