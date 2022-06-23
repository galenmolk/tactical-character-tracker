using System.Collections.Generic;
using System.Linq;
using Ebla.LibraryControllers;
using Ebla.Models;
using UnityEngine;

namespace Ebla
{
    public class FileBrowser : MonoBehaviour
    {
        [SerializeField] private RectTransform fileArea;


        private void OnEnable()
        {
            Librarian.OnConfigAdded += HandleConfigAdded;
        }

        private void OnDisable()
        {
            Librarian.OnConfigAdded -= HandleConfigAdded;
        }

        private void HandleConfigAdded(BaseConfig config)
        {
            DisplayFile(config);
        }
        
        private void DisplayFile(BaseConfig file)
        {
            FileSlot fileSlot = PrefabLibrary.Instance.GetFileSlot();
            fileSlot.OnReleaseFileSlot += HandleFileSlotReleased;
            fileSlot.Configure(file);
            fileSlot.transform.SetParent(fileArea);
        }

        private void HandleFileSlotReleased(FileSlot fileSlot)
        {
        }
    }
}
