using HexedHeroes.Models;
using HexedHeroes.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes
{
    public class AbilityOptionCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Toggle toggle;
        
        public readonly AbilityOptionCardEvent onSelectionChanged = new();

        public AbilityConfig Config { get; private set; }

        public GameObject GameObject
        {
            get
            {
                if (_gameObject == null)
                    _gameObject = gameObject;

                return _gameObject;
            }
        }

        private GameObject _gameObject;
        
        public bool IsSelected
        {
            get => toggle.isOn;
            set => toggle.SetIsOnWithoutNotify(value);
        }
        
        public void Initialize(AbilityConfig config)
        {
            Config = config;
            nameText.text = config.name;
        }
        
        public void OptionCardClicked()
        {
            toggle.isOn = !toggle.isOn;
        }
        
        private void Awake()
        {
            toggle.onValueChanged.AddListener(_ => SelectionChanged());
        }

        private void SelectionChanged()
        {
            onSelectionChanged.Invoke(this);
        }
    }
}
