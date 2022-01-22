using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private CooldownAbilitySlot cooldownAbilitySlotPrefab;
    [SerializeField] private PassiveAbilitySlot passiveAbilitySlotPrefab;

    [SerializeField] private Transform abilityParent;

    private readonly List<CooldownAbilitySlot> cooldownAbilitySlots = new List<CooldownAbilitySlot>();
    
    public void DisplayAbilities(Ability[] abilities)
    {
        for (int i = 0, length = abilities.Length; i < length; i++)
            DisplayAbility(abilities[i]);
    }

    private void DisplayAbility(Ability ability)
    {
        if (ability is CooldownAbility)
            CreateCooldownSlot(ability);
        
        if (ability is PassiveAbility)
            CreatePassiveSlot(ability);
    }

    private void CreateCooldownSlot(Ability ability)
    {
        CooldownAbility cooldownAbility = ability as CooldownAbility;
        CooldownAbilitySlot slot = Instantiate(cooldownAbilitySlotPrefab, abilityParent);
        slot.Initialize(cooldownAbility);
        cooldownAbilitySlots.Add(slot);
    }

    private void CreatePassiveSlot(Ability ability)
    {
        PassiveAbility passiveAbility = ability as PassiveAbility;
        Instantiate(passiveAbilitySlotPrefab, abilityParent).Initialize(passiveAbility);
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
