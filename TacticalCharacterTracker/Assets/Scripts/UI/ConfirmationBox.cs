using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationBox : Singleton<ConfirmationBox>
{
    [Header("Confirmation Text")]
    [SerializeField] private TMP_Text prompt;
    [SerializeField] private TMP_Text description;
    
    [Space(10)]
    [Header("Confirm Button")]
    [SerializeField] private TMP_Text confirmButtonText;
    [SerializeField] private Button confirmButton;

    [Space(10)]
    [Header("Deny Button")]
    [SerializeField] private TMP_Text denyButtonText;
    [SerializeField] private Button denyButton;
    
    [Space(10)]
    [SerializeField] private CanvasGroup canvasGroup;
    
    private ConfirmationParameters parameters;
    
    public void Open(ConfirmationParameters _parameters)
    {
        parameters = _parameters;
        OverlayCloseButton.Instance.Open();
        SetProperties();
        canvasGroup.SetIsActive(true);
    }

    public void Close()
    {
        OverlayCloseButton.Instance.Close();
        canvasGroup.SetIsActive(false);
    }

    public void Confirm()
    {
        Close();
        parameters.InvokeConfirmationAction();
    }

    public void Deny()
    {
        Close();
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        Close();
    }

    private void SetProperties()
    {
        description.text = parameters.Description;
        prompt.text = parameters.Prompt;
        confirmButtonText.text = parameters.ConfirmButtonText;
        confirmButton.colors = parameters.ConfirmBlock;
        denyButtonText.text = parameters.DenyButtonText;
        denyButton.colors = parameters.DenyBlock;
    }
}
