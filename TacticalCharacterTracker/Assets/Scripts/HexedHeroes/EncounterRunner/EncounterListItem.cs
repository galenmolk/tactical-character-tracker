using System;
using System.Diagnostics;
using Ebla.Models;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace HexedHeroes.EncounterRunner
{
    public class EncounterListItem : MonoBehaviour
    {
        public event Action<EncounterConfig> OnRun;
        public event Action<EncounterConfig> OnDelete;

        private EncounterConfig encounterConfig;
        
        [SerializeField] private TMP_Text nameText;
        
        public void Configure(EncounterConfig encounter)
        {
            encounterConfig = encounter;
            encounter.OnConfigModified += Display;
            Display(encounterConfig);
        }

        private void Display(BaseConfig baseConfig)
        {
            nameText.text = encounterConfig.Name;
        }
        
        public void Delete()
        {
            OnDelete?.Invoke(encounterConfig);
        }

        public void Run()
        {
            OnRun?.Invoke(encounterConfig);
        }

        public void OpenInExplorer()
        {
            Debug.Log(encounterConfig.FilePath);
            #if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            Process process = new Process();
            process.StartInfo.FileName = "open";
            process.StartInfo.Arguments = "-n -R \"" + encounterConfig.FilePath + "\"";
            
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.ErrorDataReceived += OnFinderError;

            if (!process.Start())
            {
                Debug.LogError("Finder process start encountered an error."); 
                return;
            }
            
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            #endif

            #if UNITY_STANDALONE_WIN && !UNITY_EDITOR_OSX
            string path = encounterConfig.FilePath.Replace(@"/", @"\");
            Process.Start("explorer.exe", "/select," + path);
            #endif
        }

        private static void OnFinderError(object sender, DataReceivedEventArgs args)
        {
            string data = args.Data;
            
            if (!string.IsNullOrWhiteSpace(data))
            {
                Debug.LogError($"Finder Error: {data}");
            }
        }

        private void OnDisable()
        {
            if (encounterConfig != null)
            {
                encounterConfig.OnConfigModified -= Display;
            }
            
            OnDelete = null;
            OnRun = null;
        }
    }
}
