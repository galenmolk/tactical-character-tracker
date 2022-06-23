using System;
using Ebla.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.AddButtons
{
    [RequireComponent(typeof(Button))]
    public abstract class AddConfigButton<TConfig> : MonoBehaviour where TConfig : BaseConfig, new()
    {
        public static event Action<TConfig> OnAddConfigButtonClicked;
        
        public void AddConfigButtonClicked()
        {
            Debug.Log("AddConfigButtonClicked");
            OnAddConfigButtonClicked?.Invoke(GetNewConfig());
        }

        protected abstract TConfig GetNewConfig();
    }
}
