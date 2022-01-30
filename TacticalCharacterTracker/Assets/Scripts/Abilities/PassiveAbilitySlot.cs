using TMPro;
using UnityEngine;

public class PassiveAbilitySlot : MonoBehaviour
{
    [SerializeField] protected TMP_Text abilityName;

    private PassiveAbilityConfig ability;
    
    public void Initialize(PassiveAbilityConfig _ability)
    {
        ability = _ability;
        abilityName.text = ability.name;
        if (ability.name == "Unstoppable")
            Unstoppable();
    }

    private void Unstoppable()
    {
        TurnManager.Instance.SubscribeTurnStarted(Gain2Health);
    }

    private void Gain2Health()
    {
        FindObjectOfType<StatsUI>().Unstoppable();
    }
    
    public void InfoButtonPressed()
    {
        AbilityInfoBox.Instance.DisplayAbilityInfo(ability);
    }
}
