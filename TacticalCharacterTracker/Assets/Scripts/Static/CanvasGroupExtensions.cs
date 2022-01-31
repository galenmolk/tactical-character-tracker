using UnityEngine;

public static class CanvasGroupExtensions
{
    public static void SetIsActive(this CanvasGroup canvasGroup, bool isActive)
    {
        canvasGroup.alpha = isActive ? 1f : 0f;
        canvasGroup.interactable = isActive;
        canvasGroup.blocksRaycasts = isActive;
    }
}
