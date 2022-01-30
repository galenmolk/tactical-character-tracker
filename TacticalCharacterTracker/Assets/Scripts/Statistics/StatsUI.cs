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

    public void TakeDamage(int amount)
    {
        Debug.Log("Take Damage");
        int health = healthSection.CurrentStat;
        int defense = defenseSection.CurrentStat;
        
        // If both stats are 0, exit.
        if (health + defense == 0)
            return;
        
        // If defense will soak all damage, subtract amount from defense and exit.
        if (defense >= amount)
        {
            defenseSection.Subtract(amount);
            return;            
        }
        
        // Otherwise, the defense breaks.
        defenseSection.Subtract(defense);

        // Subtract defense from the amount to get the remaining un-dealt damage.
        int healthDamage = amount - defense;
        
        // Subtract this remainder -- or just remove all health if damage would result in negative value.
        healthSection.Subtract(health >= healthDamage ? healthDamage : health);
    }

    public void Unstoppable()
    {
        int defenseDiff = defenseSection.TotalStat - defenseSection.CurrentStat;
        
        if (defenseDiff == 0)
            return;

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
