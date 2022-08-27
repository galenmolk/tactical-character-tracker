using System.Collections.Generic;
using Ebla.Models;
using Ebla.UI;
using Ebla.Utils;
using MolkExtras;
using TMPro;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class EncounterRunner : Singleton<EncounterRunner>
    {
        public float CanvasScaleFactor => canvas.scaleFactor;
        
        public EncounterConfig ActiveConfig { get; private set; }

        [SerializeField] private TMP_InputField encounterTitleText;
        [SerializeField] private EnemyBlock enemyBlockPrefab;
        [SerializeField] private Transform enemyTypeBlockParent;

        [SerializeField] private Canvas canvas;
        
        private readonly List<EnemyBlock> blocks = new();

        public void TryQuitApp()
        {
            ConfirmationController.Instance.QuitApp(Quit);
        }

        private void Quit()
        {
            Application.Quit();
        }
        
        public void UpdateName(string newName)
        {
            ActiveConfig.UpdateName(newName);
        }
        
        public void AddEnemy()
        {
            EnemyConfig enemy = new EnemyConfig();
            enemy.SetNameSilent(PathUtils.GetUniqueName(enemy.BaseName, blocks, block => block.Config.Name));
            ActiveConfig.AddEnemy(enemy);
            CreateBlockForEnemyType(enemy);
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
            foreach (var enemyTypeConfig in ActiveConfig.GetEnemies())
            {
                CreateBlockForEnemyType(enemyTypeConfig);
            }
        }

        private void CreateBlockForEnemyType(EnemyConfig enemyConfig)
        {
            EnemyBlock block = Instantiate(enemyBlockPrefab, enemyTypeBlockParent);
            block.Initialize(enemyConfig);
            block.OnDelete += HandleBlockDelete;
            blocks.Add(block);
        }

        private void HandleBlockDelete(EnemyBlock block)
        {
            ActiveConfig.RemoveEnemy(block.Config);
            blocks.Remove(block);
            Destroy(block.gameObject);
        }
    }
}
