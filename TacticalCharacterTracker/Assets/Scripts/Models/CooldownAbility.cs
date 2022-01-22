using UnityEngine;

[CreateAssetMenu(fileName = "New Cooldown Ability", menuName = "Custom Assets/Cooldown Ability", order = 1)]
public class CooldownAbility : Ability
{
    public override string CooldownText => $"{cooldownTurns} Turns";

    [Min(0)] [SerializeField] private int cooldownTurns;
}
