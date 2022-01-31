using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownAbilitySlot : AbilitySlot<CooldownAbilityConfig>
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text abilityCooldown;

    private int totalCooldown;
    private int currentCooldown;
    private bool isCooldownActive;

    public override void Initialize(CooldownAbilityConfig _ability)
    {
        base.Initialize(_ability);
        totalCooldown = _ability.cooldown;
        currentCooldown = totalCooldown;
        UpdateCooldownText();
    }

    public void TriggerAbility()
    {
        isCooldownActive = true;
        Deactivate();
        UpdateCooldownText();
    }

    private void Awake()
    {
        TurnManager.Instance.SubscribeTurnStarted(OnTurnStarted);
        TurnManager.Instance.SubscribeTurnEnded(OnTurnEnded);
    }

    private void Deactivate()
    {
        button.interactable = false;
    }

    private void Activate()
    {
        button.interactable = true;
    }

    private void UpdateCooldownText()
    {
        string cooldownText = isCooldownActive ? ability.GetCurrentCooldownDescription(currentCooldown) : ability.GetCooldownDescription();
        abilityCooldown.text = cooldownText;
    }

    private void EndCooldown()
    {
        isCooldownActive = false;
        currentCooldown = totalCooldown;
    }

    private void OnTurnStarted()
    {
        if (currentCooldown > 0)
            return;
        
        EndCooldown();
        Activate();
        UpdateCooldownText();
    }
    
    private void OnTurnEnded()
    {
        if (!isCooldownActive)
            return;
        
        if (currentCooldown > 0)
            currentCooldown--;
        
        UpdateCooldownText();
    }
}
