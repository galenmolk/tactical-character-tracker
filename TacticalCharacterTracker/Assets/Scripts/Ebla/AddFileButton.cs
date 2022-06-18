using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla
{
    [RequireComponent(typeof(Button))]
    public class AddFileButton : MonoBehaviour
    {
        public static event Action<FileFactory.FileType> OnFileButtonClicked;
        
        [SerializeField] private FileFactory.FileType fileType;

        public void FileButtonClicked()
        {
            OnFileButtonClicked?.Invoke(fileType);
            FileLibrary.AddFile(FileFactory.GetFileForType(fileType));
        }
    }
}
