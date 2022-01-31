using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ConfirmationParameters : ScriptableObject
{
    public string Prompt => prompt;
    public string Description => description;
    public string ConfirmButtonText => confirmButtonText;
    public ColorBlock ConfirmBlock => confirmBlock;
    public string DenyButtonText => denyButtonText;
    public ColorBlock DenyBlock => denyBlock;

    public abstract void InvokeConfirmationAction();
    
    [Header("Confirmation Text")]
    [SerializeField] private string prompt;
    [SerializeField] private string description;
    
    [Space(10)]
    [Header("Confirm Button")]
    [SerializeField] private string confirmButtonText;
    [SerializeField] private ColorBlock confirmBlock;

    [Space(10)]
    [Header("Deny Button")]
    [SerializeField] private string denyButtonText;
    [SerializeField] private ColorBlock denyBlock;

    private void Awake()
    {
        confirmBlock.colorMultiplier = Mathf.Clamp(confirmBlock.colorMultiplier, 1f, 5f);
        denyBlock.colorMultiplier = Mathf.Clamp(denyBlock.colorMultiplier, 1f, 5f);
    }
}
