using Ebla.Models;
using UnityEngine;

namespace Ebla.Libraries
{
    public class EncounterLibrarian : Librarian<EncounterLibrarian, EncounterConfig, EncounterLibraryController>
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
