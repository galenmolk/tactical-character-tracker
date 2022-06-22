using System.Collections.Generic;
using Ebla.Models;

namespace Ebla.LibraryControllers
{
    public abstract class LibraryController<TConfig, TLibraryConfig> 
        where TConfig : BaseConfig
        where TLibraryConfig : BaseLibraryConfig<TConfig>
    {
        private List<string> ContentNames
        {
            get
            {
                if (isNamesListDirty)
                    RefreshNamesList();

                return contentNames;
            }
        }

        private readonly TLibraryConfig library;
        private readonly List<string> contentNames;

        private bool isNamesListDirty;
        
        public LibraryController(TLibraryConfig libraryConfig)
        {
            library = libraryConfig;
            contentNames = new List<string>();

            Initialize();
        }

        public void Add(TConfig config)
        {
            library.Contents.Add(config);
            isNamesListDirty = true;
        }

        public void AddRange(IEnumerable<TConfig> configs)
        {
            library.Contents.AddRange(configs);
            isNamesListDirty = true;
        }

        public void Remove(TConfig config)
        {
            library.Contents.Remove(config);
            isNamesListDirty = true;
        }

        public string GetUniqueName(string baseName)
        {
            string uniqueName = baseName;
            int nameIndex = 0;
            
            while (ContentNames.Contains(uniqueName))
            {
                nameIndex++;
                uniqueName = $"{baseName} {nameIndex}";
            }

            return uniqueName;
        }

        private void Initialize()
        {
            for (int i = 0, count = library.Contents.Count; i < count; i++)
            {
                TConfig config = library.Contents[i];
                contentNames.Add(config.Name);
                config.OnConfigModified += HandleConfigModified;
            }
        }
        
        private void RefreshNamesList()
        {
            contentNames.Clear();
            
            for (int i = 0, count = library.Contents.Count; i < count; i++)
                contentNames.Add(library.Contents[i].Name);

            isNamesListDirty = false;
        }

        private void HandleConfigModified()
        {
            isNamesListDirty = true;
        }
    }
}
