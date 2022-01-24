using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownAbilitySlot : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text abilityCooldown;
    [SerializeField] protected TMP_Text abilityName;

    private int totalCooldown;
    private int currentCooldown;
    private CooldownAbilityConfig ability;
    private bool isCooldownActive;
    
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
        button.interactable = true;
        currentCooldown = totalCooldown;
    }

    private void OnTurnStarted()
    {
        if (!isCooldownActive)
        {
            button.interactable = true;
            return;
        }
        
        if (currentCooldown > 0)
            return;
        
        EndCooldown();
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
