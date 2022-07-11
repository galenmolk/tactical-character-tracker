using HexedHeroes.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexedHeroes.Utils
{
    public class GoToLoad : MonoBehaviour
    {
        private void Awake()
        {
            if (ActiveSession.AvailableCharacters == null)
                SceneManager.LoadScene(SceneKeys.LOAD);
        }
    }
}
