using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFader : MonoBehaviour, IPointerClickHandler
{
    private static readonly int FadeX = Shader.PropertyToID("_FadeX");
    private static readonly int FadeWidth = Shader.PropertyToID("_FadeWidth");

    [SerializeField, Range(0, 1)] private float targetFadeWidth = 0.2f;
    [SerializeField, Min(0)] private float fadeDurationInSeconds = 1f;
    
    private Material material;
    private float fadeWidthDuration;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(Fade());
    }
    
    private void Awake()
    {
        material = GetComponent<Image>().material;
        fadeWidthDuration = fadeDurationInSeconds * targetFadeWidth;

        Initialize();
    }

    private void Initialize()
    {
        material.SetFloat(FadeX, 1f);
        material.SetFloat(FadeWidth, 0f);
    }

    private IEnumerator Fade()
    {
        Initialize();

        yield return StartCoroutine(LerpMaterialFloatProperty(FadeWidth, targetFadeWidth, fadeWidthDuration));
        yield return StartCoroutine(LerpMaterialFloatProperty(FadeX, 0f, fadeDurationInSeconds));
        yield return StartCoroutine(LerpMaterialFloatProperty(FadeWidth, 0f, fadeWidthDuration));
    }

    private IEnumerator LerpMaterialFloatProperty(int id, float targetValue, float duration)
    {
        float time = 0f;
        float currentValue = material.GetFloat(id);
        while (time < duration)
        {
            material.SetFloat(id, Mathf.Lerp(currentValue, targetValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        material.SetFloat(id, targetValue);
    }
}
