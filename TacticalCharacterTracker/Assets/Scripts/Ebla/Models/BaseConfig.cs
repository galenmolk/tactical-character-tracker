using System;
using System.Runtime.Serialization;
using Ebla.UI;
using Ebla.Utils;
using Newtonsoft.Json;

namespace Ebla.Models
{
    [Serializable]
    public abstract class BaseConfig
    {
        public BaseConfig()
        {
            
        }
        
        protected BaseConfig(FolderConfig parent)
        {
            AssignDates();
            Path = PathUtils.GetPathToFolder(parent);
            parent.AddConfigToFolder(this);
            this.AssignUniqueName();
        }

        public event Action OnConfigModified;
        public event Action OnConfigRemoved;

        public abstract string BaseName { get; }

        [JsonProperty(ConfigKeys.NAME_KEY)]
        public string Name { get; protected set; }
        
        [JsonProperty(ConfigKeys.DESCRIPTION_KEY)]
        public string Description { get; set; }
        
        [JsonProperty(ConfigKeys.ID_KEY)]
        public string Id { get; private set; }

        [JsonProperty(ConfigKeys.PATH_KEY)]
        public string Path { get; protected set; }

        [JsonProperty(ConfigKeys.FULL_PATH_KEY)]
        public string FullPath { get; private set; }

        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; private set; }

        [JsonIgnore]
        public FolderConfig Parent { get; private set; }

        public void UpdatePath(string path)
        {
            Path = path;
            InvokeConfigModified();
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

        public void TryDeleteConfig()
        {
            ConfirmationController.Instance.DeleteConfig(this);
        }
        
        public void DeleteConfig()
        {
            RemoveConfigFromLibrary();
            OnConfigRemoved?.Invoke();
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
