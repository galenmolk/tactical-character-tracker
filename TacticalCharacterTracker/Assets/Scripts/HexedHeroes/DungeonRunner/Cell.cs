using TMPro;
using UnityEngine;

namespace HexedHeroes.DungeonRunner
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] protected TMP_Text valueText;

        public void SetString(string value)
        {
            valueText.text = value;
        }
    }
}
