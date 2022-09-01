using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.EncounterRunner
{
    public class FullScreenToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        public void HandleFullscreenToggled()
        {
            bool isFullscreen = Screen.fullScreen;
            if (isFullscreen != toggle.isOn)
            {
                SetToggleWithState(isFullscreen);
            }
        }
        
        private void Start()
        {
            SetToggleWithState(Screen.fullScreen);
        }

        private void SetToggleWithState(bool state)
        {
            toggle.SetIsOnWithoutNotify(state);
        }
    }
}
