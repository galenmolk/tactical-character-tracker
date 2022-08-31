using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class Settings : MonoBehaviour
    {
        public void ToggleFullscreen()
        {
            Debug.Log("Toggle Fullscreen");
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
