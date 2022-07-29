using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Ebla.Libraries;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebla.Models
{
    public class FolderConfig : BaseConfig
    {
        public override string BaseName => "untitled folder";

        [JsonIgnore]
        public List<BaseConfig> Configs { get; } = new();

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

        protected override void HandleDeserialization()
        {
            base.HandleDeserialization();
            Debug.Log("Deserializing folder, registering");
            ScopeController.Instance.RegisterFolder(this);
        }
    }
}
