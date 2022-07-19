using Ebla.LibraryControllers;
using Ebla.Models;
using Ebla.UI;
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
            Debug.Log("FileBrowser.HandleConfigAdded");
            ConfigSlot configSlot = PrefabLibrary.Instance.GetConfigSlot();
            configSlot.OnReleaseObject += HandleConfigSlotReleased;
            configSlot.Configure(config);
            configSlot.transform.SetParent(fileArea);        
        }

        private void HandleConfigSlotReleased(ConfigSlot fileSlot)
        {
            
        }
    }
}
