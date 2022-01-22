using UnityEngine.Events;

public static class MessageCenter
{
    private static readonly AbilityEvent AbilityInfoButtonPressed = new AbilityEvent();
    private static readonly UnityEvent OverlayCloseButtonPressed = new UnityEvent();
    private static readonly UnityEvent TurnStarted = new UnityEvent();
    private static readonly UnityEvent TurnEnded = new UnityEvent();
    private static readonly CooldownAbilitySlotEvent CooldownAbilityTriggered = new CooldownAbilitySlotEvent();
    
    #region AbilityInfoButtonPressed
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
    #endregion

    #region OverlayCloseButtonPressed
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
    #endregion

    #region TurnStarted
    public static void InvokeTurnStarted()
    {
        TurnStarted.Invoke();
    }
    
    public static void SubscribeTurnStarted(UnityAction action)
    {
        TurnStarted.AddListener(action);
    }
    
    public static void UnsubscribeTurnStarted(UnityAction action)
    {
        TurnStarted.RemoveListener(action);
    }
    #endregion
    
    #region TurnEnded
    public static void InvokeTurnEnded()
    {
        TurnEnded.Invoke();
    }
    
    public static void SubscribeTurnEnded(UnityAction action)
    {
        TurnEnded.AddListener(action);
    }
    
    public static void UnsubscribeTurnEnded(UnityAction action)
    {
        TurnEnded.RemoveListener(action);
    }
    #endregion
    
    #region CooldownAbilityTriggered
    public static void InvokeCooldownAbilityTriggered(CooldownAbilitySlot cooldownAbilitySlot)
    {
        CooldownAbilityTriggered.Invoke(cooldownAbilitySlot);
    }
    
    public static void SubscribeCooldownAbilityTriggered(UnityAction<CooldownAbilitySlot> action)
    {
        CooldownAbilityTriggered.AddListener(action);
    }
    
    public static void UnsubscribeCooldownAbilityTriggered(UnityAction<CooldownAbilitySlot> action)
    {
        CooldownAbilityTriggered.RemoveListener(action);
    }
    #endregion
}
