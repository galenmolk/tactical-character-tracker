using UnityEngine;

namespace Ebla
{
    public class FileBrowser : MonoBehaviour
    {
        [SerializeField] private RectTransform fileArea;

        private void OnEnable()
        {
            FileLibrary.OnPostLibraryModified += HandlePostLibraryModified;
        }

        private void OnDisable()
        {
            FileLibrary.OnPostLibraryModified -= HandlePostLibraryModified;
        }

        private void HandlePostLibraryModified()
        {
            foreach (IFileable file in FileLibrary.AllFiles)
            {
                DisplayFile(file);
            }
        }
        
        private void DisplayFile(IFileable file)
        {
            FileSlot fileSlot = PrefabLibrary.Instance.GetFileSlot();
            fileSlot.Configure(file);
            fileSlot.transform.SetParent(fileArea);
        }
    }
}
