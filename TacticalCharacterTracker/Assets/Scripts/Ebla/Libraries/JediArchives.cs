using UnityEngine;

namespace Ebla.Libraries
{
    public class JediArchives : MonoBehaviour
    {
        private void Start()
        {
            FolderLibrarian.Instance.InitializeFolders();
            AbilityLibrarian.Instance.InitializeFolders();
        }
    }
}
