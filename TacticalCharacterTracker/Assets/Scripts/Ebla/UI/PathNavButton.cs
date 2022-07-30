using System;
using Ebla.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.UI
{
    public class PathNavButton : BaseBehaviour<PathNavButton>
    {
        public override event Action<PathNavButton> OnReleaseObject;
        
        [SerializeField] private TMP_Text folderNameText;
        [SerializeField] private Button button;
        
        private FolderConfig folder;
        
        public void Configure(FolderConfig folderConfig)
        {
            ScopeController.OnScopeChanged += ReleaseObject;

            button.interactable = ScopeController.Instance.CurrentFolder != folderConfig;
            
            folder = folderConfig;
            folderNameText.text = folder.Name;
        }

        public override void ResetObject()
        {
            button.interactable = false;
            ScopeController.OnScopeChanged -= ReleaseObject;
            folderNameText.text = string.Empty;
            folder = null;
        }
        
        protected override void InvokeReleaseObject()
        {
            OnReleaseObject?.Invoke(this);
        }
        
        private void GotToFolder()
        {
            ScopeController.Instance.LoadScope(folder);
        }

        private void Awake()
        {
            button.onClick.AddListener(GotToFolder);
        }
    }
}
