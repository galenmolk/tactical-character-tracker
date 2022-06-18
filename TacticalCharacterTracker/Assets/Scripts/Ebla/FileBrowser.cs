using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ebla
{
    public class FileBrowser : MonoBehaviour
    {
        [SerializeField] private RectTransform fileArea;
        
        private readonly Dictionary<IFile, File> fileRegistry = new();

        private void OnEnable()
        {
            FileLibrary.OnFileAdded += HandleFileAdded;
            FileLibrary.OnFileRemoved += HandleFileRemoved;
        }

        private void OnDisable()
        {
            FileLibrary.OnFileAdded -= HandleFileAdded;
            FileLibrary.OnFileRemoved -= HandleFileRemoved;        
        }

        private void HandleFileAdded(IFile file)
        {
            IRequestable<File>.Request(prefab =>
            {
                File fileInstance = Instantiate(prefab, fileArea);
                fileRegistry.Add(file, fileInstance);
                fileInstance.Configure(file);
            });
        }

        private void HandleFileRemoved(IFile file)
        {
            Debug.Log("HandleFileRemoved");
            if (!fileRegistry.TryGetValue(file, out File fileInstance))
                return;

            Debug.Log("HandleFileRemoved Destroy");

            Destroy(fileInstance.gameObject);
            fileRegistry.Remove(file);
        }
    }
}
