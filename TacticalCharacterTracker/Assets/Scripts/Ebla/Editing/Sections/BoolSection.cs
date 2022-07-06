using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.Editing.Sections
{
    public class BoolSection : ControlsSection<bool>
    {
        [SerializeField] private Toggle toggle;
        
        public override void TrySetValue(bool newValue)
        {
            if (newValue == Value)
                return;
            
            toggle.SetIsOnWithoutNotify(newValue);
            ModifyValue(newValue);
        }

        private void Awake()
        {
            toggle.onValueChanged.AddListener(TrySetValue);
        }
    }
}
