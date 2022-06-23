using System;
using Ebla.LibraryControllers;
using Ebla.Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ebla
{
    public class FileSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static event Action<FileSlot> OnOpenFileForEditing;
        public event Action<FileSlot> OnReleaseFileSlot;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text nameText;

        public BaseConfig File { get; private set; }

        public void Configure(BaseConfig myFile)
        {
            File = myFile;
            File.OnConfigModified += ApplyFileToSlot;
            ApplyFileToSlot();
        }

        public void ApplyFileToSlot()
        {
            nameText.text = File != null ? File.Name : string.Empty;
        }

        public void DeleteFile()
        {
            Librarian.Instance.Remove(File as AbilityConfig);
            File = null;
            OnReleaseFileSlot?.Invoke(this);
        }

        public void EditFile()
        {
            OnOpenFileForEditing?.Invoke(this);
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
    }
}
