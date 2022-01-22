using TMPro;
using UnityEngine;

public class AbilityInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private CanvasGroup canvasGroup;
    
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityCooldown;

    private void DisplayAbilityInfo(Ability ability)
    {
        abilityName.text = ability.name;
        abilityCooldown.text = ability.GetCooldownDescription();
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
