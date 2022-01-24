using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private CooldownAbilitySlot cooldownAbilitySlotPrefab;
    [SerializeField] private PassiveAbilitySlot passiveAbilitySlotPrefab;

    [SerializeField] private Transform abilityParent;

    private readonly List<CooldownAbilitySlot> cooldownAbilitySlots = new List<CooldownAbilitySlot>();
    private readonly List<PassiveAbilitySlot> passiveAbilitySlots = new List<PassiveAbilitySlot>();
    
    public void DisplayPassiveAbilities(PassiveAbilityConfig[] abilities)
    {
        DestroyPassiveAbilities();
        for (int i = 0, length = abilities.Length; i < length; i++)
            CreatePassiveSlot(abilities[i]);
    }
    
    public void DisplayCooldownAbilities(CooldownAbilityConfig[] abilities)
    {
        DestroyCooldownAbilities();
        for (int i = 0, length = abilities.Length; i < length; i++)
            CreateCooldownSlot(abilities[i]);
    }

    private void DestroyCooldownAbilities()
    {
        for (int i = cooldownAbilitySlots.Count - 1; i >= 0; i--)
        {
            Destroy(cooldownAbilitySlots[i].gameObject);
        }
        
        cooldownAbilitySlots.Clear();
    }
    
    private void DestroyPassiveAbilities()
    {
        for (int i = passiveAbilitySlots.Count - 1; i >= 0; i--)
        {
            Destroy(passiveAbilitySlots[i].gameObject);
        }
        
        passiveAbilitySlots.Clear();
    }

    private void CreatePassiveSlot(PassiveAbilityConfig ability)
    {
        PassiveAbilitySlot slot = Instantiate(passiveAbilitySlotPrefab, abilityParent);
        slot.Initialize(ability);
        passiveAbilitySlots.Add(slot);
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
