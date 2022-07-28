using System;
using System.Collections.Generic;
using Ebla.Libraries;
using Newtonsoft.Json;

namespace Ebla.Models
{
    [Serializable]
    public class FolderConfig : BaseConfig
    {
        public FolderConfig()
        {
            
        }
        
        public FolderConfig(FolderConfig parent) : base(parent)
        {
            
        }
        
        public override string BaseName => "untitled folder";

        [JsonIgnore]
        public List<BaseConfig> Configs { get; private set; } = new();

        public void AddConfigToFolder(BaseConfig config)
        {
            Configs.Add(config);
            config.UpdateParent(this);
            InvokeConfigModified();
        }
        
        protected override void RemoveConfigFromLibrary()
        {
            FolderLibrarian.Instance.Remove(this);
        }
    }
}
