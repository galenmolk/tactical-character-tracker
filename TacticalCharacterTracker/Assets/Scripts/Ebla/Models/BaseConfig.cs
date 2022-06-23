using System;
using System.Runtime.Serialization;

namespace Ebla.Models
{
    [Serializable]
    public abstract class BaseConfig
    {
        public event Action OnConfigModified;

        public BaseConfig()
        {
            DateCreated = DateTime.Now;
            DateModified = DateCreated;
        }

        public abstract string BaseName { get; }

        public string Name { get; private set; }
        
        public string Description { get; set; }
        public string ParentFolderName { get; private set; }
        public DateTime DateCreated { get; }
        public DateTime DateModified { get; }
        public string Id { get; private set; }

        public void UpdateName(string newName)
        {
            Name = newName;
            OnConfigModified?.Invoke();
        }
        
        public void SetFolder(FolderSlot folder)
        {
            ParentFolderName = folder.Path;
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
                Id = NCuid.Cuid.Generate();
        }
    }
}
