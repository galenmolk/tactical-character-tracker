using Ebla.Models;
using MolkExtras;
using UnityEngine;

namespace Ebla.UI
{
    public class ConfirmationController : Singleton<ConfirmationController>
    {
        [SerializeField] private ConfirmationOverlay overlayPrefab;
        [SerializeField] private Transform overlayParent;

        [Space(5), Header("Confirmation Parameter Assets")]
        [SerializeField] private ConfirmationParams deleteConfigParams;
        
        private ConfirmationOverlay overlayInstance;

        public void DeleteConfig(BaseConfig baseConfig)
        {
            deleteConfigParams.SetTitle($"Delete {baseConfig.Name}?");
            deleteConfigParams.LoadAction(baseConfig.DeleteConfig);
            CreateOverlay(deleteConfigParams);
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
            if (overlayInstance == null)
            {
                return;
            }

            Destroy(overlayInstance.gameObject);
            overlayInstance = null;
        }
    }
}
