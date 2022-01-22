using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsSection : MonoBehaviour
{
      [SerializeField] private TMP_Text totalStatText;
      [SerializeField] private TMP_Text currentStatText;
      [SerializeField] private Button plusButton;
      [SerializeField] private Button subtractButton;

      private int totalStat;
      private int currentStat;
      
      public void LoadStat(int _totalStat)
      {
            totalStat = _totalStat;
            currentStat = _totalStat;

            UpdateText();
            UpdateButtonStates();
      }

      public void Add()
      {
            currentStat++;
            UpdateStatSection();
      }

      public void Subtract()
      {
            currentStat--;
            UpdateStatSection();
      }

      private void UpdateStatSection()
      {
            UpdateText();
            UpdateButtonStates();
      }
      
      private void UpdateText()
      {
            totalStatText.text = totalStat.ToString();
            currentStatText.text = currentStat.ToString(); 
      }
      
      private void UpdateButtonStates()
      {
            subtractButton.interactable = currentStat > 0;
            plusButton.interactable = currentStat < totalStat;
      }
}
