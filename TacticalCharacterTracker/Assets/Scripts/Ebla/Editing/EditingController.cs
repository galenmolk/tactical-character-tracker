using Ebla.Editing.Sections;
using Ebla.Models;
using MolkExtras;
using UnityEngine;

namespace Ebla.Editing
{
    public class EditingController : Singleton<EditingController>
    {
        [SerializeField] private Transform controlsParent;
        [SerializeField] private ControlsPrefabLibrary controlsPrefabLibrary;

        private GameObject activeControls;
        
        public void BeginAbilityEditing(AbilityConfig abilityConfig)
        {
            ClearActiveControls();
            AbilityControls controls = Instantiate(controlsPrefabLibrary.AbilityControls, controlsParent);
            activeControls = controls.gameObject;
            controls.Initialize(abilityConfig);
        }

        private void ClearActiveControls()
        {
            if (activeControls == null)
                return;
         
            Destroy(activeControls);
            activeControls = null;
        }
        
        private void OnEnable()
        {
            AddAbilityButton.OnAddConfigButtonClicked += BeginAbilityEditing;
        }

        private void OnDisable()
        {
            AddAbilityButton.OnAddConfigButtonClicked -= BeginAbilityEditing;
        }
    }
}
