using Ebla.API;
using Ebla.Models;

namespace Ebla.Libraries
{
    public class AbilityLibrarian : Librarian<AbilityLibrarian, AbilityConfig, AbilityLibraryController>
    {
        protected override string ApiRoute => ApiUtils.ABILITY_ROUTE;
        
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
