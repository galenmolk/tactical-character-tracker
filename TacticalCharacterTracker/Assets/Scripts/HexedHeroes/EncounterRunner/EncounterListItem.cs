using System;
using Ebla.Models;
using TMPro;
using UnityEngine;

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
    }
}
