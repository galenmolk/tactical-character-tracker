using System;
using System.Collections.Generic;
using UnityEngine;

public static class YieldRegistry
{
    public static WaitForEndOfFrame WaitForEndOfFrame { get; } = new();
    public static WaitForFixedUpdate WaitForFixedUpdate { get; } = new();

    private static readonly Dictionary<float, WaitForSeconds> TimeIntervalRegistry = new();
    private static readonly Dictionary<Func<bool>, WaitUntil> PredicateRegistry = new();

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        TimeIntervalRegistry.TryGetValue(seconds, out WaitForSeconds yield); 
        return yield ?? RegisterNewWaitForSeconds(seconds);
    }

    public static WaitUntil WaitUntil(Func<bool> predicate)
    {
        PredicateRegistry.TryGetValue(predicate, out WaitUntil yield);
        return yield ?? RegisterNewWaitUntil(predicate);
    }

    private static WaitUntil RegisterNewWaitUntil(Func<bool> predicate)
    {
        WaitUntil yield = new WaitUntil(predicate);
        PredicateRegistry.Add(predicate, yield);
        return yield;
    }

    private static WaitForSeconds RegisterNewWaitForSeconds(float seconds)
    {
        WaitForSeconds yield = new WaitForSeconds(seconds);
        TimeIntervalRegistry.Add(seconds, yield);
        return yield;
    }
}