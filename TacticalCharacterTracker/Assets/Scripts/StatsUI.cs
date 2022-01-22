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
}
