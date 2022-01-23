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
    }
    
    public void InfoButtonPressed()
    {
        MessageCenter.InvokeAbilityInfoButtonPressed(ability);
    }
}
