using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyOptionCard : MonoBehaviour
{
      [SerializeField] private TMP_Text nameText;
      [SerializeField] private Toggle toggle;
      
      public CharacterConfig Config { get; private set; }

      public bool IsSelected => toggle.isOn;
      
      public void Initialize(CharacterConfig config)
      {
            Config = config;
            nameText.text = config.name;
      }
}
