using TMPro;
using UnityEngine;

public class PassiveAbilitySlot : MonoBehaviour
{
    [SerializeField] protected TMP_Text abilityName;

    private PassiveAbility ability;
    
    public void Initialize(PassiveAbility _ability)
    {
        ability = _ability;
        abilityName.text = ability.name;
    }
    
    public void InfoButtonPressed()
    {
        MessageCenter.InvokeAbilityInfoButtonPressed(ability);
    }
}
