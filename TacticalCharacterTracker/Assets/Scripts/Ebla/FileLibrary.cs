using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ebla
{
    public static class FileLibrary
    {
        public static event Action<IFile> OnFileAdded;
        public static event Action<IFile> OnFileRemoved;

        public static List<IFile> AllFiles { get; private set; } = new();

        public static void AddFile(IFile file)
        {
            AllFiles.Add(file);
            OnFileAdded?.Invoke(file);
        }
        
        public static void RemoveFile(IFile file)
        {
            Debug.Log("RemoveFile");

            AllFiles.Remove(file);
            OnFileRemoved?.Invoke(file);
        }
    }
}
