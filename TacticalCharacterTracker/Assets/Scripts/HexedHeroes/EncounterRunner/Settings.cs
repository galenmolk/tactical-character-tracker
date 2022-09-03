using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class Settings : MonoBehaviour
    {
        public void ToggleFullscreen(bool value)
        {
            SetMode(value ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed);
        }

        private void SetMode(FullScreenMode mode)
        {
            Screen.fullScreenMode = mode;
            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, mode);
        }
    }
}
