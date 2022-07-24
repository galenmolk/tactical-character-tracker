using UnityEngine;

namespace Ebla.Editing
{
    public class EditingControllerSettings : MonoBehaviour
    {
        public float OpenXPos => openXPos;
        public float CloseXPos => closeXPos;
        public float ToggleDuration => toggleDuration;
        public RectTransform ControlsParent => controlsParent;
        public RectTransform ControlsArea => controlsArea;
        public GameObject Overlay => overlay;

        [SerializeField] private float openXPos;
        [SerializeField] private float closeXPos;
        [SerializeField] private float toggleDuration;
        [SerializeField] private RectTransform controlsParent;
        [SerializeField] private RectTransform controlsArea;
        [SerializeField] private GameObject overlay;
    }
}
