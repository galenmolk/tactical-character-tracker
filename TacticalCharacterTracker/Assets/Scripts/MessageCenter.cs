using UnityEngine;
using UnityEngine.Events;

public static class MessageCenter
{
    private static readonly AbilityEvent AbilityInfoButtonPressed = new AbilityEvent();
    private static readonly UnityEvent OverlayCloseButtonPressed = new UnityEvent();
    
    public static void InvokeAbilityInfoButtonPressed(Ability ability)
    {
        AbilityInfoButtonPressed.Invoke(ability);
    }
    
    public static void SubscribeAbilityInfoButtonPressed(UnityAction<Ability> action)
    {
        AbilityInfoButtonPressed.AddListener(action);
    }
    
    public static void UnsubscribeAbilityInfoButtonPressed(UnityAction<Ability> action)
    {
        AbilityInfoButtonPressed.RemoveListener(action);
    }
    
    public static void InvokeOverlayCloseButtonPressed()
    {
        OverlayCloseButtonPressed.Invoke();
    }
    
    public static void SubscribeOverlayCloseButtonPressed(UnityAction action)
    {
        OverlayCloseButtonPressed.AddListener(action);
    }
    
    public static void UnsubscribeOverlayCloseButtonPressed(UnityAction action)
    {
        OverlayCloseButtonPressed.RemoveListener(action);
    }
}
