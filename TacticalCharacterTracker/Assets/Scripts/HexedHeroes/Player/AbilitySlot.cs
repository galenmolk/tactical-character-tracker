using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text abilityCooldown;
    [SerializeField] private TMP_Text abilityName;

    private AbilityConfig ability;
    
    private int totalCooldown;
    private int currentCooldown;
    private bool isCooldownActive;

    public void Initialize(AbilityConfig abilityConfig)
    {
        ability = abilityConfig;
        abilityName.text = ability.name;
        CustomAbilityLibrary.TryAddCustomAbility(gameObject, ability.name);
        totalCooldown = abilityConfig.cooldown;
        currentCooldown = totalCooldown;
        UpdateCooldownText();

        if (abilityConfig.isPassive)
            button.enabled = false;
    }

    public void InfoButtonPressed()
    {
        AbilityInfoBox.Instance.DisplayAbilityInfo(ability);
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
        string text = isCooldownActive ? ability.GetCurrentCooldownDescription(currentCooldown) : ability.GetCooldownDescription();
        abilityCooldown.text = text;
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
