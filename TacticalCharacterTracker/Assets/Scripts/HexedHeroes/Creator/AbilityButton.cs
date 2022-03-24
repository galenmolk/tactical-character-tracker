using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AbilityButton : MonoBehaviour
{
    public AbilityConfig AbilityConfig { get; private set; }

    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text cooldownText;

    private UnityAction<AbilityButton> selectAbilityAction;

    public virtual void Initialize(AbilityConfig _abilityConfig, UnityAction<AbilityButton> action)
    {
        selectAbilityAction = action;
        Display(_abilityConfig);
    }

    public void Display(AbilityConfig _abilityConfig)
    {
        AbilityConfig = _abilityConfig;
        name.text = AbilityConfig.name;
        cooldownText.text = AbilityConfig.isPassive ? AbilityConfig.PASSIVE_TEXT : AbilityConfig.cooldown.ToString();
    }
    
    public void OnSelect()
    {
        selectAbilityAction.Invoke(this);
    }
}
