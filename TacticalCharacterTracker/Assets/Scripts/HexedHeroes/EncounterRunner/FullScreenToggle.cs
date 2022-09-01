using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.EncounterRunner
{
    public class FullScreenToggle : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        public void HandleFullscreenChanged(bool value)
        {
            if (toggle.isOn != value)
            {
                SetToggleWithState(value);
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
