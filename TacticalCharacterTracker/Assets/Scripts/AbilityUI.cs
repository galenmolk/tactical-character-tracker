using TMPro;
using UnityEngine;

public abstract class AbilityUI : MonoBehaviour
{
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityCooldown;

    protected virtual void DisplayAbilityInfo(Ability ability)
    {
        abilityName.text = ability.name;
        abilityCooldown.text = ability.CooldownText;
    }
}
