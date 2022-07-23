using Ebla.LibraryControllers;
using Ebla.Models;
using Ebla.UI;
using UnityEngine;

namespace Ebla
{
    public class FileBrowser : MonoBehaviour
    {
        [SerializeField] private RectTransform fileArea;

        private void OnEnable()
        {
            Librarian.OnAbilityAdded += HandleAbilityAdded;
            Librarian.OnEnemyAdded += HandleEnemyAdded;
        }

        private void OnDisable()
        {
            Librarian.OnAbilityAdded -= HandleAbilityAdded;
            Librarian.OnEnemyAdded -= HandleEnemyAdded;
        }

        private void HandleAbilityAdded(AbilityConfig abilityConfig)
        {
            AbilitySlot slot = PrefabLibrary.Instance.GetAbilitySlot();
            InitializeSlot(slot, abilityConfig);
        }

        private void HandleEnemyAdded(EnemyConfig enemyConfig)
        {
            EnemySlot slot = PrefabLibrary.Instance.GetEnemySlot();
            InitializeSlot(slot, enemyConfig);
        }

        private void InitializeSlot<TSlot, TConfig>(TSlot slot, TConfig config)
            where TSlot : ConfigSlot<TSlot, TConfig>
            where TConfig : BaseConfig
        {
            slot.Configure(config);
            slot.transform.SetParent(fileArea);
        }
    }
}
