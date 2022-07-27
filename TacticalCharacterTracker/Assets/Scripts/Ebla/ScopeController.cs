using System.Collections.Generic;
using System.IO;
using Ebla.Libraries;
using Ebla.Models;
using Ebla.UI;
using Ebla.Utils;
using MolkExtras;
using UnityEngine;
using UnityEngine.XR;

namespace Ebla
{
    public class ScopeController : Singleton<ScopeController>
    {
        private const string ROOT_NAME = "ROOT";
        public const char PATH_DELIMITER = '/';
        
        private FolderConfig rootFolder;
        private FolderConfig currentFolder;

        private Dictionary<string, FolderConfig> folderRegistry = new();

        private List<BaseConfig> configs;
        private List<FolderConfig> folders;

        public string GetPathForCurrentScope()
        {
            return GetPathToFolder(currentFolder);
        }

        public string path;
        
        [UnityEngine.ContextMenu("TestFolderPaths")]
        public void TestFolderPaths()
        {
            AbilityConfig ac = new AbilityConfig();
            ac.UpdatePath(path);

            var paths = ac.GetAllFoldersInPath();
            foreach (string stringPath in paths)
            {
                Debug.Log($"Path: {stringPath}");
            }
        }
        
        [UnityEngine.ContextMenu("TestArrayExt")]
        public void TestArrayExt()
        {
            string[] test = new string[1];
        }
        
        [UnityEngine.ContextMenu("Create Folder System")]
        public void CreateFolderSystem()
        {
            folders = new List<FolderConfig>();
            configs = new List<BaseConfig>();
            configs.AddRange(AbilityLibrarian.Instance.GetAbilities());
            Debug.Log($"configs found: {configs.Count}");

            foreach (var config in configs)
            {
                CreateFolderForConfig(config);
            }

            string folderLog = string.Empty;
            foreach (var folderConfig in folders)
            {
                folderLog += $"Folder Name {folderConfig.Name}, Files: {folderConfig.Files.AltStringify(file => file.Name)}, " +
                             $"Parent: {folderConfig.Parent.Name}\n"; 
            }
            Debug.Log(folderLog);
            
            string configLog = string.Empty;
            foreach (var bc in configs)
            {
                configLog += $"Config Name {bc.Name}, Parent: {bc.Parent.Name}\n"; 
            }
            Debug.Log(configLog);
        }
        
   
        
        protected override void OnAwake()
        {
            base.OnAwake();
            CreateRootFolder();
            currentFolder = rootFolder;
        }

        private void CreateFolderForConfig(BaseConfig config)
        {
            string[] folderPaths = config.GetAllFoldersInPath();

            if (!folderPaths.TryGetLength(out int length))
            {
                rootFolder.AddFile(config);
                return;
            }

            FolderConfig parent = null;

            for (int i = 0; i < length; i++)
            {
                string folderPath = folderPaths[i];
                if (!folderRegistry.TryGetValue(folderPath, out FolderConfig folderConfig))
                {
                    folderConfig = new FolderConfig(folderPath);
                    folders.Add(folderConfig);
                    folderRegistry.Add(folderPath, folderConfig);
                }
                
                if (folderConfig.Parent == null)
                {
                    folderConfig.UpdateParent(parent ?? rootFolder);
                }
                
                parent = folderConfig;

                if (i == length - 1)
                {
                    folderConfig.AddFile(config);
                }
            }
        }
        
        public static string[] GetNamesFromPath(string path)
        {
            return path.Split(PATH_DELIMITER);
        }

        public static string GetLastNameFromPath(string path)
        {
            string[] folders = GetNamesFromPath(path);
            return !folders.TryGetLength(out int length) ? null : folders[length - 1];
        }

        
        private void CreateRootFolder()
        {
            rootFolder = new FolderConfig(ROOT_NAME);
        }

        private string GetPathToFolder(FolderConfig folderConfig)
        {
            FolderConfig folder = folderConfig;
            string path = folderConfig.Name;
                
            while (folder.Parent != null && folder.Parent != rootFolder)
            {
                string parentName =  folder.Parent.Name + PATH_DELIMITER;
                path = path.Insert(0, parentName);
                folder = folder.Parent;
            }

            return path;
        }
    }
}
