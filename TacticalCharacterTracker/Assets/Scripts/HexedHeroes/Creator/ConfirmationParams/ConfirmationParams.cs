using UnityEngine;
using UnityEngine.Events;

public abstract class ConfirmationParams
{
    public string HeaderText => headerText;
    public string DescriptionText => descriptionText;
    public string ConfirmButtonText => confirmButtonText;
    public string DenyButtonText => denyButtonText;
    public Color? ConfirmButtonColor => confirmButtonColor;
    public Color? DenyButtonColor => denyButtonColor;
    
    public UnityAction Action => action;

    protected string headerText;
    protected string descriptionText;
    protected string confirmButtonText;
    protected string denyButtonText;
    protected Color? confirmButtonColor;
    protected Color? denyButtonColor;
    protected UnityAction action;
}

