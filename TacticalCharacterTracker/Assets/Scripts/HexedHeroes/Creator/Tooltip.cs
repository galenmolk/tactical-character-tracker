using HexedHeroes.Utils;
using MolkExtras;
using TMPro;
using UnityEngine;

namespace HexedHeroes.Creator
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text tooltipText;

        [SerializeField] private float fadeDuration = 0.1f;
    
        private RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = transform as RectTransform;

                return rectTransform;
            }
        }

        private RectTransform rectTransform;
    
        private TooltipElementEventArgs currentArgs;

        private void Activate(TooltipElementEventArgs eventArgs)
        {
            if (currentArgs?.Element == eventArgs.Element)
                return;
        
            currentArgs = eventArgs;
            tooltipText.text = eventArgs.Element.Text;
            this.ExecuteAtEndOfFrame(() =>
            {
                var size = RectTransform.sizeDelta * CanvasController.Instance.ScaleFactor;

                var tooltipPosition = (Vector2)Input.mousePosition + size * 0.5f;

                RectTransform.position = tooltipPosition;
            
                canvasGroup.SetIsVisible(true, fadeDuration);
            });
        }

        private void Deactivate(TooltipElementEventArgs eventArgs)
        {
            if (currentArgs?.Element != eventArgs.Element)
                return;

            currentArgs = null;
            canvasGroup.SetIsVisible(false, fadeDuration);
        }
    
        private void OnEnable()
        {
            TooltipElement.PointerEnter += Activate;
            TooltipElement.PointerExit += Deactivate;
        }

        private void OnDisable()
        {
            TooltipElement.PointerEnter -= Activate;
            TooltipElement.PointerExit -= Deactivate;
        }
    }
}
