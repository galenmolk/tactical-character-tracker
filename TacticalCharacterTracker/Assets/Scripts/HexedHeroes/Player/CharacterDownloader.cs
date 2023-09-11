using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gisty;
using HexedHeroes.Models;
using HexedHeroes.Utils;
using Newtonsoft.Json;
using Unity.RemoteConfig;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexedHeroes.Player
{
    public class CharacterDownloader : MonoBehaviour
    {
        [SerializeField] private bool loadLocalConfig;
        [SerializeField] private TextAsset fallbackCharacterListConfig;
        [SerializeField] private ProgressBar progressBar;
    
        private Coroutine progressFill;
    
        private struct UserAttributes { }

        private struct AppAttributes { }
    
        private readonly AppAttributes appParams = new();

        GistService characterService;
        GistService abilityService;

        private async void Awake()
        {
            abilityService = new GistService("e081087e30ca9af5f1bfbeff95511c3a", "abilities.json", "hexed-heroes-character-tracker-app");
            characterService = new GistService("12953c4c94be8897997bc3746fd82be9", "characters.json", "hexed-heroes-character-tracker-app");
            ActiveSession.IsOnline = await TryGetCharacters();

            if (!ActiveSession.IsOnline)
            {
                ActiveSession.AvailableCharacters = GetCharacterListConfigForJson(fallbackCharacterListConfig.text);
            }

            SceneManager.LoadScene(SceneKeys.CHARACTER_SELECT);
        }

        private async Task<bool> TryGetCharacters()
        {
            var abilities = await abilityService.GetGistContent<AbilityConfig[]>();
            var characters = await characterService.GetGistContent<List<CharacterConfig>>();

            if (abilities == null || characters == null)
            {
                return false;
            }

            foreach (var character in characters)
            {
                List<AbilityConfig> abilityConfigs = new();
                foreach (string id in character.abilityIds)
                {
                    foreach (AbilityConfig abilityConfig in abilities)
                    {
                        if (string.Equals(abilityConfig.id, id))
                        {
                            abilityConfigs.Add(abilityConfig);
                            break;
                        }
                    } 
                }
                character.abilities = abilityConfigs;
            }

            ActiveSession.AvailableCharacters = characters;
            return true;
        }
    
        private void GetRemoteConfigSettings()
        {
            progressFill = StartCoroutine(progressBar.Fill());
            ConfigManager.FetchCompleted += ParseResponse;
            ConfigManager.FetchConfigs(new UserAttributes(), appParams);
        }
    
        private void ParseResponse(ConfigResponse response)
        {
            ConfigManager.FetchCompleted -= ParseResponse;
            StartCoroutine(WaitAndLoad());
        }

        private IEnumerator WaitAndLoad()
        {
            yield return progressFill;
            LoadCharacterConfigs();
        }

        private void LoadCharacterConfigs()
        {
            ActiveSession.AvailableCharacters = GetCharacterListConfigForJson(fallbackCharacterListConfig.text);
            //ActiveSession.AvailableCharacters = GetConfigOrFallback();
            SceneManager.LoadScene(SceneKeys.CHARACTER_SELECT);
        }

        private static List<CharacterConfig> GetCharacterListConfigForJson(string json)
        {
            return JsonConvert.DeserializeObject<List<CharacterConfig>>(json);
        }

        private List<CharacterConfig> GetConfigOrFallback()
        {
            if (loadLocalConfig)
            {
                return GetCharacterListConfigForJson(fallbackCharacterListConfig.text);
            }

            return GetCharacterListConfigForJson(GetCharacterListJson()) ?? 
                   GetCharacterListConfigForJson(fallbackCharacterListConfig.text);
        }
    
        private string GetCharacterListJson()
        {
            return ConfigManager.appConfig.GetJson(RemoteConfigKeys.HERO_LIST_KEY);
        }
    }
}
