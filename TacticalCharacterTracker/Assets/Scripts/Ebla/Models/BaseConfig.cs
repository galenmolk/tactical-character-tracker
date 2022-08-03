using System;
using System.Runtime.Serialization;
using Ebla.UI;
using Ebla.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebla.Models
{
    public abstract class BaseConfig
    {
        public event Action OnConfigModified;
        public event Action<BaseConfig> OnConfigRemoved;

        public abstract string BaseName { get; }

        [JsonProperty(ConfigKeys.NAME_KEY)]
        public string Name { get; protected set; }
        
        [JsonProperty(ConfigKeys.DESCRIPTION_KEY)]
        public string Description { get; set; }
        
        [JsonProperty(ConfigKeys.ID_KEY)]
        public string Id { get; private set; }

        [JsonProperty(ConfigKeys.PATH_KEY)]
        public string Path { get; protected set; }

        [JsonIgnore]
        public string FullPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Path))
                {
                    return Name;
                }
                
                return Path + PathUtils.PATH_DELIMITER + Name;
            }
        }
        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; private set; }

        [JsonIgnore]
        public FolderConfig Parent { get; private set; }

        public virtual void InvokeLoadIntoFolder()
        {
            Debug.LogWarning("InvokeLoadIntoFolder Not implemented for this config type");
        }
        
        public void Initialize()
        {
            ScopeController.Instance.CurrentFolder.AddConfig(this);
            AssignDates();
            Path = PathUtils.GetPathToFolder(Parent);
            this.AssignUniqueName();
        }

        public void GetParentFromPath()
        {
            Debug.Log($"{Name} GetParentFromPath {Path}");
            if (ScopeController.Instance.TryGetFolderForPath(Path, out FolderConfig folderConfig))
            {
                Debug.Log($"{Name} Parent Found: {folderConfig.Name}");
                folderConfig.AddConfig(this);
            }
            else
            {
                Debug.Log($"{Name} Parent NOT FOUND FOR PATH : {Path}");
            }
        }
        
        public void UpdateName(string newName)
        {
            Name = newName;
            InvokeConfigModified();
        }

        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
            InvokeConfigModified();
        }

        public void UpdateParent(FolderConfig folderConfig)
        {
            Parent = folderConfig;
            InvokeConfigModified();
        }

        public virtual void TryDeleteConfig()
        {
            ConfirmationController.Instance.DeleteConfig(this);
        }
        
        public void DeleteConfig()
        {
            Debug.Log($"Base Config Delete Config {Name}");

            RemoveConfigFromLibrary();
            OnConfigRemoved?.Invoke(this);
        }

        public virtual string GetDeletionText()
        {
            return $"Delete {Name}?";
        }

        protected abstract void RemoveConfigFromLibrary();

        protected void InvokeConfigModified()
        {
            OnConfigModified?.Invoke();
        }

        [OnSerialized]
        private void SerializedCallback(StreamingContext context)
        {
            Identify();
        }
        
        [OnDeserialized]
        private void DeserializedCallback(StreamingContext context)
        {
            Identify();
            HandleDeserialization();
        }

        protected virtual void HandleDeserialization()
        {
            
        }

        private void Identify()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = NCuid.Cuid.Generate();
            }
        }

        private void AssignDates()
        {
            DateCreated = DateTime.Now;
            DateModified = DateCreated;
            OnConfigModified += HandleConfigModified;
        }

        private void HandleConfigModified()
        {
            DateModified = DateTime.Now;
        }
    }
}
