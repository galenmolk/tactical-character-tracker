using Ebla.Models;
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
            quitAppParams.LoadAction(quitAction);
            CreateOverlay(quitAppParams);
        }
        
        public void DeleteConfig(BaseConfig baseConfig)
        {
            deleteConfigParams.SetTitle(baseConfig.GetDeletionText());
            deleteConfigParams.LoadAction(baseConfig.DeleteConfig);
            CreateOverlay(deleteConfigParams);
        }

        public void DeleteFolder(FolderConfig folderConfig)
        {
            deleteConfigParams.SetTitle(folderConfig.GetDeletionText());
            deleteConfigParams.LoadAction(folderConfig.DeleteConfig);
            CreateOverlay(deleteConfigParams);
        }

        public void EncounterInfo(EncounterConfig encounterConfig)
        {
            encounterInfoParams.SetTitle(encounterConfig.Name);
            CreateOverlay(encounterInfoParams);
        }

        public void SaveDefault()
        {
            overwriteDefaultParams.LoadAction(EncounterRunner.Instance.SaveDefault);
            CreateOverlay(overwriteDefaultParams);
        }

        public void LoadDefault()
        {
            loadDefaultParams.LoadAction(EncounterRunner.Instance.LoadDefault);
            CreateOverlay(loadDefaultParams);
        }

        private void CreateOverlay(ConfirmationParams confirmationParams)
        {
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
