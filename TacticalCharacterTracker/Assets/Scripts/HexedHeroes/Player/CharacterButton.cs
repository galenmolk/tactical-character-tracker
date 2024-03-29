using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HexedHeroes.Player
{
    public class CharacterButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text characterName;
        [SerializeField] private Image image;
    
        private CharacterConfig characterConfig;
        private UnityAction<CharacterConfig> selectAction;
    
        public void Initialize(CharacterConfig _characterConfig, UnityAction<CharacterConfig> _selectAction)
        {
            selectAction = _selectAction;
            characterConfig = _characterConfig;
            image.color = Utils.Utils.GetColorFromHex(characterConfig.colorHex) ?? Color.white;
            characterName.text = characterConfig.name;
        }

        public void Select()
        {
            selectAction.Invoke(characterConfig);
        }
    }
}
