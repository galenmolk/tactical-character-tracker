using Ebla.Models;
using UnityEngine;

namespace Ebla.Libraries
{
    public class AbilityLibrarian : Librarian<AbilityLibrarian, AbilityConfig, AbilityLibraryController>
    {
        protected override void Start()
        {
            base.Start();
            Debug.Log("ability librarian start");
        }
    }
}
