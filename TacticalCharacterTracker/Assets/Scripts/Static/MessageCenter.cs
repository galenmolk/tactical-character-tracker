using UnityEngine.Events;

public static class MessageCenter
{
    private static readonly AbilityConfigEvent AbilityInfoButtonPressed = new AbilityConfigEvent();
    private static readonly UnityEvent OverlayCloseButtonPressed = new UnityEvent();
    private static readonly UnityEvent TurnStarted = new UnityEvent();
    private static readonly UnityEvent TurnEnded = new UnityEvent();
    private static readonly CooldownAbilitySlotEvent CooldownAbilityTriggered = new CooldownAbilitySlotEvent();
    private static readonly UnityEvent BurningTokenActivated = new UnityEvent();
    private static readonly CharacterListEvent CharacterListReceived = new CharacterListEvent();
    private static readonly CharacterConfigEvent CharacterLoaded = new CharacterConfigEvent();
    
    #region AbilityInfoButtonPressed
    public static void InvokeAbilityInfoButtonPressed(AbilityConfig ability)
    {
        AbilityInfoButtonPressed.Invoke(ability);
    }
    
    public static void SubscribeAbilityInfoButtonPressed(UnityAction<AbilityConfig> action)
    {
        AbilityInfoButtonPressed.AddListener(action);
    }
    
    public static void UnsubscribeAbilityInfoButtonPressed(UnityAction<AbilityConfig> action)
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

    #region BurningTokenActivated
    public static void InvokeBurningTokenActivated()
    {
        BurningTokenActivated.Invoke();
    }
    
    public static void SubscribeBurningTokenActivated(UnityAction action)
    {
        BurningTokenActivated.AddListener(action);
    }
    
    public static void UnsubscribeBurningTokenActivated(UnityAction action)
    {
        BurningTokenActivated.RemoveListener(action);
    }
    #endregion
    
    #region CharacterListReceived
    public static void InvokeCharacterListReceived(CharacterListConfig characterListConfig)
    {
        CharacterListReceived.Invoke(characterListConfig);
    }
    
    public static void SubscribeCharacterListReceived(UnityAction<CharacterListConfig> action)
    {
        CharacterListReceived.AddListener(action);
    }
    
    public static void UnsubscribeCharacterListReceived(UnityAction<CharacterListConfig> action)
    {
        CharacterListReceived.RemoveListener(action);
    }
    #endregion
    
    #region CharacterLoaded
    public static void InvokeCharacterLoaded(CharacterConfig character)
    {
        CharacterLoaded.Invoke(character);
    }
    
    public static void SubscribeCharacterLoaded(UnityAction<CharacterConfig> action)
    {
        CharacterLoaded.AddListener(action);
    }
    
    public static void UnsubscribeCharacterLoaded(UnityAction<CharacterConfig> action)
    {
        CharacterLoaded.RemoveListener(action);
    }
    #endregion
}
