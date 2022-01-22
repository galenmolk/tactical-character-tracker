using UnityEngine;

[CreateAssetMenu(fileName = "New Cooldown Ability", menuName = "Custom Assets/Cooldown Ability", order = 1)]
public class CooldownAbility : Ability
{
    private const string TURNS_PLURAL = "Turns";
    private const string TURNS_SINGULAR = "Turn";
    
    private string GetTurnText(int cooldown)
    {
        return cooldown == 1 ? TURNS_SINGULAR : TURNS_PLURAL;
    }
    
    public int CooldownTurns => cooldownTurns;
    
    [Min(0)] [SerializeField] private int cooldownTurns;
    
    public override string GetCooldownDescription()
    {
        return $"{cooldownTurns} {GetTurnText(cooldownTurns)}";
    }

    public string GetCurrentCooldownDescription(int currentCooldown)
    {
        return $"{currentCooldown} {GetTurnText(currentCooldown)} Left";
    }
}
