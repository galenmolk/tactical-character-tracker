using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Custom Assets/Character", order = 2)]
public class Character : GameElement
{
    public int totalDefense;
    public int totalHealth;
    public int totalSpeed;
    
    private int currentDefense;
    private int currentHealth;
    private int currentSpeed;
    
    public Ability[] abilities;
    public Token[] tokens;
}
