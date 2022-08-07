using System;
using System.Collections;
using System.Globalization;
using Ebla.Utils;
using MolkExtras;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ebla.API
{
    public class ApiTestBehaviour : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                RequestManager.Instance.AddToQueue(LogTime());
            }
        }

        private IEnumerator LogTime()
        {
            float delay = Random.Range(1f, 5f);
            yield return YieldRegistry.WaitForSeconds(delay);
            Debug.Log($"LogTime after {delay}: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        }
    }
}
