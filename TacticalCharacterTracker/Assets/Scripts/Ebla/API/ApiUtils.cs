using System;
using System.Collections;
using System.Collections.Generic;
using Ebla.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Ebla.API
{
    public class ApiUtils : MonoBehaviour
    {
        public const string BASE_URL = "https://shielded-hollows-53494.herokuapp.com";
        public const string ABILITY_ROUTE = BASE_URL + "/abilities";
        
        private const string CONTENT_TYPE_HEADER_KEY = "Content-Type";
        private const string JSON_HEADER_VALUE = "application/json";

        private static void SetDefaultHeaders(UnityWebRequest request)
        {
            request.SetRequestHeader(CONTENT_TYPE_HEADER_KEY, JSON_HEADER_VALUE);
        }

        public static IEnumerator DownloadConfigs<TConfig>(string route, Action<Dictionary<string, TConfig>> callback)
            where TConfig : BaseConfig
        {
            using UnityWebRequest request = NewGetRequest(ABILITY_ROUTE);
            SetDefaultHeaders(request);
            
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"DownloadConfigs Error: {request.error}");
                yield break;
            }
            
            Dictionary<string, TConfig> configs = JsonConvert.DeserializeObject<Dictionary<string, TConfig>>(request.downloadHandler.text);
            if (configs != null)
            {
                callback?.Invoke(configs);
            }
        }

        private static UnityWebRequest NewGetRequest(string route)
        {           
            return new UnityWebRequest(
                route, 
                UnityWebRequest.kHttpVerbGET, 
                new DownloadHandlerBuffer(), 
                null);
        }
    }
}
