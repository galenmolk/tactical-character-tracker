using Ebla.Models;
using MolkExtras;
using UnityEngine;

namespace Ebla
{
    public class ScopeController : Singleton<ScopeController>
    {
        private const string ROOT_NAME = "ROOT";
        private const char PATH_DELIMITER = '/';
        
        private FolderConfig rootFolder;
        private FolderConfig currentFolder;
        
        public string GetPathForCurrentScope()
        {
            return GetPathToFolder(currentFolder);
        }
        
        protected override void OnAwake()
        {
            base.OnAwake();
            CreateRootFolder();
            currentFolder = rootFolder;
            // var f1 = new FolderConfig("Galen");
            // var f2 = new FolderConfig("Truett");
            // var f3 = new FolderConfig("Simon");
            // var f4 = new FolderConfig("Forrest");
            // f1.ParentFolder = rootFolder;
            // f2.ParentFolder = f1;
            // f3.ParentFolder = f1;
            // f4.ParentFolder = f2;
            // Debug.Log($"Path to folder {f1.Name}: {GetPathToFolder(f1)}");
            // Debug.Log($"Path to folder {f2.Name}: {GetPathToFolder(f2)}");
            // Debug.Log($"Path to folder {f3.Name}: {GetPathToFolder(f3)}");
            // Debug.Log($"Path to folder {f4.Name}: {GetPathToFolder(f4)}");
        }

        private void CreateRootFolder()
        {
            rootFolder = new FolderConfig(ROOT_NAME);
        }

        private string GetPathToFolder(FolderConfig folderConfig)
        {
            FolderConfig folder = folderConfig;
            string path = folderConfig.Name;
                
            while (folder.ParentFolder != null)
            {
                string parentName = folder.ParentFolder.Name;
                path = path.Insert(0, parentName + PATH_DELIMITER);
                folder = folder.ParentFolder;
            }

            return path;
        }
    }
}
