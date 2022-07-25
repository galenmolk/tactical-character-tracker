using System;
using Ebla.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ebla
{
    public class FolderSlot : MonoBehaviour, IDropHandler
    {
        public event Action<FolderSlot> OnFolderDisabled;
        
        public string Path => path;
        [SerializeField] private string path;

        public void ResetFolder()
        {
            
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnDrop(PointerEventData eventData)
        {
            // GameObject droppedObject = eventData.pointerDrag;
            // if (droppedObject == null)
            //     return;
            //
            // if (droppedObject.TryGetComponent(out BaseConfig file))
            //     FileLibrary.MoveFile(file, this);
        }
        //
        // private void OnEnable()
        // {
        //     FileLibrary.OnPreLibraryModified += HandlePreLibraryModified;
        // }
        //
        // private void OnDisable()
        // {
        //     OnFolderDisabled?.Invoke(this);
        //     FileLibrary.OnPreLibraryModified -= HandlePreLibraryModified;
        // }
        
        private void HandlePreLibraryModified()
        {
            gameObject.SetActive(false);
        }
    }
}
