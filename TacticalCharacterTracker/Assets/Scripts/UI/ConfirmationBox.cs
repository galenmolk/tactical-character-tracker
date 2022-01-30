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
    
    private ConfirmationParameters parameters;
    
    public void Open(ConfirmationParameters parameters)
    {
        SetProperties(parameters);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
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

    private void SetProperties(ConfirmationParameters parameters)
    {
        description.text = parameters.Description;
        prompt.text = parameters.Prompt;
        confirmButtonText.text = parameters.ConfirmButtonText;
        confirmButton.colors = parameters.ConfirmButtonColorBlock;
        denyButtonText.text = parameters.DenyButtonText;
        denyButton.colors = parameters.DenyButtonColorBlock;
    }
}
