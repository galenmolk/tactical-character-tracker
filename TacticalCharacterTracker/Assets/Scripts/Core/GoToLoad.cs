using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLoad : MonoBehaviour
{
    private void Awake()
    {
        if (ActiveSession.AvailableCharacters == null)
            SceneManager.LoadScene(Scenes.LOAD);
    }
}
