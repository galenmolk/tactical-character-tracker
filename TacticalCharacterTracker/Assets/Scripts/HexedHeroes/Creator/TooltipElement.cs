using System.Collections;
using HexedHeroes.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void TooltipElementEvent(TooltipElementEventArgs eventArgs);

    public static event TooltipElementEvent PointerEnter;
    public static event TooltipElementEvent PointerExit;

    public string Text => text;
    
    private const float APPEAR_DELAY = 1f;
    
    [SerializeField] private string text;

    [SerializeField] private bool useAppearDelayOverride;
    [SerializeField] private float appearDelayOverride;

    private float appearDelay;
    
    private TooltipElementEventArgs args;
    private bool isAppearing;
    private Coroutine appearingCoroutine;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (appearingCoroutine != null)
            StopCoroutine(appearingCoroutine);
        
        isAppearing = true;
        appearingCoroutine = StartCoroutine(Appear(eventData));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (appearingCoroutine != null)
            StopCoroutine(appearingCoroutine);
        
        isAppearing = false;
        args.UpdateEventData(eventData);
        PointerExit?.Invoke(args);
    }

    private IEnumerator Appear(PointerEventData eventData)
    {
        yield return YieldRegistry.WaitForSeconds(appearDelay);
        
        if (!isAppearing)
            yield break;
        
        args.UpdateEventData(eventData);
        PointerEnter?.Invoke(args);
    }

    private void Awake()
    {
        args = new TooltipElementEventArgs(this);
        appearDelay = useAppearDelayOverride ? appearDelayOverride : APPEAR_DELAY;
    }
}
