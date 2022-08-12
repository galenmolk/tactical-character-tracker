using System;
using System.Collections.Generic;
using System.IO;
using Ebla.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EncounterLibrary : MonoBehaviour
    {
        public static event Action OnEncountersDeserialized;
        
        private const string CUSTOM_DATA_PATH = "/Configs/Encounters/";
        private const string JSON_SUFFIX = ".json";

        public IReadOnlyList<EncounterConfig> Encounters => encounters;

        private static string SavePath => Application.persistentDataPath + CUSTOM_DATA_PATH;

        private readonly List<EncounterConfig> encounters = new();
        
        private void Start()
        {
            TryCreateFolder();
            
            LoadEncounters();
            
            OnEncountersDeserialized?.Invoke();
            
            if (encounters.Count > 0)
            {
                EncounterRunner.Instance.SpinUpEncounter(encounters[0]);
            }
        }

        private void LoadEncounters()
        {
            DirectoryInfo saveFolder = new(SavePath);
            FileInfo[] files = saveFolder.GetFiles();
            
            foreach (FileInfo file in files)
            {
                GetEncounterFromFile(file);
            }
        }

        private static void TryCreateFolder()
        {
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
        }

        private void GetEncounterFromFile(FileInfo fileInfo)
        {
            string json = File.ReadAllText(fileInfo.FullName);
            EncounterConfig config = JsonConvert.DeserializeObject<EncounterConfig>(json);
            encounters.Add(config);
        }
        
        private void OnEnable()
        {
            BaseConfig.OnConfigModifiedStatic += HandleConfigModified;
        }

        private void OnDisable()
        {
            BaseConfig.OnConfigModifiedStatic -= HandleConfigModified;
        }

        private void HandleConfigModified()
        {
            EncounterConfig config = EncounterRunner.Instance.ActiveConfig;
            string encounterJson = JsonConvert.SerializeObject(config);
            File.WriteAllText(GetFullSavePath(config), encounterJson);
        }

        private string GetFullSavePath(BaseConfig encounterConfig)
        {
            return SavePath + encounterConfig.Id + JSON_SUFFIX;
        }
    }
}
