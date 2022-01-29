using System.Collections.Generic;
using UnityEngine;

public static class YieldRegistry
{
    public static WaitForEndOfFrame WaitForEndOfFrame { get; } = new WaitForEndOfFrame();
    public static WaitForFixedUpdate waitForFixedUpdate { get; } = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> timeIntervalRegistry = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        timeIntervalRegistry.TryGetValue(seconds, out WaitForSeconds yield); 
        return yield ?? RegisterNewYield(seconds);
    }

    private static WaitForSeconds RegisterNewYield(float seconds)
    {
        WaitForSeconds yield = new WaitForSeconds(seconds);
        timeIntervalRegistry.Add(seconds, yield);
        return yield;
    }
}
