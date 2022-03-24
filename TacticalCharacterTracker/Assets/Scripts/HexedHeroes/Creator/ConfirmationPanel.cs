using TMPro;
using UnityEngine;

public class ConfirmationPanel : Singleton<ConfirmationPanel>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text confirmButtonText;
    [SerializeField] private TMP_Text denyButtonText;
    
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
    }
}
