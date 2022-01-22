using TMPro;
using UnityEngine;

public class AbilityInfo : AbilityUI
{
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private CanvasGroup canvasGroup;
    
    protected override void DisplayAbilityInfo(Ability ability)
    {
        base.DisplayAbilityInfo(ability);
        abilityDescription.text = ability.description;
        Utils.SetIsCanvasGroupActive(canvasGroup, true);
    }
    
    private void CloseAbilityInfo()
    {
        Utils.SetIsCanvasGroupActive(canvasGroup, false);
    }

    private void OnEnable()
    {
        Utils.SetIsCanvasGroupActive(canvasGroup, false);
        MessageCenter.SubscribeOverlayCloseButtonPressed(CloseAbilityInfo);
        MessageCenter.SubscribeAbilityInfoButtonPressed(DisplayAbilityInfo);  
    }

    private void OnDisable()
    {
        MessageCenter.UnsubscribeOverlayCloseButtonPressed(CloseAbilityInfo);
        MessageCenter.UnsubscribeAbilityInfoButtonPressed(DisplayAbilityInfo);
    }
}
