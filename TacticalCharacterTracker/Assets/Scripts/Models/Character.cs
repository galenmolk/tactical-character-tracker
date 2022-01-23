using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "Custom Assets/Character", order = 2)]
public class Character : GameElement
{
    public int totalDefense;
    public int totalHealth;
    public int totalSpeed;
    
    public PassiveAbility[] passiveAbilities;
    public CooldownAbility[] cooldownAbilities;
}
