using System.Collections.Generic;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Libraries
{
    public class AbilityLibraryController : LibraryController<AbilityConfig>
    {
        public AbilityLibraryController(List<AbilityConfig> libraryConfig) : base(libraryConfig)
        {
        }
    }
}
