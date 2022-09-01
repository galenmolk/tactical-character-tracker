using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class Settings : MonoBehaviour
    {
        public void ToggleFullscreen(bool value)
        {
            Screen.fullScreen = value;
        }
    }
}
