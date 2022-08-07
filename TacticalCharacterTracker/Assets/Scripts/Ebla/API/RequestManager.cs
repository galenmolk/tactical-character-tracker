using System.Collections;
using System.Collections.Generic;
using Ebla.Models;
using MolkExtras;
using UnityEngine;

namespace Ebla.API
{
    public class RequestManager : Singleton<RequestManager>
    {
        private readonly Queue<IEnumerator> coroutines = new();

        public void CreateConfig(string route, BaseConfig baseConfig)
        {
            Debug.Log($"RequestManager.CreateConfig {route}, {baseConfig.Name}");
            EnqueueRequest(StartLinkedRequest(ApiUtils.CreateConfig(route, baseConfig)));
            DequeueRequest();
        }

        public void AddToQueue(IEnumerator coroutine)
        {
            EnqueueRequest(StartLinkedRequest(coroutine));
            DequeueRequest();
        }
        
        private void EnqueueRequest(IEnumerator coroutine)
        {
            Debug.Log($"EnqueueRequest {coroutines.Count}");
            coroutines.Enqueue(coroutine);
        }

        private void DequeueRequest()
        {
            Debug.Log($"DequeueRequest {coroutines.Count}");

            if (coroutines.TryDequeue(out IEnumerator coroutine))
            {
                Debug.Log($"TryDequeue Success {coroutines.Count}");
                StartCoroutine(coroutine);
            }
        }

        private IEnumerator StartLinkedRequest(IEnumerator coroutine)
        {
            Debug.Log($"StartLinkedRequest {coroutines.Count}");
            yield return StartCoroutine(this.ExecuteAfterCoroutine(coroutine, DequeueRequest));
        }
    }
}
