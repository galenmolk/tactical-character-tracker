using MolkExtras;
using UnityEngine;
using UnityEngine.Pool;

namespace Ebla
{
    public class PrefabLibrary : Singleton<PrefabLibrary>
    {
        private IObjectPool<ConfigSlot> filePool;
        private IObjectPool<FolderSlot> folderPool;

        [SerializeField] private ConfigSlot fileSlotPrefab;
        [SerializeField] private FolderSlot folderPrefab;

        public ConfigSlot GetConfigSlot()
        {
            return filePool.Get();
        }

        public FolderSlot GetFolder()
        {
            return folderPool.Get();
        }
        
        private void Awake()
        {
            filePool = new ObjectPool<ConfigSlot>(CreateFile, OnTakeFromFilePool, OnFileReturnedToPool, OnDestroyFile, false, 5, 50);
            folderPool = new ObjectPool<FolderSlot>(CreateFolder, OnTakeFromFolderPool, OnFolderReturnedToPool,
                OnDestroyFolder, false, 5, 50);
        }

        private ConfigSlot CreateFile()
        {
            ConfigSlot fileSlot = Instantiate(fileSlotPrefab, transform);
            fileSlot.gameObject.SetActive(false);
            fileSlot.OnReleaseConfigSlot += HandleReleaseConfigSlot;
            return fileSlot;
        }

        private FolderSlot CreateFolder()
        {
            FolderSlot folder = Instantiate(folderPrefab, transform);
            folder.gameObject.SetActive(false);
            folder.OnFolderDisabled += HandleFolderDisabled;
            return folder;
        }

        private void OnTakeFromFilePool(ConfigSlot fileSlot)
        {
            fileSlot.gameObject.SetActive(true);
        }

        private void OnTakeFromFolderPool(FolderSlot folder)
        {
            folder.gameObject.SetActive(true);
        }

        private void OnFileReturnedToPool(ConfigSlot fileSlot)
        {
            fileSlot.ApplyConfigToSlot();
        }

        private void OnFolderReturnedToPool(FolderSlot folder)
        {
            folder.ResetFolder();
        }

        private void OnDestroyFile(ConfigSlot fileSlot)
        {
            Destroy(fileSlot.gameObject);
        }

        private void OnDestroyFolder(FolderSlot folder)
        {
            Destroy(folder.gameObject);
        }

        private void HandleReleaseConfigSlot(ConfigSlot fileSlot)
        {
            fileSlot.transform.SetParent(transform);
            filePool.Release(fileSlot);
            fileSlot.gameObject.SetActive(false);
        }

        private void HandleFolderDisabled(FolderSlot folder)
        {
            folder.transform.SetParent(transform);
            folderPool.Release(folder);
        }
    }
}
