using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private AbilitySlot abilitySlotPrefab;
    // [SerializeField] private PassiveAbilitySlot passiveAbilitySlotPrefab;

    [SerializeField] private Transform abilityParent;

    [SerializeField] private ScrollRect abilityScrollRect;
    
    private readonly List<AbilitySlot> cooldownAbilitySlots = new List<AbilitySlot>();
    // private readonly List<PassiveAbilitySlot> passiveAbilitySlots = new List<PassiveAbilitySlot>();

    public void DisplayAbilities(CharacterConfig config)
    {
        //DisplayPassiveAbilities(config.passiveAbilities);
        DisplayCooldownAbilities(config.abilities);
        abilityScrollRect.normalizedPosition = Vector2.up;
    }

    // private void DisplayPassiveAbilities(List<PassiveAbilityConfig> abilities)
    // {
    //     DestroyPassiveAbilities();
    //     for (int i = 0, length = abilities.Count; i < length; i++)
    //         CreatePassiveSlot(abilities[i]);
    // }

    private void DisplayCooldownAbilities(List<AbilityConfig> abilities)
    {
        DestroyCooldownAbilities();
        for (int i = 0, length = abilities.Count; i < length; i++)
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
    
    // private void DestroyPassiveAbilities()
    // {
    //     for (int i = passiveAbilitySlots.Count - 1; i >= 0; i--)
    //     {
    //         Destroy(passiveAbilitySlots[i].gameObject);
    //     }
    //     
    //     passiveAbilitySlots.Clear();
    // }

    // private void CreatePassiveSlot(PassiveAbilityConfig ability)
    // {
    //     PassiveAbilitySlot slot = Instantiate(passiveAbilitySlotPrefab, abilityParent);
    //     slot.Initialize(ability);
    //     passiveAbilitySlots.Add(slot);
    // }
    
    private void CreateCooldownSlot(AbilityConfig ability)
    {
        AbilitySlot slot = Instantiate(abilitySlotPrefab, abilityParent);
        slot.Initialize(ability);
        cooldownAbilitySlots.Add(slot);
    }
}
