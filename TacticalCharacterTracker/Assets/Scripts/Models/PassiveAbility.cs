using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Ability", menuName = "Custom Assets/Passive Ability", order = 0)]
public class PassiveAbility : Ability
{
    private const string PASSIVE_TEXT = "(Passive)";

    public override string GetCooldownDescription()
    {
        return PASSIVE_TEXT;
    }
}
