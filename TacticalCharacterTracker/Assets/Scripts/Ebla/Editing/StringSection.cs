using Ebla.Editing.Sections;
using TMPro;
using UnityEngine;

namespace Ebla.Editing
{
    public class StringSection : ControlsSection<string>
    {
        [SerializeField] private TMP_InputField inputField;
        
        public override void TrySetValue(string newValue)
        {
            if (string.Equals(newValue, Value))
                return;

            ModifyValue(newValue);
        }

        private void Awake()
        {
            inputField.onEndEdit.AddListener(TrySetValue);
        }
    }
}
