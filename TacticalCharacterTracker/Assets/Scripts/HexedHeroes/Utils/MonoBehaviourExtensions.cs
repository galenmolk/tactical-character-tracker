using System;
using System.Collections;
using UnityEngine;

namespace HexedHeroes.Utils
{
    public static class MonoBehaviourExtensions
    {
        public static IEnumerator CoroutineCallback(this MonoBehaviour monoBehaviour, IEnumerator coroutine, Action callback)
        {
            yield return monoBehaviour.StartCoroutine(coroutine);
            callback();
        }
    
        public static void ExecuteAfterDelay(this MonoBehaviour monoBehaviour, float delay, Action callback)
        {
            monoBehaviour.StartCoroutine(DelayCallbackByYieldInstruction(YieldRegistry.WaitForSeconds(delay), callback));
        }

        public static void DelayExecutionUntil(this MonoBehaviour monoBehaviour, Func<bool> predicate, Action callback)
        {
            monoBehaviour.StartCoroutine(DelayCallbackByPredicate(predicate, callback));
        }

        public static void DelayExecutionUntilEndOfFrame(this MonoBehaviour monoBehaviour, Action callback)
        {
            monoBehaviour.StartCoroutine(DelayCallbackByYieldInstruction(YieldRegistry.WaitForEndOfFrame, callback));
        }

        private static IEnumerator DelayCallbackByPredicate(Func<bool> predicate, Action callback)
        {
            yield return YieldRegistry.WaitUntil(predicate);
            callback?.Invoke();
        }
    
        private static IEnumerator DelayCallbackByYieldInstruction(YieldInstruction yieldInstruction, Action callback)
        {
            Debug.Log("DelayCallbackByYieldInstruction");
            yield return yieldInstruction;
            Debug.Log("Callback");
            callback?.Invoke();
        }
    }
}