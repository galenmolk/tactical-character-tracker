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
    public bool isCooldownActive;

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
        Deactivate();
        UpdateCooldownText();
    }

    public void InfoButtonPressed()
    {
        AbilityInfoBox.Instance.DisplayAbilityInfo(ability);
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
