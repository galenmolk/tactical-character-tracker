using Ebla.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.UI
{
    public class ConfigDragIcon : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text configName;
        [SerializeField] private Image border;
        
        private Transform Transform
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
    }
}
