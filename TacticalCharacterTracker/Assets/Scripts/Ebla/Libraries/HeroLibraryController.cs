using System.Collections.Generic;
using Ebla.Models;
using UnityEngine;

namespace Ebla.Libraries
{
    public class HeroLibraryController : LibraryController<HeroConfig>
    {
        public HeroLibraryController(List<HeroConfig> libraryConfig) : base(libraryConfig)
        {
        }
    }
}
