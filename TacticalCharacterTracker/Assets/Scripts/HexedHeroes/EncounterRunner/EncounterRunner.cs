using Ebla.Models;
using MolkExtras;
using TMPro;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EncounterRunner : Singleton<EncounterRunner>
    {
        public EncounterConfig ActiveConfig => encounterConfig;
        
        [SerializeField] private TextAsset encounterJson;
        [SerializeField] private TMP_InputField encounterTitleText;
        [SerializeField] private EnemyTypeBlock enemyTypeBlockPrefab;
        [SerializeField] private Transform enemyTypeBlockParent;

        private EncounterConfig encounterConfig = new();

        public void UpdateName(string newName)
        {
            encounterConfig.UpdateName(newName);
        }
        
        public void AddEnemyType()
        {
            var enemyType = new EnemyTypeConfig();
            encounterConfig.AddEnemyType(enemyType);
            CreateBlockForEnemyType(enemyType);
        }

        public void SpinUpEncounter(EncounterConfig encounter)
        {
            encounterConfig = encounter;
            encounterTitleText.text = encounterConfig.Name;
            CreateEnemyTypes();
        }
        
        private void CreateEnemyTypes()
        {
            foreach (var enemyTypeConfig in encounterConfig.GetEnemyTypes())
            {
                CreateBlockForEnemyType(enemyTypeConfig);
            }
        }

        private void CreateBlockForEnemyType(EnemyTypeConfig enemyTypeConfig)
        {
            EnemyTypeBlock block = Instantiate(enemyTypeBlockPrefab, enemyTypeBlockParent);
            block.Initialize(enemyTypeConfig);
        }
    }
}
