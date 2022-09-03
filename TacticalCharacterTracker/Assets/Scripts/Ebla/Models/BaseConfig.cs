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
        public static event Action OnAnyConfigModified;
        
        public event Action<BaseConfig> OnConfigModified;
        public event Action<BaseConfig> OnConfigRemoved;

        [JsonIgnore] public virtual string BaseName => string.Empty;

        [JsonProperty(ConfigKeys.NAME_KEY)]
        public string Name { get; protected set; }
        
        [JsonProperty(ConfigKeys.DESCRIPTION_KEY)]
        public string Description { get; set; } = string.Empty;
        
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

        [JsonIgnore] public FolderConfig Parent { get; private set; }
        
        public void SetNameSilent(string newName)
        {
            Name = newName;
        }
        
        public virtual void InvokeLoadIntoFolder()
        {
            Debug.LogWarning("InvokeLoadIntoFolder Not implemented for this config type");
        }

        public string GetIndexedBaseName(int index)
        {
            return $"{BaseName} {index}";
        }
        
        public void Initialize()
        {
            Identify();
            ScopeController.Instance.CurrentFolder.AddConfig(this);
            AssignDates();
            Path = PathUtils.GetPathToFolder(Parent);
            this.AssignUniqueName();
        }

        public void GetParentFromPath()
        {
            if (ScopeController.Instance.TryGetFolderForPath(Path, out FolderConfig folderConfig))
            {
                folderConfig.AddConfig(this);
            }
        }
        
        public void UpdateName(string newName)
        {
            if (string.Equals(newName, Name))
            {
                return;
            }
            
            Name = newName;
            InvokeConfigModified();
        }

        public void UpdateDescription(string newDescription)
        {
            if (string.Equals(newDescription, Description))
            {
                return;
            }
            
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
            OnConfigRemoved?.Invoke(this);
            InvokeTypedOnConfigRemoved();
        }

        public virtual string GetDeletionText()
        {
            return $"Delete {(string.IsNullOrWhiteSpace(Name) ? BaseName : Name)}?";
        }
        
        protected virtual void InvokeTypedOnConfigRemoved()
        {
            
        }

        protected virtual void RemoveConfigFromLibrary()
        {
            Debug.LogWarning("RemoveConfigFromLibrary not implemented.");
        }

        protected void InvokeConfigModified()
        {
            OnAnyConfigModified?.Invoke();
            OnConfigModified?.Invoke(this);
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

        protected void Identify()
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
        }
    }
}
