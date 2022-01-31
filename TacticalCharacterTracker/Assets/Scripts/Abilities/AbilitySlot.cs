using TMPro;
using UnityEngine;

public abstract class AbilitySlot<TAbility> : MonoBehaviour where TAbility : AbilityConfig
{
    protected TAbility ability;
    [SerializeField] private TMP_Text abilityName;

    public virtual void Initialize(TAbility abilityConfig)
    {
        ability = abilityConfig;
        abilityName.text = ability.name;
    }
    
    public void InfoButtonPressed()
    {
        AbilityInfoBox.Instance.DisplayAbilityInfo(ability);
    }
}
