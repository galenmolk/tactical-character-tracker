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
        public const string ABILITY_ROUTE = BASE_URL + "/abilities";
        
        private const string BASE_URL = "https://shielded-hollows-53494.herokuapp.com";
        private const string ACCEPT_HEADER_KEY = "Accept";
        private const string CONTENT_TYPE_HEADER_KEY = "Content-Type";
        private const string JSON_HEADER_VALUE = "application/json";

        public static IEnumerator CreateConfig(string route, BaseConfig baseConfig)
        {
            Debug.Log($"Post Config called for route {route}, config {baseConfig.Name}. Is Ability? {baseConfig is AbilityConfig}");
            using UnityWebRequest request = NewPostRequest(route, baseConfig);

            SetDefaultHeaders(request);
            SetAcceptHeader(request);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"PostConfig Error: {request.error}");
                yield break;
            }

            Debug.Log($"PostConfig Success: {request.downloadHandler.text} {request.responseCode}");
        }
        
        public static IEnumerator DownloadConfigs<TConfig>(string route, Action<Dictionary<string, TConfig>> callback)
            where TConfig : BaseConfig
        {
            using UnityWebRequest request = NewGetRequest(route);
            SetDefaultHeaders(request);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"DownloadConfigs Error: {request.error}");
                yield break;
            }

            Dictionary<string, TConfig> configs =
                JsonConvert.DeserializeObject<Dictionary<string, TConfig>>(request.downloadHandler.text);
            if (configs != null)
            {
                callback?.Invoke(configs);
            }
        }

        public static IEnumerator UpdateConfig(string route, BaseConfig baseConfig)
        {
            route = IdentifyRoute(route, baseConfig);
            Debug.Log($"UpdateConfig called for route {route}, config {baseConfig.Name}. Is Ability? {baseConfig is AbilityConfig}");

            using UnityWebRequest request = NewPutRequest(route, baseConfig);
            
            SetDefaultHeaders(request);
            SetAcceptHeader(request);
            
            yield return request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"UpdateConfig Error: {request.error}");
                yield break;
            }

            Debug.Log($"UpdateConfig Success: {request.downloadHandler.text} {request.responseCode}");
        }

        public static IEnumerator DeleteConfig(string route, BaseConfig baseConfig)
        {
            route = IdentifyRoute(route, baseConfig);
            Debug.Log($"DeleteConfig called for route {route}, config {baseConfig.Name}. Is Ability? {baseConfig is AbilityConfig}");

            using UnityWebRequest request = NewDeleteRequest(route);
            
            yield return request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"DeleteConfig Error: {request.error}");
                yield break;
            }

            Debug.Log($"DeleteConfig Success: {request.downloadHandler.text} {request.responseCode}");
        }

        private static void SetDefaultHeaders(UnityWebRequest request)
        {
            request.SetRequestHeader(CONTENT_TYPE_HEADER_KEY, JSON_HEADER_VALUE);
        }

        private static void SetAcceptHeader(UnityWebRequest request)
        {
            request.SetRequestHeader(ACCEPT_HEADER_KEY, JSON_HEADER_VALUE);
        }

        private static string IdentifyRoute(string route, BaseConfig baseConfig)
        {
            return $"{route}/{baseConfig.Id}";
        }
        
        private static UnityWebRequest NewPostRequest(string route, BaseConfig baseConfig)
        {
            return new UnityWebRequest(
                route, 
                UnityWebRequest.kHttpVerbPOST,
                new DownloadHandlerBuffer(),
                GetUploadHandlerForConfig(baseConfig));
        }
        
        private static UnityWebRequest NewGetRequest(string route)
        {
            return new UnityWebRequest(
                route,
                UnityWebRequest.kHttpVerbGET,
                new DownloadHandlerBuffer(),
                null);
        }

        private static UnityWebRequest NewPutRequest(string route, BaseConfig baseConfig)
        {
            return new UnityWebRequest(
                route, 
                UnityWebRequest.kHttpVerbPUT,
                new DownloadHandlerBuffer(),
                GetUploadHandlerForConfig(baseConfig));
        }

        private static UnityWebRequest NewDeleteRequest(string route)
        {
            return new UnityWebRequest(
                route, 
                UnityWebRequest.kHttpVerbDELETE, 
                new DownloadHandlerBuffer(), 
                null);
        }

        private static UploadHandlerRaw GetUploadHandlerForConfig(BaseConfig baseConfig)
        {
            string json = JsonConvert.SerializeObject(baseConfig);
            Debug.Log($"json: {json}");
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);
            return new UploadHandlerRaw(bytes);
        }
    }
}
