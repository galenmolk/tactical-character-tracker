using UnityEngine;

namespace Ebla.Libraries
{
    public class JediArchives : MonoBehaviour
    {
        private void Start()
        {
            FolderLibrarian.Instance.InitializeFolders();
            DungeonLibrarian.Instance.InitializeFolders();
            EncounterLibrarian.Instance.InitializeFolders();
            EnemyLibrarian.Instance.InitializeFolders();
            HeroLibrarian.Instance.InitializeFolders();
            AbilityLibrarian.Instance.InitializeFolders();
        }
    }
}
