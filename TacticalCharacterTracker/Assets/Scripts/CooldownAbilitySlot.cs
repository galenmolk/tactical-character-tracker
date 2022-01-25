using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownAbilitySlot : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text abilityCooldown;
    [SerializeField] protected TMP_Text abilityName;

    public string AbilityName => ability.name;
    
    public bool IsInterruptAbility => ability.isInterrupt;
    
    private int totalCooldown;
    private int currentCooldown;
    private CooldownAbilityConfig ability;
    public bool isCooldownActive;

    private const string EMERALD_LANCE = "Emerald Lance";
    private const string EMERALD_LIGHTNING = "Emerald Lightning";

    private static bool isEmeraldLanceActive;
    
    public void Initialize(CooldownAbilityConfig _ability)
    {
        ability = _ability;
        abilityName.text = ability.name;
        totalCooldown = _ability.cooldown;
        currentCooldown = totalCooldown;
        UpdateCooldownText();
    }

    public void TriggerAbility()
    {
        isCooldownActive = true;

        if (ability.name == EMERALD_LANCE)
        {
            isEmeraldLanceActive = true;
            isCooldownActive = false;
        }

        if (ability.name == EMERALD_LIGHTNING)
        {
            var allSlots = FindObjectsOfType<CooldownAbilitySlot>();

            foreach (var slot in allSlots)
            {
                if (slot.AbilityName == EMERALD_LANCE)
                    slot.isCooldownActive = true;
            }

            isEmeraldLanceActive = false;
        }
        
        UpdateCooldownText();
        MessageCenter.InvokeCooldownAbilityTriggered(this);
    }

    public void InfoButtonPressed()
    {
        MessageCenter.InvokeAbilityInfoButtonPressed(ability);
    }

    public void Deactivate()
    {
        button.interactable = false;
    }

    private void Activate()
    {
        button.interactable = true;
    }
    
    private void OnEnable()
    {
        MessageCenter.SubscribeTurnStarted(OnTurnStarted);
        MessageCenter.SubscribeTurnEnded(OnTurnEnded);
    }

    private void OnDisable()
    {
        MessageCenter.UnsubscribeTurnStarted(OnTurnStarted);
        MessageCenter.UnsubscribeTurnEnded(OnTurnEnded);
    }

    private void UpdateCooldownText()
    {
        string cooldownText = isCooldownActive ? ability.GetCurrentCooldownDescription(currentCooldown) : ability.GetCooldownDescription();
        abilityCooldown.text = cooldownText;
    }

    private void EndCooldown()
    {
        isCooldownActive = false;
        Activate();
        currentCooldown = totalCooldown;
    }

    private void OnTurnStarted()
    {
        if (ability.name == EMERALD_LIGHTNING && !isEmeraldLanceActive)
            return;
        
        if (ability.name == EMERALD_LANCE && isEmeraldLanceActive)
            return;
        
        if (ability.isInterrupt)
        {
            Deactivate();
            return;
        }
        
        if (!isCooldownActive)
        {
            Activate();
            return;
        }
        
        if (currentCooldown > 0)
            return;
        
        EndCooldown();
        UpdateCooldownText();
    }
    
    private void OnTurnEnded()
    {
        if (!ability.isInterrupt)
            Deactivate();
        else if (!isCooldownActive)
            Activate();
        
        if (!isCooldownActive)
            return;
        
        if (currentCooldown > 0)
            currentCooldown--;
        
        UpdateCooldownText();
    }
}
