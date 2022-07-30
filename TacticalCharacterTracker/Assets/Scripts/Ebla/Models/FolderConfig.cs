using System;
using System.Collections.Generic;
using Ebla.Libraries;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebla.Models
{
    [Serializable]
    public class FolderConfig : BaseConfig
    {
        public static event Action<FolderConfig> OnLoadIntoFolder;
        
        public override string BaseName => "untitled folder";

        [JsonIgnore]
        public List<BaseConfig> Configs { get; } = new();

        public override void InvokeLoadIntoFolder()
        {
            OnLoadIntoFolder?.Invoke(this);
        }
        
        public void AddConfigToFolder(BaseConfig config)
        {
            Configs.Add(config);
            config.OnConfigRemoved += HandleConfigRemoved;
            config.UpdateParent(this);
            InvokeConfigModified();
        }
        
        protected override void RemoveConfigFromLibrary()
        {
            FolderLibrarian.Instance.Remove(this);
        }

        protected override void HandleDeserialization()
        {
            base.HandleDeserialization();
            Debug.Log("Deserializing folder, registering");
            ScopeController.Instance.RegisterFolder(this);
        }

        private void HandleConfigRemoved(BaseConfig baseConfig)
        {
            baseConfig.OnConfigRemoved -= HandleConfigRemoved;
            Configs.Remove(baseConfig);
        }
    }
}
