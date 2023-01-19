using System.Collections.Generic;
using System.IO;
using System.Text;
using DG.Tweening;
using Ebla.Models;
using Ebla.Utils;
using HexedHeroes.Utils;
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
        private static float OffPositionY => Screen.height / EncounterRunner.Instance.CanvasScaleFactor;
        
        private Vector2 offPosition = Vector2.zero;
        private Vector2 OffPos
        {
            get
            {
                offPosition.y = OffPositionY;
                return offPosition;
            }
        }
        
        [SerializeField] private float tweenDuration;
        [SerializeField] private CanvasGroup canvasGroup;
        
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
            RectTransform.anchoredPosition = OffPos;
            MakeCanvasGroupVisible();
            RectTransform.DOAnchorPosY(ON_POSITION_Y, tweenDuration).OnComplete(EnableCanvasGroup);
        }

        public void Close()
        {
            RectTransform.DOAnchorPosY(OffPositionY, tweenDuration).OnComplete(DisableCanvasGroup);
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

        private void EnableCanvasGroup()
        {
            canvasGroup.SetIsActive(true);
        }

        private void MakeCanvasGroupVisible()
        {
            canvasGroup.SetIsVisible(true);
        }

        private void DisableCanvasGroup()
        {
            canvasGroup.SetIsActive(false);
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
            DebugConsole.Instance.Log("Creating Encounter Item.");
            encounterConfig.FilePath = GetFullSavePath(encounterConfig);
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
            string json = File.ReadAllText(fileInfo.FullName);
            encounterConfig = JsonConvert.DeserializeObject<EncounterConfig>(json);
            bool wasSuccess = encounterConfig != null;
            return wasSuccess;
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
            return new StringBuilder().Append(SavePath).Append(config.Id).Append(JSON_EXTENSION).ToString();
        }
    }
}
