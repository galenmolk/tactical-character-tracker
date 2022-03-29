using System.Collections;
using HexedHeroes.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image barBackground;
    [SerializeField] private float fillDurationInSeconds;
    [SerializeField] private float preFillDelay;

    private const float MIN_VALUE = 0f;
    private const float MAX_VALUE = 1f;
    
    private float startTime = 0f;
    
    private void Awake()
    {
        barBackground.material.SetFloat(ShaderProps.ProgressBarValue, MIN_VALUE);
    }

    public IEnumerator Fill()
    {
        yield return YieldRegistry.WaitForSeconds(preFillDelay);
        yield return StartCoroutine(FillBar());
    }
    
    private IEnumerator FillBar()
    {
        startTime = Time.time;
        float totalTime = 0f;
        while (totalTime < fillDurationInSeconds)
        {
            totalTime = Time.time - startTime;
            float value = Mathf.Lerp(MIN_VALUE, MAX_VALUE, totalTime / fillDurationInSeconds);
            barBackground.material.SetFloat(ShaderProps.ProgressBarValue, value);
            yield return null;
        }
    }
}
