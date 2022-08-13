using Ebla.Models;

namespace Ebla.Libraries
{
    public class FolderLibrarian : Librarian<FolderLibrarian, FolderConfig, FolderLibraryController>
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
