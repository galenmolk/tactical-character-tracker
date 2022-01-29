using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image barBackground;
    [SerializeField] private float fillDurationInSeconds;
    [SerializeField] private float preFillDelay;
    [SerializeField] private float postFillDelay;

    private const float MIN_VALUE = 0f;
    private const float MAX_VALUE = 1f;
    
    private float totalSliderTime =0f;

    private void Awake()
    {
        barBackground.material.SetFloat(ShaderProps.progressBarValue, MIN_VALUE);
        StartCoroutine(BeginFill());
    }

    private IEnumerator BeginFill()
    {
        yield return YieldRegistry.WaitForSeconds(preFillDelay);
        yield return StartCoroutine(FillBar());
        yield return YieldRegistry.WaitForSeconds(postFillDelay);
        gameObject.SetActive(false);
    }
    
    private IEnumerator FillBar()
    {
        while (totalSliderTime < fillDurationInSeconds)
        {
            totalSliderTime += Time.deltaTime;
            float value = Mathf.Lerp(MIN_VALUE, MAX_VALUE, totalSliderTime / fillDurationInSeconds);
            barBackground.material.SetFloat(ShaderProps.progressBarValue, value);
            yield return YieldRegistry.WaitForEndOfFrame;
        }
    }
}
