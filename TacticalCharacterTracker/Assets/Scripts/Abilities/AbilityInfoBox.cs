using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInfoBox : Singleton<AbilityInfoBox>
{
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private CanvasGroup canvasGroup;
    
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityCooldown;

    [SerializeField] private ScrollRect textScrollRect;
    
    public void DisplayAbilityInfo(AbilityConfig ability)
    {
        abilityName.text = ability.name;
        abilityCooldown.text = ability.GetCooldownDescription();
        abilityDescription.text = ability.colorCodedDescription;
        textScrollRect.normalizedPosition = Vector2.up;
        OverlayCloseButton.Instance.Open();
        Utils.SetIsCanvasGroupActive(canvasGroup, true);
    }
    
    public void CloseAbilityInfo()
    {
        Utils.SetIsCanvasGroupActive(canvasGroup, false);
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        CloseAbilityInfo();
    }
}
