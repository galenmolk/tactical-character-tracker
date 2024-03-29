using System;
using System.Collections.Generic;
using Ebla.Libraries;
using Ebla.UI;
using MolkExtras;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebla.Models
{
    [Serializable]
    public class FolderConfig : BaseConfig
    {
        public static event Action<FolderConfig> OnLoadIntoFolder;

        public FolderConfig()
        {
            
        }
        
        // For Root Folder.
        public FolderConfig(string name)
        {
            Name = name;
        }
        
        public override string BaseName => "untitled folder";
        
        [JsonIgnore]
        public List<BaseConfig> Configs
        {
            get => configs;

            set => configs = value;
        }

        private List<BaseConfig> configs = new();

        public override void InvokeLoadIntoFolder()
        {
            OnLoadIntoFolder?.Invoke(this);
        }
        
        public void AddConfig(BaseConfig config)
        {
            Configs.Add(config);
            config.OnConfigRemoved += HandleConfigRemoved;
            config.UpdateParent(this);
            InvokeConfigModified();
        }

        public override void TryDeleteConfig()
        {
            ConfirmationController.Instance.DeleteFolder(this);
        }

        public override string GetDeletionText()
        {
            int configCount = 0;
            int folderCount = 0;

            for (int i = 0, count = Configs.Count; i < count; i++)
            {
                if (Configs[i] is FolderConfig)
                {
                    folderCount++;
                }
                else
                {
                    configCount++;
                }
            }

            string configKeyword = StringUtils.GetCountDistinction("file", configCount);
            string folderKeyword = StringUtils.GetCountDistinction("folder", folderCount);
            string contentsString = $"({folderCount} {folderKeyword}, {configCount} {configKeyword})";
            return $"{base.GetDeletionText()} \n{contentsString}";
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
