using System.Collections.Generic;
using Ebla.Models;

namespace Ebla.Libraries
{
    public abstract class LibraryController<TConfig> 
        where TConfig : BaseConfig
    {
        // private List<string> ContentNames
        // {
        //     get
        //     {
        //         if (isNamesListDirty)
        //             RefreshNamesList();
        //
        //         return contentNames;
        //     }
        // }

        private List<TConfig> library = new();
        // private readonly List<string> contentNames;

        // private bool isNamesListDirty;
        
        // public LibraryController(List<TConfig> libraryConfig)
        // {
        //     library = libraryConfig;
        //     contentNames = new List<string>();
        //
        //     Initialize();
        // }

        public void Add(TConfig config)
        {
            // config.UpdateName(GetUniqueName(config.BaseName));
            // config.OnConfigModified += HandleConfigModified;
            library.Add(config);
            // isNamesListDirty = true;
        }

        public void Remove(TConfig config)
        {
            // config.OnConfigModified -= HandleConfigModified;
            library.Remove(config);
            // isNamesListDirty = true;
        }

        public List<TConfig> All()
        {
            return library;
        }

        // private string GetUniqueName(string baseName)
        // {
        //     string uniqueName = baseName;
        //     int nameIndex = 0;
        //     
        //     while (ContentNames.Contains(uniqueName))
        //     {
        //         nameIndex++;
        //         uniqueName = $"{baseName} {nameIndex}";
        //     }
        //
        //     return uniqueName;
        // }

        // private void Initialize()
        // {
        //     for (int i = 0, count = library.Count; i < count; i++)
        //     {
        //         TConfig config = library[i];
        //         contentNames.Add(config.Name);
        //         // config.OnConfigModified += HandleConfigModified;
        //     }
        // }
        
        // private void RefreshNamesList()
        // {
        //     contentNames.Clear();
        //     
        //     for (int i = 0, count = library.Count; i < count; i++)
        //         contentNames.Add(library[i].Name);
        //
        //     isNamesListDirty = false;
        // }

        // private void HandleConfigModified()
        // {
        //     isNamesListDirty = true;
        // }
    }
}
