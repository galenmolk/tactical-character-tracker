using System.Collections;
using Ebla.API;
using Ebla.Models;
using UnityEngine;

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
                configs => 
                {
                    foreach (var config in configs)
                    {
                        Debug.Log($"Config Downloaded {config.Value.Name}");
                    }
                    
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
