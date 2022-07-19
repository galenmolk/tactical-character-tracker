using System.Collections.Generic;
using Ebla.Models;

namespace Ebla.LibraryControllers
{
    public class EnemyLibraryController : LibraryController<EnemyConfig>
    {
        public EnemyLibraryController(List<EnemyConfig> libraryConfig) : base(libraryConfig)
        {
        }
    }
}
