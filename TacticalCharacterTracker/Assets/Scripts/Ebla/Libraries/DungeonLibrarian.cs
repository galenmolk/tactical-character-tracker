using Ebla.Models;

namespace Ebla.Libraries
{
    public class DungeonLibrarian : Librarian<DungeonLibrarian, DungeonConfig, DungeonLibraryController>
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
