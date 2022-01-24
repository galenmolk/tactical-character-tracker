using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text failureText;
    [SerializeField] private string[] textFrames;
    [Min(0)] [SerializeField] private float framesPerSecond;

    private Coroutine animation;
    private WaitForSeconds frameDelay;
    private readonly WaitForSeconds errorDelay = new WaitForSeconds(3f);
    private int textFrameIndex;
    private int totalFrames;
    
    public void StartAnimation()
    {
        frameDelay = new WaitForSeconds(1f / framesPerSecond);
        textFrameIndex = 0;
        text.text = textFrames[0];
        totalFrames = textFrames.Length;
        animation = StartCoroutine(Animation());
    }

    public void StopAnimation()
    {
        if (animation != null)
            StopCoroutine(animation);
        
        gameObject.SetActive(false);
    }

    public IEnumerator DisplayError()
    {
        StopAnimation();
        failureText.gameObject.SetActive(true);
        yield return errorDelay;
        failureText.gameObject.SetActive(false);
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            yield return frameDelay;
            if (textFrameIndex == totalFrames - 1)
                textFrameIndex = 0;
            else
                textFrameIndex++;
            
            text.text = textFrames[textFrameIndex];
        }
    }
}