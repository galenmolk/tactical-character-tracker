using System;
using Ebla.LibraryControllers;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Selection
{
    public class AbilitySelector : ConfigSelector<AbilityConfig, CharacterConfig, AbilityOption>
    {
        [SerializeField] private AbilityFilterControl abilityFilterControl;
        
        protected override void CreateOptions()
        {
            var abilities = Librarian.Instance.GetAbilities();

            Debug.Log("CreateOptions: " + abilities.Count);
            foreach (var abilityConfig in abilities)
            {
                if (!abilityFilterControl.IsValid(abilityConfig))
                    continue;
                    
                AbilityOption option = PrefabLibrary.Instance.GetAbilityOption();
                option.Transform.SetParent(optionParent);
                option.Initialize(abilityConfig, onFiltersUpdated);
                //options.Add(option);
            }
        }

        private void HandleFiltersModified()
        {
            Debug.Log("HandleFiltersModified");
            onFiltersUpdated.Invoke();
            CreateOptions();
        }
        
        private void OnEnable()
        {
            abilityFilterControl.OnFilterModified += HandleFiltersModified;
        }

        private void OnDisable()
        {
            abilityFilterControl.OnFilterModified -= HandleFiltersModified;
        }
    }
}
