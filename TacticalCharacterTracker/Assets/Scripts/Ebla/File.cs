using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ebla
{
    public class File : MonoBehaviour, IContextual
    {
        private IFile file;
        
        public void Configure(IFile myFile)
        {
            file = myFile;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right)
                return;
            
            
            Debug.Log("Right Click");
            FileLibrary.RemoveFile(file);

        }
    }
}
