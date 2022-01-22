using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BurningTokenManager : MonoBehaviour
{
    [SerializeField] private Image tokenImage;
    [SerializeField] private TMP_Text tokenCounterText;
    [SerializeField] private Button addButton;
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

    private void UpdateUI()
    {
        subtractButton.interactable = tokenCount > 0;
        tokenImage.color = tokenCount > 0 ? Color.white : Color.gray;
        tokenCounterText.text = tokenCount.ToString();
    }

    private void OnEnable()
    {
        UpdateUI();
        MessageCenter.SubscribeTurnEnded(ActivateBurningToken);
    }

    private void OnDisable()
    {
        MessageCenter.UnsubscribeTurnEnded(ActivateBurningToken);
    }

    private void ActivateBurningToken()
    {
        if (tokenCount <= 0)
            return;
        
        Subtract();
        MessageCenter.InvokeBurningTokenActivated();
    }
}
