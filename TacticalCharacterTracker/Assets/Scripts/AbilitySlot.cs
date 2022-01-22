using UnityEngine;

public class AbilitySlot : AbilityUI
{
    [SerializeField] private Ability testAbility;
    
    private Ability ability;

    public void Initialize(Ability _ability)
    {
        ability = _ability;
        DisplayAbilityInfo(ability);
    }

    public void InfoButtonPressed()
    {
        Debug.Log("InfoButtonPressed");
        MessageCenter.InvokeAbilityInfoButtonPressed(ability);
    }
    
    private void Awake()
    {
        Initialize(testAbility);
    }
}
