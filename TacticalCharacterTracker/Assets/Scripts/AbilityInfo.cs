using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AbilityInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text abilityDescription;
    [SerializeField] private CanvasGroup canvasGroup;
    
    [SerializeField] private TMP_Text abilityName;
    [SerializeField] private TMP_Text abilityCooldown;

    [SerializeField] private ScrollRect textScrollRect;
    
    private void DisplayAbilityInfo(AbilityConfig ability)
    {
        abilityName.text = ability.name;
        abilityCooldown.text = ability.GetCooldownDescription();
        abilityDescription.text = ability.colorCodedDescription;
        textScrollRect.normalizedPosition = Vector2.up;
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
