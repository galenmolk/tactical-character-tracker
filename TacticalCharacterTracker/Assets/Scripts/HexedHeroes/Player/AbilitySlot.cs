using HexedHeroes.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.Player
{
    public class AbilitySlot : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text abilityCooldown;
        [SerializeField] private TMP_Text abilityName;
        [SerializeField] private GameObject longPressArea;
    
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
            {
                longPressArea.SetActive(false);
                button.enabled = false;
            }
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
            longPressArea.SetActive(true);
        }
    
        public void ResetAbilitySlot()
        {
            if (button.interactable || !isCooldownActive)
                return;
        
            EndCooldown();
            Activate();
            UpdateCooldownText();
            longPressArea.SetActive(false);
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

            ResetAbilitySlot();
        }
    
        private void OnTurnEnded()
        {
            if (!isCooldownActive)
                return;
        
            if (currentCooldown > 0)
                currentCooldown--;
        
            UpdateCooldownText();
        
            if (ability.isInterrupt && currentCooldown == 0)
                ResetAbilitySlot();
        }
    }
}
