using Ebla.Models;
using UnityEngine;

namespace Ebla.Libraries
{
    public class HeroLibrarian : Librarian<HeroLibrarian, HeroConfig, HeroLibraryController>
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
