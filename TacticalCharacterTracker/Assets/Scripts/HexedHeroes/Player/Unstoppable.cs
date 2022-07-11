using HexedHeroes.Player;
using UnityEngine;

public class Unstoppable : MonoBehaviour
{
    private const int DEFENSE_GAIN_AMOUNT = 2;
    
    private void Awake()
    {
        TurnManager.Instance.SubscribeTurnStarted(GainTwoHealth);
    }

    private void GainTwoHealth()
    {
        CharacterSheet.Instance.GainDefense(DEFENSE_GAIN_AMOUNT);
    }
}
