using System.Collections.Generic;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Libraries
{
    public class EncounterLibraryController : LibraryController<EncounterConfig>
    {
        public EncounterLibraryController(List<EncounterConfig> libraryConfig) : base(libraryConfig)
        {
        }
    }
}
