using System.Collections.Generic;
using Ebla.Libraries;
using Ebla.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.UI
{
    public class BackForwardButtons : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button forwardButton;

        [SerializeField] private List<FolderConfig> folderHistory = new();
        private int folderIndex = -1;
        private FolderConfig currentFolder;
        
        private void GoForward()
        {
            folderIndex++;
            OpenFolder();
            UpdateButtonStates();
        }

        private void GoBack()
        {
            folderIndex--;
            OpenFolder();
            UpdateButtonStates();
        }

        private void OpenFolder()
        {
            FolderConfig folder = folderHistory[folderIndex];
            currentFolder = folder;
            ScopeController.Instance.LoadScope(folder);
        }

        private void UpdateButtonStates()
        {
            backButton.interactable = folderIndex > 0;
            forwardButton.interactable = folderIndex < folderHistory.Count - 1;
        }
        
        private void HandleScopeChanged()
        {
            Debug.Log($"HandleScopeChanged : {currentFolder?.Name}");
            Debug.Log($"HandleScopeChanged scope : {ScopeController.Instance.CurrentFolder?.Name}");
            // Only act on this event if it wasn't triggered by this script.
            if (ScopeController.Instance.CurrentFolder == currentFolder)
            {
                return;
            }

            folderIndex++;

            currentFolder = ScopeController.Instance.CurrentFolder;

            TryTrimFolderHistory();

            folderHistory.Add(currentFolder);
            UpdateButtonStates();
        }

        private void HandleFolderRemoved(FolderConfig folderConfig)
        {
            if (!folderHistory.Remove(folderConfig))
            {
                return;
            }

            TryTrimFolderHistory();
            UpdateButtonStates();
        }

        private void TryTrimFolderHistory()
        {
            if (folderHistory.Count > folderIndex)
            {
                folderHistory.RemoveRange(folderIndex, folderHistory.Count - folderIndex);
            }
        }
        
        private void Start()
        {
            FolderLibrarian.OnConfigRemoved += HandleFolderRemoved;
            ScopeController.OnScopeChanged += HandleScopeChanged;
            forwardButton.onClick.AddListener(GoForward);
            backButton.onClick.AddListener(GoBack);
            UpdateButtonStates();
        }

        private void OnDestroy()
        {
            ScopeController.OnScopeChanged -= HandleScopeChanged;
        }
    }
}
