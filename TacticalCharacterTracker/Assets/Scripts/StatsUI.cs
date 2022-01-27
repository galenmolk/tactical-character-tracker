using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private StatsSection defenseSection;
    [SerializeField] private StatsSection healthSection;
    [SerializeField] private StatsSection speedSection;

    public void LoadStats(CharacterConfig character)
    {
        defenseSection.LoadStat(character.defense);
        healthSection.LoadStat(character.health);
        speedSection.LoadStat(character.speed);
    }

    private void OnEnable()
    {
        MessageCenter.SubscribeBurningTokenActivated(OnBurningTokenActivated);
    }

    private void OnDisable()
    {
        MessageCenter.UnsubscribeBurningTokenActivated(OnBurningTokenActivated);
    }

    private void OnBurningTokenActivated()
    {
        if (defenseSection.CurrentStat > 0)
        {
            defenseSection.Subtract();
            return;            
        }
        
        if (healthSection.CurrentStat > 0)
            healthSection.Subtract();
    }

    public void Unstoppable()
    {
        int defenseDiff = defenseSection.TotalStat - defenseSection.CurrentStat;
        
        Debug.Log(defenseDiff);
        
        if (defenseDiff == 0)
            return;

        Debug.Log(defenseSection.TotalStat);
        
        
        Debug.Log(defenseSection.CurrentStat);

        if (defenseDiff == 1)
        {
            defenseSection.Add();
        }
        else
        {
            defenseSection.Add();
            defenseSection.Add();
        }
    }
}
