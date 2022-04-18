using TMPro;
using UnityEngine;

namespace HexedHeroes.DungeonRunner
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] protected TMP_InputField valueText;

        public void SetString(string value)
        {
            valueText.text = value;
        }
    }
}
