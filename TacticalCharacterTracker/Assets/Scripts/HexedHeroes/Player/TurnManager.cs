using HexedHeroes.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnManager : Singleton<TurnManager>
{
    private readonly UnityEvent TurnStarted = new UnityEvent();
    private readonly UnityEvent TurnEnded = new UnityEvent();
    
    [SerializeField] private Button startTurnButton;
    [SerializeField] private Button endTurnButton;
    
    public void SubscribeTurnStarted(UnityAction action) => TurnStarted.AddListener(action);
    
    public void SubscribeTurnEnded(UnityAction action) => TurnEnded.AddListener(action);

    public void StartTurn()
    {
        startTurnButton.gameObject.SetActive(false);
        endTurnButton.gameObject.SetActive(true);
        TurnStarted.Invoke();
    }

    public void EndTurn()
    {
        startTurnButton.gameObject.SetActive(true);
        endTurnButton.gameObject.SetActive(false);
        TurnEnded.Invoke();
    }
}
