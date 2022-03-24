using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyOptionCard : MonoBehaviour
{
      [SerializeField] private TMP_Text nameText;
      [SerializeField] private Toggle toggle;

      public EnemyOptionCardEvent OnSelectionChanged = new EnemyOptionCardEvent();
      
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
            OnSelectionChanged.Invoke(this);
      }
}
