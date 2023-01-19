using DG.Tweening;
using UnityEngine;

public class AutoFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField, Min(0f)] private float fadeDuration = 1f;
    [SerializeField] private bool autoFadeInOnStart = true;

    public void FadeIn(TweenCallback callback = null)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, fadeDuration).OnComplete(callback);
    }

    public void FadeOut(TweenCallback callback = null)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0f, fadeDuration).OnComplete(callback);
    }

    private void Start()
    {
        if (autoFadeInOnStart && canvasGroup != null)
        {
            FadeIn();
        }
    }
}
