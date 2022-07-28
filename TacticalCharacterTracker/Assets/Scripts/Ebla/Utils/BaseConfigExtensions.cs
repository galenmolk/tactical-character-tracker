using Ebla.Models;
using MolkExtras;

namespace Ebla.Utils
{
    public static class BaseConfigExtensions
    {
        public static void AssignUniqueName(this BaseConfig baseConfig)
        {
            baseConfig.UpdateName(PathUtils.GetUniqueNameForScope(baseConfig.BaseName));
        }
        
        public static string[] GetFolderNamesInPath(this BaseConfig baseConfig)
        {
            return PathUtils.GetNamesFromPath(baseConfig.Path);
        }

        public static string GetParentName(this BaseConfig baseConfig)
        {
            string[] folders = baseConfig.GetFolderNamesInPath();

            int? foldersLength = folders?.Length;
            return folders?.Length > 0 ? folders[foldersLength.Value - 1] : null;
        }

        public static string[] GetAllParentFolderPathsForConfig(this BaseConfig baseConfig)
        {
            string[] folderNames = baseConfig.GetFolderNamesInPath();

            if (!folderNames.TryGetLength(out int length))
            {
                return null;
            }

            string[] folders = new string[length];
            
            string folderPath = string.Empty;
            
            for (int i = 0; i < length; i++)
            {
                folders[i] = folderPath += PathUtils.PATH_DELIMITER + folderNames[i];;
            }

            return folders;
        }
    }
}
