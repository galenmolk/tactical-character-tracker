using System.Collections.Generic;
using Ebla.LibraryControllers;
using Ebla.Models;
using UnityEngine;

namespace Ebla
{
    public class AbilityLibraryController : LibraryController<AbilityConfig>
    {
        public AbilityLibraryController(List<AbilityConfig> libraryConfig) : base(libraryConfig)
        {
            foreach (var confi in libraryConfig)
            {
                Debug.Log("config name: " + confi.Name);
            }
        }
    }
}
