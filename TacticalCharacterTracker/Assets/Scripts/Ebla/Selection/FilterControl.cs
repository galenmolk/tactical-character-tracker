using System;
using Ebla.Models;
using TMPro;
using UnityEngine;

namespace Ebla.Selection
{
    public abstract class FilterControl<TConfig> : MonoBehaviour
        where TConfig : BaseConfig
    {
        public event Action OnFilterModified;
        
        public string Name => nameFilterInputField.text.Trim().ToLower();
        
        [SerializeField] private TMP_InputField nameFilterInputField;

        public void OnSettingsChanged()
        {
            OnFilterModified?.Invoke();
        }

        public virtual bool IsValid(TConfig config)
        {
            return string.IsNullOrWhiteSpace(Name) || config.Name.Trim().ToLower().Contains(Name);
        }
    }
}
