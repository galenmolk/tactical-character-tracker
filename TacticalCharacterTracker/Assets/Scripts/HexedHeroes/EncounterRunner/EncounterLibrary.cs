using System;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Ebla.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EncounterLibrary : MonoBehaviour
    {
        public static event Action OnEncountersDeserialized;
        
        private const string CUSTOM_DATA_PATH = "/Configs/Encounters/";
        private const string JSON_EXTENSION = ".json";
        private const string JSON_SEARCH_PATTERN = "*.json";
        
        public IReadOnlyList<EncounterConfig> Encounters => encounters;

        private static string SavePath => Application.persistentDataPath + CUSTOM_DATA_PATH;

        [SerializeField] private float onPositionY;
        [SerializeField] private float offPositionY;
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
        
        private readonly List<EncounterConfig> encounters = new();
        private List<EncounterListItem> items = new();

        [SerializeField] private Transform itemParent;
        [SerializeField] private EncounterListItem itemPrefab;
        
        private void Start()
        {
            RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, onPositionY);
            TryCreateFolder();
            
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
                    
                EncounterListItem item = Instantiate(itemPrefab, itemParent);
                item.Configure(encounterConfig);
                
                item.OnRun += HandleEncounterRun;
                item.OnDelete += HandleEncounterDelete;
                
                items.Add(item);
            }
        }

        private void HandleEncounterRun(EncounterConfig encounterConfig)
        {
            EncounterRunner.Instance.SpinUpEncounter(encounterConfig);
            RectTransform.DOAnchorPosY(offPositionY, tweenDuration);
        }

        private void HandleEncounterDelete(EncounterConfig encounterConfig)
        {
            
        }
        
        private static void TryCreateFolder()
        {
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
        }

        private bool TryGetEncounterFromFile(FileSystemInfo fileInfo, out EncounterConfig encounterConfig)
        {
            using StreamReader sr = new StreamReader(fileInfo.FullName);
            string json = sr.ReadToEnd();
            encounterConfig = JsonConvert.DeserializeObject<EncounterConfig>(json);

            bool isValid = encounterConfig != null;
            
            if (isValid)
            {
                encounters.Add(encounterConfig);
            }

            return isValid;
        }
        
        private void OnEnable()
        {
            BaseConfig.OnConfigModifiedStatic += HandleConfigModified;
        }

        private void OnDisable()
        {
            BaseConfig.OnConfigModifiedStatic -= HandleConfigModified;
        }

        private static void HandleConfigModified()
        {
            EncounterConfig config = EncounterRunner.Instance.ActiveConfig;
            string encounterJson = JsonConvert.SerializeObject(config);
            File.WriteAllText(GetFullSavePath(config), encounterJson);
        }

        private static string GetFullSavePath(BaseConfig encounterConfig)
        {
            return SavePath + encounterConfig.Id + JSON_EXTENSION;
        }
    }
}
