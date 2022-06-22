using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ebla
{
    public class FileSlot : MonoBehaviour, IContextual, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<FileSlot> OnFileDisabled;
        
        [SerializeField] private CanvasGroup canvasGroup;
        
        private IFileable file;
        
        public void Configure(IFileable myFile)
        {
            file = myFile;
        }

        public void ResetFile()
        {
            
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right)
                return;
            
            FileLibrary.RemoveFile(file);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = false;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
        }

        private void OnEnable()
        {
            FileLibrary.OnPreLibraryModified += HandlePreLibraryModified;
        }

        private void OnDisable()
        {
            OnFileDisabled?.Invoke(this);
            FileLibrary.OnPreLibraryModified -= HandlePreLibraryModified;
        }

        private void HandlePreLibraryModified()
        {
            gameObject.SetActive(false);
        }
    }
}
