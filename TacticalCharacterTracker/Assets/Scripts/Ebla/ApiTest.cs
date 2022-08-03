using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Ebla
{
    public class ApiTest : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(SendRequest());
        }

        private IEnumerator SendRequest()
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get("https://shielded-hollows-53494.herokuapp.com/users");
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Not success. {unityWebRequest.error}");
            }
            Debug.Log(unityWebRequest.responseCode);
            Debug.Log(unityWebRequest.downloadHandler.text);
            unityWebRequest.Dispose();
        }
    }
}
