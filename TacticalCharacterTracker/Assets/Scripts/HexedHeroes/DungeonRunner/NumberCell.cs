using TMPro;
using UnityEngine;

namespace HexedHeroes.DungeonRunner
{
    public class NumberCell : Cell
    {
        protected int startAmount;
        
        public void SetInt(int statAmount)
        {
            startAmount = statAmount;
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

        private int GetNumber()
        {
            if (int.TryParse(valueText.text, out int value))
                return value;

            Debug.LogWarning("Integer Parsing Failed.");
            valueText.text = startAmount.ToString();
            return startAmount;
        }
    }
}
