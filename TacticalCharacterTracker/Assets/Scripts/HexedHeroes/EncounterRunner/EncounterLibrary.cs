using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Ebla.Models;
using Ebla.Utils;
using MolkExtras;
using Newtonsoft.Json;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EncounterLibrary : MonoBehaviour
    {
        private const string CUSTOM_DATA_PATH = "/Configs/Encounters/";
        private const string JSON_EXTENSION = ".json";
        private const string JSON_SEARCH_PATTERN = "*.json";
        
        private static string SavePath => Application.persistentDataPath + CUSTOM_DATA_PATH;

        private const float ON_POSITION_Y = 0f;
        private float OffPositionY => Screen.height;
        
        [SerializeField] private float tweenDuration;
        
        private RectTransform RectTransform
        {
            get
            {
                if (rectTransform == null)
                {
                    rectTransform = transform as RectTransform;
                }

                return rectTransform;
            }
        }

        private RectTransform rectTransform;
        
        private readonly Dictionary<EncounterConfig, EncounterListItem> items = new();

        [SerializeField] private Transform itemParent;
        [SerializeField] private EncounterListItem itemPrefab;

        public void Open()
        {
            RectTransform.DOAnchorPosY(ON_POSITION_Y, tweenDuration);
        }

        public void Close()
        {
            RectTransform.DOAnchorPosY(OffPositionY, tweenDuration);
        }

        public void NewEncounter()
        {
            EncounterConfig encounterConfig = new();
            encounterConfig.SetNameSilent(PathUtils.GetUniqueName(encounterConfig.BaseName, items, pair => pair.Key.Name));
            EncounterRunner.Instance.SpinUpEncounter(encounterConfig);
            Close();
            
            this.ExecuteAfterDelay(tweenDuration, () =>
            {
                CreateEncounterListItem(encounterConfig);
                SaveConfigToDisk(encounterConfig);
            });
        }
        
        private void Start()
        {
            RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, ON_POSITION_Y);
            
            TryCreateSaveFolder();
            LoadEncounterItemsIntoList();
        }

        private void LoadEncounterItemsIntoList()
        {
            DirectoryInfo saveFolder = new(SavePath);
            FileSystemInfo[] files = saveFolder.GetFileSystemInfos(JSON_SEARCH_PATTERN);

            foreach (FileSystemInfo file in files)
            {
                if (!TryGetEncounterFromFile(file, out EncounterConfig encounterConfig))
                {
                    continue;
                }
                
                CreateEncounterListItem(encounterConfig);
            }
        }

        private void CreateEncounterListItem(EncounterConfig encounterConfig)
        {
            EncounterListItem item = Instantiate(itemPrefab, itemParent);
            item.Configure(encounterConfig);
                
            item.OnRun += HandleEncounterRun;
            item.OnDelete += HandleEncounterDeleteButtonClicked;
                
            items.Add(encounterConfig, item);
        }
        
        private void HandleDeleteEncounter(EncounterConfig encounter)
        {
            if (!items.TryGetValue(encounter, out EncounterListItem item))
            {
                return;
            }

            if (EncounterRunner.Instance.ActiveConfig == encounter)
            {
                EncounterRunner.Instance.ClearEncounter();
            }
            
            items.Remove(encounter);
            Destroy(item.gameObject);
            File.Delete(GetFullSavePath(encounter));
        }
        
        private void HandleEncounterRun(EncounterConfig encounterConfig)
        {
            EncounterRunner.Instance.SpinUpEncounter(encounterConfig);
            Close();
        }

        private static void HandleEncounterDeleteButtonClicked(EncounterConfig encounterConfig)
        {
            encounterConfig.TryDeleteConfig();
        }
        
        private static void TryCreateSaveFolder()
        {
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
        }

        private static bool TryGetEncounterFromFile(FileSystemInfo fileInfo, out EncounterConfig encounterConfig)
        {
            using StreamReader sr = new StreamReader(fileInfo.FullName);
            string json = sr.ReadToEnd();
            encounterConfig = JsonConvert.DeserializeObject<EncounterConfig>(json);
            return encounterConfig != null;
        }
        
        private void OnEnable()
        {
            BaseConfig.OnAnyConfigModified += HandleAnyConfigModified;
            EncounterConfig.OnEncounterDeleted += HandleDeleteEncounter;
        }

        private void OnDisable()
        {
            BaseConfig.OnAnyConfigModified -= HandleAnyConfigModified;
            EncounterConfig.OnEncounterDeleted -= HandleDeleteEncounter;
        }

        private static void HandleAnyConfigModified()
        {
            EncounterRunner runner = EncounterRunner.Instance;

            if (runner == null)
            {
                return;
            }
            
            EncounterConfig config = EncounterRunner.Instance.ActiveConfig;

            if (config == null)
            {
                return;
            }
            
            SaveConfigToDisk(config);
        }

        private static void SaveConfigToDisk(BaseConfig config)
        {
            string encounterJson = JsonConvert.SerializeObject(config);
            File.WriteAllText(GetFullSavePath(config), encounterJson);
        }

        private static string GetFullSavePath(BaseConfig config)
        {
            return SavePath + config.Id + JSON_EXTENSION;
        }
    }
}
