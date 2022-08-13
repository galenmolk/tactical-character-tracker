using Ebla.Models;

namespace Ebla.Libraries
{
    public class EnemyLibrarian : Librarian<EnemyLibrarian, EnemyConfig, EnemyLibraryController>
    {
        protected override void SubscribeToDeleteEvents()
        {
            throw new System.NotImplementedException();
        }

        protected override void UnsubscribeToDeleteEvents()
        {
            throw new System.NotImplementedException();
        }
    }
}
