using System;
using HexedHeroes.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BurningTokenManager : MonoBehaviour
{
    private const int BURNING_TOKEN_DAMAGE_AMOUNT = 1;
    
    [SerializeField] private Image tokenImage;
    [SerializeField] private TMP_Text tokenCounterText;
    [SerializeField] private Button subtractButton;

    private int tokenCount;
    
    public void Add()
    {
        tokenCount++;
        UpdateUI();
    }

    public void Subtract()
    {
        tokenCount--;
        UpdateUI();
    }

    private void Awake()
    {
        UpdateUI();
        TurnManager.Instance.SubscribeTurnEnded(ActivateBurningToken);
    }

    private void UpdateUI()
    {
        subtractButton.interactable = tokenCount > 0;
        tokenImage.color = tokenCount > 0 ? Color.white : Color.gray;
        tokenCounterText.text = tokenCount.ToString();
    }

    private void Reset()
    {
        tokenCount = 0;
        UpdateUI();
    }
    
    private void ActivateBurningToken()
    {
        if (tokenCount <= 0)
            return;
        
        Subtract();
        CharacterSheet.Instance.TakeDamage(BURNING_TOKEN_DAMAGE_AMOUNT);
    }
}
