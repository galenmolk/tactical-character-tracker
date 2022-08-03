using System;
using System.Collections.Generic;
using Ebla.Models;
using Ebla.UI.Slots;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ebla.UI
{
    public class ConfigDragIcon : MonoBehaviour, IDropHandler
    {
        public event Action<FolderSlot> OnDroppedOnFolder;
        
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text configName;
        [SerializeField] private Image border;
        
        private readonly List<RaycastResult> raycastResults = new();
        
        public Transform Transform
        {
            get
            {
                if (_transform == null)
                {
                    _transform = transform;
                }

                return _transform;
            }
        }

        private Transform _transform;
        
        public void Configure(BaseConfig baseConfig, ConfigParams configParams)
        {
            border.color = configParams.Color;
            configName.text = baseConfig.Name;
            icon.sprite = configParams.Icon;
        }

        public void Move()
        {
            Transform.position = Input.mousePosition;
        }

        public void OnDrop(PointerEventData eventData)
        {
            EventSystem.current.RaycastAll(eventData, raycastResults);
            Debug.Log($"raycastResults: {raycastResults.Count}");
            for (int i = 0, count = raycastResults.Count; i < count; i++)
            {
                if (!raycastResults[i].gameObject.TryGetComponent(out FolderSlot folderSlot))
                {
                    continue;
                }
                
                Debug.Log($"FOLDER FOUND : {folderSlot.Config.Name} ");
                OnDroppedOnFolder?.Invoke(folderSlot);
                break;
            }
        }
    }
}
