using System;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private StatsSection defenseSection;
    [SerializeField] private StatsSection healthSection;
    [SerializeField] private StatsSection speedSection;

    public void LoadStats(Character character)
    {
        defenseSection.LoadStat(character.totalDefense);
        healthSection.LoadStat(character.totalHealth);
        speedSection.LoadStat(character.totalSpeed);
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
}
