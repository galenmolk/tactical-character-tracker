using HexedHeroes.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.Creator
{
      public class EnemyOptionCard : MonoBehaviour
      {
            [SerializeField] private TMP_Text nameText;
            [SerializeField] private Toggle toggle;

            public readonly EnemyOptionCardEvent onSelectionChanged = new();
      
            public CharacterConfig Config { get; private set; }

            public bool IsSelected
            {
                  get => toggle.isOn;
                  set => toggle.isOn = value;
            }

            public void Initialize(CharacterConfig config)
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
