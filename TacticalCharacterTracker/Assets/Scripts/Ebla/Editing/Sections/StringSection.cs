using TMPro;
using UnityEngine;

namespace Ebla.Editing.Sections
{
    public class StringSection : ControlsSection<string>
    {
        [SerializeField] private TMP_InputField inputField;
        
        public override void TrySetValue(string newValue)
        {
            if (string.Equals(newValue, Value))
                return;

            inputField.SetTextWithoutNotify(newValue);
            ModifyValue(newValue);
        }

        private void Awake()
        {
            inputField.onEndEdit.AddListener(TrySetValue);
        }
    }
}
