using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.Editing.Sections
{
    public class TickerSection : ControlsSection<int>
    {
        [SerializeField] private Button subtractButton;
        [SerializeField] private TMP_InputField inputField;
        
        public override void TrySetValue(int newValue)
        {
            if (newValue == Value)
                return;

            newValue = Mathf.Max(newValue, 0);
            inputField.SetTextWithoutNotify(newValue.ToString());
            subtractButton.interactable = newValue > 0;
            ModifyValue(newValue);
        }

        public void AddValue()
        {
            TrySetValue(Value + 1);
        }

        public void TrySubtractValue()
        {
            if (Value > 0)
                TrySetValue(Value - 1);
        }
        
        private void Awake()
        {
            inputField.onEndEdit.AddListener(_ => TrySetValue(ParseText()));
        }

        private int ParseText()
        {
            bool isValid = int.TryParse(inputField.text, out int value);
            return !isValid ? 0 : value;
        }
    }
}
