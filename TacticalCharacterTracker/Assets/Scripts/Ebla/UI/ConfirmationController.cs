using Ebla.Models;
using Ebla.Utils;
using HexedHeroes.EncounterRunner;
using MolkExtras;
using UnityEngine;
using UnityEngine.Events;

namespace Ebla.UI
{
    public class ConfirmationController : Singleton<ConfirmationController>
    {
        [SerializeField] private ConfirmationOverlay overlayPrefab;
        [SerializeField] private Transform overlayParent;

        [Space(5), Header("Confirmation Parameter Assets")]
        [SerializeField] private ConfirmationParams deleteConfigParams;
        [SerializeField] private ConfirmationParams encounterInfoParams;

        [SerializeField] private ConfirmationParams overwriteDefaultParams;
        [SerializeField] private ConfirmationParams loadDefaultParams;
        [SerializeField] private ConfirmationParams quitAppParams;
        
        private ConfirmationOverlay overlayInstance;

        public void QuitApp(UnityAction quitAction)
        {
            CreateOverlay(quitAppParams, quitAction);
        }
        
        public void DeleteConfig(BaseConfig baseConfig)
        {
            deleteConfigParams.SetTitle(baseConfig.GetDeletionText());
            CreateOverlay(deleteConfigParams, baseConfig.DeleteConfig);
        }

        public void DeleteFolder(FolderConfig folderConfig)
        {
            deleteConfigParams.SetTitle(folderConfig.GetDeletionText());
            CreateOverlay(deleteConfigParams, folderConfig.DeleteConfig);
        }

        //public void EncounterInfo(EncounterConfig encounterConfig)
        //{
        //    encounterInfoParams.SetTitle(encounterConfig.Name);
        //    CreateOverlay(encounterInfoParams);
        //}

        public void SaveDefault()
        {
            CreateOverlay(overwriteDefaultParams, EncounterRunner.Instance.SaveDefault);
        }

        public void LoadDefault()
        {
            CreateOverlay(loadDefaultParams, EncounterRunner.Instance.LoadDefault);
        }

        private void CreateOverlay(ConfirmationParams confirmationParams, UnityAction action)
        {
            if (Input.GetKey(HotKeys.ForceExecute))
            {
                action?.Invoke();
                return;
            }

            confirmationParams.LoadAction(action);
            ClearActiveInstance();
            overlayInstance = Instantiate(overlayPrefab, overlayParent);
            overlayInstance.OnClose += ClearActiveInstance;
            overlayInstance.Configure(confirmationParams);
        }
        
        private void ClearActiveInstance()
        {
            overlayInstance = null;
        }
    }
}
