using System.Collections.Generic;
using Ebla.Models;
using Ebla.Utils;
using MolkExtras;
using TMPro;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EncounterRunner : Singleton<EncounterRunner>
    {
        public EncounterConfig ActiveConfig { get; private set; }

        [SerializeField] private TMP_InputField encounterTitleText;
        [SerializeField] private EnemyTypeBlock enemyTypeBlockPrefab;
        [SerializeField] private Transform enemyTypeBlockParent;

        private readonly List<EnemyTypeBlock> blocks = new();

        public void UpdateName(string newName)
        {
            ActiveConfig.UpdateName(newName);
        }
        
        public void AddEnemyType()
        {
            EnemyTypeConfig enemyType = new EnemyTypeConfig();
            enemyType.SetNameSilent(PathUtils.GetUniqueName(enemyType.BaseName, blocks, block => block.Config.Name));
            ActiveConfig.AddEnemyType(enemyType);
            CreateBlockForEnemyType(enemyType);
        }

        public void SpinUpEncounter(EncounterConfig encounter)
        {
            Clear();
            ActiveConfig = encounter;
            encounterTitleText.text = ActiveConfig.Name;
            CreateEnemyTypes();
        }

        public void ClearEncounter()
        {
            Clear();
            ActiveConfig = null;
            encounterTitleText.text = string.Empty;
        }

        private void Clear()
        {
            blocks.DestroyAndClear(block => block.gameObject);
        }
        
        private void CreateEnemyTypes()
        {
            foreach (var enemyTypeConfig in ActiveConfig.GetEnemyTypes())
            {
                CreateBlockForEnemyType(enemyTypeConfig);
            }
        }

        private void CreateBlockForEnemyType(EnemyTypeConfig enemyTypeConfig)
        {
            EnemyTypeBlock block = Instantiate(enemyTypeBlockPrefab, enemyTypeBlockParent);
            block.Initialize(enemyTypeConfig);
            block.OnDelete += HandleBlockDelete;
            blocks.Add(block);
        }

        private void HandleBlockDelete(EnemyTypeBlock block)
        {
            ActiveConfig.RemoveEnemyType(block.Config);
            blocks.Remove(block);
            Destroy(block.gameObject);
        }
    }
}
