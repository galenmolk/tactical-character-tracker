using DG.Tweening;
using UnityEngine;

namespace HexedHeroes.Utils
{
    public static class CanvasGroupExtensions
    {
        public static void SetIsActive(this CanvasGroup canvasGroup, bool isActive)
        {
            canvasGroup.alpha = isActive ? 1f : 0f;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }
    
        public static void SetIsVisible(this CanvasGroup canvasGroup, bool isVisible, float duration = 0f)
        {
            var target = isVisible ? 1f : 0f;
        
            if (duration <= 0f)
            {
                canvasGroup.alpha = target;
                return;
            }

            canvasGroup.DOFade(target, duration);
        }
    }
}
