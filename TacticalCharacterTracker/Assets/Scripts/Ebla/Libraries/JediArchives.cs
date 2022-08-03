using System;
using System.Collections;
using System.Collections.Generic;
using Ebla.API;
using Ebla.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Ebla.Libraries
{
    public class JediArchives : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(LoadSequence());
        }

        private IEnumerator LoadSequence()
        {
            yield return StartCoroutine(ApiUtils.DownloadConfigs<AbilityConfig>(ApiUtils.ABILITY_ROUTE, 
                configs => {
                AbilityLibrarian.Instance.LoadInConfigs(configs);
            }));
            
            AbilityLibrarian.Instance.InitializeFolders();
            ScopeController.Instance.LoadRoot();
        }

        private void Start()
        {
            // FolderLibrarian.Instance.InitializeFolders();
            // DungeonLibrarian.Instance.InitializeFolders();
            // EncounterLibrarian.Instance.InitializeFolders();
            // EnemyLibrarian.Instance.InitializeFolders();
            // HeroLibrarian.Instance.InitializeFolders();
            // AbilityLibrarian.Instance.InitializeFolders();
        }

        
    }
}
