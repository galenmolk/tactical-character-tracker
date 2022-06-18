using System;
using UnityEngine;

namespace Ebla
{
    public class PrefabLibrary : MonoBehaviour, IRequestable<File>
    {
        [SerializeField] private File filePrefab;

        private void OnEnable()
        {
            IRequestable<File>.OnRequested += HandleRequested;
        }

        private void OnDisable()
        {
            IRequestable<File>.OnRequested -= HandleRequested;
        }

        public void HandleRequested(Action<File> requestAction)
        {
            requestAction?.Invoke(filePrefab);
        }
    }
}
