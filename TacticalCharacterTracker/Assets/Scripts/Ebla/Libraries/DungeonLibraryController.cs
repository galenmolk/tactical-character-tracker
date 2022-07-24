using System.Collections.Generic;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Libraries
{
    public class DungeonLibraryController : LibraryController<DungeonConfig>
    {
        public DungeonLibraryController(List<DungeonConfig> libraryConfig) : base(libraryConfig)
        {
        }
    }
}
