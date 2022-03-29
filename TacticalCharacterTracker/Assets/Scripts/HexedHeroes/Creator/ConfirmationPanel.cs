using HexedHeroes.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationPanel : Singleton<ConfirmationPanel>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text confirmButtonText;
    [SerializeField] private TMP_Text denyButtonText;
    [SerializeField] private Image confirmButtonImage;
    [SerializeField] private Image denyButtonImage;
    
    private ConfirmationParams confirmationParams;
    
    public void Open(ConfirmationParams _confirmationParams)
    {
        confirmationParams = _confirmationParams;
        InitializeUI();
        canvasGroup.SetIsActive(true);
    }

    public void Confirm()
    {
        confirmationParams.Action();
        Close();
    }

    public void Deny()
    {
        canvasGroup.SetIsActive(false);
    }

    public void Close()
    {
        canvasGroup.SetIsActive(false);
    }

    protected override void OnAwake()
    {
        Close();
    }

    private void InitializeUI()
    {
        headerText.text = confirmationParams.HeaderText;
        descriptionText.text = confirmationParams.DescriptionText;
        confirmButtonText.text = confirmationParams.ConfirmButtonText;
        denyButtonText.text = confirmationParams.DenyButtonText;
        
        if (confirmationParams.ConfirmButtonColor.HasValue)
            confirmButtonImage.color = confirmationParams.ConfirmButtonColor.Value;
        
        if (confirmationParams.DenyButtonColor.HasValue)
            denyButtonImage.color = confirmationParams.DenyButtonColor.Value;
    }
}
