using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsSection : MonoBehaviour
{
      [SerializeField] private TMP_Text totalStatText;
      [SerializeField] private TMP_Text currentStatText;
      [SerializeField] private Button plusButton;
      [SerializeField] private Button subtractButton;

      public int TotalStat { get; private set; }
      public int CurrentStat { get; private set; }

      public void LoadStat(int _totalStat)
      {
            TotalStat = _totalStat;
            CurrentStat = _totalStat;

            UpdateText();
            UpdateButtonStates();
      }

      public void Add()
      {
            CurrentStat++;
            UpdateStatSection();
      }

      public void Subtract()
      {
            CurrentStat--;
            UpdateStatSection();
      }

      private void UpdateStatSection()
      {
            UpdateText();
            UpdateButtonStates();
      }
      
      private void UpdateText()
      {
            totalStatText.text = TotalStat.ToString();
            currentStatText.text = CurrentStat.ToString(); 
      }
      
      private void UpdateButtonStates()
      {
            subtractButton.interactable = CurrentStat > 0;
            plusButton.interactable = CurrentStat < TotalStat;
      }
}
