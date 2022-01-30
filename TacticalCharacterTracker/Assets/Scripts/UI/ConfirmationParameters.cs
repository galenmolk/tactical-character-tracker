using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Confirmation Parameters", menuName = "Custom/Confirmation Parameters")]
public class ConfirmationParameters : ScriptableObject
{
    public string Prompt => prompt;
    public string Description => description;
    public string ConfirmButtonText => confirmButtonText;
    public ColorBlock ConfirmButtonColorBlock => confirmButtonColorBlock;
    public string DenyButtonText => denyButtonText;
    public ColorBlock DenyButtonColorBlock => denyButtonColorBlock;

    public void InvokeConfirmationAction()
    {
        action.Invoke();
    }

    public void SetConfirmationAction(UnityAction unityAction)
    {
        action = unityAction;
    }
    
    [Header("Confirmation Text")]
    [SerializeField] private string prompt;
    [SerializeField] private string description;
    
    [Space(10)]
    [Header("Confirm Button")]
    [SerializeField] private string confirmButtonText;
    [SerializeField] private ColorBlock confirmButtonColorBlock;

    [Space(10)]
    [Header("Deny Button")]
    [SerializeField] private string denyButtonText;
    [SerializeField] private ColorBlock denyButtonColorBlock;

    private UnityAction action;
}
