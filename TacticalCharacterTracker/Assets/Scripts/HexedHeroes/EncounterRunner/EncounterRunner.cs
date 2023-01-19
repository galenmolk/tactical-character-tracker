using System;
using System.Collections.Generic;
using DG.Tweening;
using Ebla.Models;
using Ebla.UI;
using Ebla.Utils;
using MolkExtras;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace HexedHeroes.EncounterRunner
{
    public class EncounterRunner : Singleton<EncounterRunner>
    {
        public static event Action OnDefaultLoaded;
        public static event Action OnEncounterLoaded; 

        public float CanvasScaleFactor => canvas.scaleFactor;
        
        public EncounterConfig ActiveConfig { get; private set; }

        [SerializeField] private TMP_InputField encounterTitleText;
        [SerializeField] private EnemyBlock enemyBlockPrefab;
        [SerializeField] private Transform enemyTypeBlockParent;

        [SerializeField] private Canvas canvas;

        [SerializeField] private LayoutElement spacerPrefab;
        [SerializeField, Min(0f)] private float shrinkDuration;

        private readonly List<EnemyBlock> blocks = new();

        private bool wasDefaultJustLoaded;

        public void TryQuitApp()
        {
            ConfirmationController.Instance.QuitApp(Quit);
        }

        private static void Quit()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.ExitPlaymode();
            }
#else
            Application.Quit();
#endif
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

        public void TrySaveDefault()
        {
            if (ActiveConfig.HasDefault)
            {
                ConfirmationController.Instance.SaveDefault();
            }
            else
            {
                SaveDefault();
            }
        }

        public void TryLoadDefault()
        {
            if (ActiveConfig.HasDefault)
            {
                ConfirmationController.Instance.LoadDefault();
            }
        }

        public void LoadDefault()
        {
            wasDefaultJustLoaded = true;
            Clear();
            ActiveConfig.LoadDefault();
            CreateEnemyTypes();
            OnDefaultLoaded?.Invoke();
        }

        public void SaveDefault()
        {
            wasDefaultJustLoaded = true;
            ActiveConfig.SaveDefault();
            OnDefaultLoaded?.Invoke();
        }

        private void Clear()
        {
            blocks.DestroyAndClear(block => block.gameObject);
        }
        
        private void CreateEnemyTypes()
        {
            OnEncounterLoaded?.Invoke();

            foreach (EnemyConfig enemyTypeConfig in ActiveConfig.GetEnemies())
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
            int blockCount = blocks.Count;
            float height = block.RectTransform.sizeDelta.y;
            int siblingIndex = block.transform.GetSiblingIndex();

            ActiveConfig.RemoveEnemy(block.Config);
            blocks.Remove(block);

            if (block.Fade != null)
            {
                block.Fade.FadeOut(DestroyBlock);
            }

            void DestroyBlock()
            {
                Destroy(block.gameObject);

                if (blockCount > 1)
                {
                    SlideOver(siblingIndex, height);
                }
            }
        }

        private void SlideOver(int siblingIndex, float height)
        {
            LayoutElement spacer = Instantiate(spacerPrefab, enemyTypeBlockParent);
            spacer.transform.SetSiblingIndex(siblingIndex);
            spacer.minHeight = height;

            Vector2 minSize = new Vector2(spacer.minWidth, 0f);
            spacer.DOMinSize(minSize, shrinkDuration).OnComplete(DestroySpacer);

            void DestroySpacer()
            {
                Destroy(spacer.gameObject);
            }
        }

        private void Awake()
        {
            BaseConfig.OnAnyConfigModified += HandleAnyConfigModified;
        }

        private void OnDestroy()
        {
            BaseConfig.OnAnyConfigModified -= HandleAnyConfigModified;
        }

        private void HandleAnyConfigModified()
        {
            if (wasDefaultJustLoaded)
            {
                wasDefaultJustLoaded = false;
                ActiveConfig.SetIsDefaultLoaded(true);
            }
            else
            {
                ActiveConfig.SetIsDefaultLoaded(false);
            }
        }
    }
}
