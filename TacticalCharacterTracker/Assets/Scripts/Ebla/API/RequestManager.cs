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

        private Coroutine activeRequest;
        
        public void AddToQueue(IEnumerator coroutine)
        {
            EnqueueRequest(StartLinkedRequest(coroutine));
            
            if (activeRequest == null)
            {
                DequeueRequest();
            }
        }
        
        private void EnqueueRequest(IEnumerator coroutine)
        {
            coroutines.Enqueue(coroutine);
        }

        private void DequeueRequest()
        {
            if (!coroutines.TryDequeue(out IEnumerator coroutine))
            {
                activeRequest = null;
                return;
            }
            
            activeRequest = StartCoroutine(coroutine);
        }

        private IEnumerator StartLinkedRequest(IEnumerator coroutine)
        {
            yield return StartCoroutine(this.ExecuteAfterCoroutine(coroutine, DequeueRequest));
        }
    }
}
