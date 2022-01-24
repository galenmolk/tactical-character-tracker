using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Button startTurnButton;
    [SerializeField] private Button endTurnButton;
    
    public void StartTurn()
    {
        startTurnButton.gameObject.SetActive(false);
        endTurnButton.gameObject.SetActive(true);
        MessageCenter.InvokeTurnStarted();
    }

    public void EndTurn()
    {
        startTurnButton.gameObject.SetActive(true);
        endTurnButton.gameObject.SetActive(false);
        MessageCenter.InvokeTurnEnded();
    }

    private void Awake()
    {
        MessageCenter.SubscribeCharacterLoaded(_ => Initialize());
        Initialize();
    }

    private void OnDestroy()
    {
        MessageCenter.UnsubscribeCharacterLoaded(_ => Initialize());
    }

    private void Initialize()
    {
        startTurnButton.gameObject.SetActive(true);
        endTurnButton.gameObject.SetActive(false);
    }
}
