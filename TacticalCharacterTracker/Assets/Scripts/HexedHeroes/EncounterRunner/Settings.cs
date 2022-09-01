using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class Settings : MonoBehaviour
    {
        public void ToggleFullscreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
