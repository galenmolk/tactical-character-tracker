using Ebla.LibraryControllers;
using Ebla.Models;

namespace Ebla.Editing
{
    public class EnemyControls : EditingControls<EnemyConfig>
    {
        protected override void RemoveConfig()
        {
            EnemyLibrarian.Instance.Remove(ActiveConfig);
        }
    }
}
