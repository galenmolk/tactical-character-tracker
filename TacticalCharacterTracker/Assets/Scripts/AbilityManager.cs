using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private CooldownAbilitySlot cooldownAbilitySlotPrefab;
    [SerializeField] private PassiveAbilitySlot passiveAbilitySlotPrefab;

    [SerializeField] private Transform abilityParent;

    private readonly List<CooldownAbilitySlot> cooldownAbilitySlots = new List<CooldownAbilitySlot>();
    
    public void DisplayPassiveAbilities(PassiveAbilityConfig[] abilities)
    {
        for (int i = 0, length = abilities.Length; i < length; i++)
            CreatePassiveSlot(abilities[i]);
    }
    
    public void DisplayCooldownAbilities(CooldownAbilityConfig[] abilities)
    {
        for (int i = 0, length = abilities.Length; i < length; i++)
            CreateCooldownSlot(abilities[i]);
    }

    private void CreatePassiveSlot(PassiveAbilityConfig ability)
    {
        Instantiate(passiveAbilitySlotPrefab, abilityParent).Initialize(ability);
    }
    
    private void CreateCooldownSlot(CooldownAbilityConfig ability)
    {
        CooldownAbilitySlot slot = Instantiate(cooldownAbilitySlotPrefab, abilityParent);
        slot.Initialize(ability);
        cooldownAbilitySlots.Add(slot);
    }

    private void OnEnable()
    {
        MessageCenter.SubscribeCooldownAbilityTriggered(DeactivateCooldownAbilities);
    }

    private void OnDisable()
    {
        MessageCenter.UnsubscribeCooldownAbilityTriggered(DeactivateCooldownAbilities);
    }

    private void DeactivateCooldownAbilities(CooldownAbilitySlot cooldownAbilitySlot)
    {
        for (int i = 0, length = cooldownAbilitySlots.Count; i < length; i++)
        {
            cooldownAbilitySlots[i].Deactivate();
        }
    }
}
