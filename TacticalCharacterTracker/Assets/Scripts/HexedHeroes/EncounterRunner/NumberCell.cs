using UnityEngine;
using UnityEngine.Events;

namespace HexedHeroes.EncounterRunner
{
    public class NumberCell : Cell
    {
        [SerializeField] private UnityEvent<int> onValueChanged;

        private int startAmount;
        
        public void SetInt(int amount)
        {
            startAmount = amount;
            valueText.text = startAmount.ToString();
        }

        public void Increment()
        {
            int value = GetNumber();
            value++;
            valueText.text = value.ToString();
            onValueChanged?.Invoke(value);
        }

        public void Decrement()
        {
            int value = GetNumber();
            value--;
            valueText.text = value.ToString();
            onValueChanged?.Invoke(value);
        }

        protected int DecrementAndReturnValue()
        {
            int value = GetNumber();
            value--;
            valueText.text = value.ToString();
            onValueChanged?.Invoke(value);
            return value;
        }

        protected int GetNumber()
        {
            string text = valueText.text;
            
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }
            
            if (int.TryParse(text, out int value))
            {
                return value;
            }

            Debug.LogWarning("Integer Parsing Failed.");
            valueText.text = string.Empty;
            return 0;
        }
    }
}
