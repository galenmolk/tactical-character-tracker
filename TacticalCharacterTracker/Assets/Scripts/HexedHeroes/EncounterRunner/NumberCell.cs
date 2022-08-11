using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class NumberCell : Cell
    {
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
        }

        public void Decrement()
        {
            int value = GetNumber();
            value--;
            valueText.text = value.ToString();
        }

        protected int GetNumber()
        {
            if (int.TryParse(valueText.text, out int value))
            {
                return value;
            }

            Debug.LogWarning("Integer Parsing Failed.");
            valueText.text = 0.ToString();
            return 0;
        }
    }
}
