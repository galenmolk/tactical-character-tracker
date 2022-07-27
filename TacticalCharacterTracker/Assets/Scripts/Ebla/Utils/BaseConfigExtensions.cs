using System;
using System.Collections.Generic;
using Ebla.Models;
using MolkExtras;
using UnityEngine;

namespace Ebla.Utils
{
    public static class BaseConfigExtensions
    {
        public static string[] GetFolderNamesInPath(this BaseConfig baseConfig)
        {
            return ScopeController.GetNamesFromPath(baseConfig.Path);
        }

        public static string GetParentName(this BaseConfig baseConfig)
        {
            string[] folders = baseConfig.GetFolderNamesInPath();

            int? foldersLength = folders?.Length;
            return folders?.Length > 0 ? folders[foldersLength.Value - 1] : null;
        }

  

        public static string[] GetAllFoldersInPath(this BaseConfig baseConfig)
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
                folders[i] = folderPath += ScopeController.PATH_DELIMITER + folderNames[i];;
            }

            return folders;
        }
        
        public static string AltStringify<T>(this List<T> list, Func<T, object> property, string delimiter = ", ")
        {
            string listAsString = string.Empty;
            for (int i = 0, count = list.Count; i < count; i++)
            {
                listAsString += property(list[i]).ToString();

                if (i < count - 1)
                {
                    listAsString += delimiter;
                }
            }

            return listAsString;
        }
    }
}
