using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ebla
{
    public static class FileLibrary
    {
        public static event Action OnPreLibraryModified;
        public static event Action OnPostLibraryModified;

        public static List<IFileable> AllFiles { get; private set; } = new();

        public static void AddFile(IFileable file)
        {
            OnPreLibraryModified?.Invoke();
            AllFiles.Add(file);
            OnPostLibraryModified?.Invoke();
        }
        
        public static void RemoveFile(IFileable file)
        {
            OnPreLibraryModified?.Invoke();
            AllFiles.Remove(file);
            OnPostLibraryModified?.Invoke();
        }

        public static void MoveFile(IFileable file, FolderSlot destination)
        {
            OnPreLibraryModified?.Invoke();
            file.SetFolder(destination);
            OnPostLibraryModified?.Invoke();
        }
    }
}
