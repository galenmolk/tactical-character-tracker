using System;
using UnityEngine;

namespace Ebla
{
    [Serializable]
    public class BaseConfig : IFile
    {
        public BaseConfig()
        {
            DateCreated = DateTime.Now;
            DateModified = DateCreated;
        }

        public string Name => name;

        public string LowerCaseName => lowerCaseName ??= Name.ToLower();
        public DateTime DateCreated { get; }
        public DateTime DateModified { get; }

        private string lowerCaseName = null;

        [SerializeField] private string name;

        public int CompareTo(object obj)
        {
            IFile file = obj as IFile;
            
            if (file == null)
                return 1;

            return string.Compare(LowerCaseName, file.LowerCaseName, StringComparison.Ordinal);
        }
    }
}
