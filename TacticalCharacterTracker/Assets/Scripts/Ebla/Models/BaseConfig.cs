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
            Initialize();
        }

        public BaseConfig(string name)
        {
            Initialize();
            Name = name;
        }

        public event Action OnConfigModified;
        public event Action OnConfigRemoved;

        public abstract string BaseName { get; }

        [JsonProperty(ConfigKeys.NAME_KEY)]
        public string Name { get; private set; }
        
        [JsonProperty(ConfigKeys.DESCRIPTION_KEY)]
        public string Description { get; set; }
        
        [JsonProperty(ConfigKeys.ID_KEY)]
        public string Id { get; private set; }

        [JsonProperty(ConfigKeys.PATH_KEY)]
        public string Path { get; private set; }

        public DateTime DateCreated { get; private set; }
        public DateTime DateModified { get; private set; }

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
        private void OnSerialized(StreamingContext context)
        {
            Identify();
        }
        
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
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

        private void Initialize()
        {
            DateCreated = DateTime.Now;
            DateModified = DateCreated;
        }
    }
}
