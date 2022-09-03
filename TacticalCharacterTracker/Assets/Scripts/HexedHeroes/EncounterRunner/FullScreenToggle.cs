using MolkExtras;
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

        private void SetToggleWithState(bool state)
        {
            toggle.SetIsOnWithoutNotify(state);
        }

        private void Start()
        {
            this.ExecuteAfterDelay(1f, () =>
            {
                SetToggleWithState(Screen.fullScreen);
            });
        }
    }
}
