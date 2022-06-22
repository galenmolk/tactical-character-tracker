using MolkExtras;
using UnityEngine;
using UnityEngine.Pool;

namespace Ebla
{
    public class PrefabLibrary : Singleton<PrefabLibrary>
    {
        private IObjectPool<FileSlot> filePool;
        private IObjectPool<FolderSlot> folderPool;

        [SerializeField] private FileSlot fileSlotPrefab;
        [SerializeField] private FolderSlot folderPrefab;

        public FileSlot GetFileSlot()
        {
            return filePool.Get();
        }

        public FolderSlot GetFolder()
        {
            return folderPool.Get();
        }
        
        private void Awake()
        {
            filePool = new ObjectPool<FileSlot>(CreateFile, OnTakeFromFilePool, OnFileReturnedToPool, OnDestroyFile, false, 5, 50);
            folderPool = new ObjectPool<FolderSlot>(CreateFolder, OnTakeFromFolderPool, OnFolderReturnedToPool,
                OnDestroyFolder, false, 5, 50);
        }

        private FileSlot CreateFile()
        {
            FileSlot fileSlot = Instantiate(fileSlotPrefab, transform);
            fileSlot.gameObject.SetActive(false);
            fileSlot.OnFileDisabled += HandleFileDisabled;
            return fileSlot;
        }

        private FolderSlot CreateFolder()
        {
            FolderSlot folder = Instantiate(folderPrefab, transform);
            folder.gameObject.SetActive(false);
            folder.OnFolderDisabled += HandleFolderDisabled;
            return folder;
        }

        private void OnTakeFromFilePool(FileSlot fileSlot)
        {
            fileSlot.gameObject.SetActive(true);
        }

        private void OnTakeFromFolderPool(FolderSlot folder)
        {
            folder.gameObject.SetActive(true);
        }

        private void OnFileReturnedToPool(FileSlot fileSlot)
        {
            fileSlot.ResetFile();
        }

        private void OnFolderReturnedToPool(FolderSlot folder)
        {
            folder.ResetFolder();
        }

        private void OnDestroyFile(FileSlot fileSlot)
        {
            Destroy(fileSlot.gameObject);
        }

        private void OnDestroyFolder(FolderSlot folder)
        {
            Destroy(folder.gameObject);
        }

        private void HandleFileDisabled(FileSlot fileSlot)
        {
            fileSlot.transform.SetParent(transform);
            filePool.Release(fileSlot);
        }

        private void HandleFolderDisabled(FolderSlot folder)
        {
            folder.transform.SetParent(transform);
            folderPool.Release(folder);
        }
    }
}
