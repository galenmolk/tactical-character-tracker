using System.Collections.Generic;
using Ebla.Models;
using MolkExtras;

namespace Ebla.Utils
{
    public static class PathUtils
    {
        public const string ROOT_NAME = "ROOT";
        public const char PATH_DELIMITER = '/';
        
        public static string[] GetNamesFromPath(string path)
        {
            return path.Split(PATH_DELIMITER);
        }

        public static string GetLastNameFromPath(string path)
        {
            string[] folders = GetNamesFromPath(path);
            return !folders.TryGetLength(out int length) ? null : folders[length - 1];
        }

        public static string GetCurrentPath()
        {
            return GetPathToFolder(ScopeController.Instance.CurrentFolder);
        }
        
        public static string GetUniqueNameForScope(string baseName)
        {
            FolderConfig currentScope = ScopeController.Instance.CurrentFolder;

            List<string> configNames = GetAllConfigNamesInFolder(currentScope);
            string name = baseName;
            for (int i = 0; configNames.Contains(name); i++)
            {
                name = $"{baseName} {i}";
            }

            return name;
        }

        public static string GetPathToFolder(FolderConfig folderConfig)
        {
            FolderConfig folder = folderConfig;
            string path = folderConfig.Name;
                
            while (folder.Parent != null)
            {
                string parentName =  folder.Parent.Name + PATH_DELIMITER;
                path = path.Insert(0, parentName);
                folder = folder.Parent;
            }

            return path;
        }

        public static List<FolderConfig> GetFolderConfigsInPath(FolderConfig folderConfig)
        {
            List<FolderConfig> folders = new() { folderConfig };
            
            FolderConfig folder = folderConfig;
                
            while (folder.Parent != null)
            {
                folders.Insert(0, folder.Parent);
                folder = folder.Parent;
            }

            return folders;
        }

        private static List<string> GetAllConfigNamesInFolder(FolderConfig folderConfig)
        {
            List<string> configNames = new();
            foreach (BaseConfig config in folderConfig.Configs)
            {
                configNames.Add(config.Name);
            }

            return configNames;
        }
    }
}
