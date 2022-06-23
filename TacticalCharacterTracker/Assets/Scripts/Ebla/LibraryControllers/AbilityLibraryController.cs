using Ebla.LibraryControllers;
using Ebla.Models;

namespace Ebla
{
    public class AbilityLibraryController : LibraryController<AbilityConfig, AbilityLibraryConfig>
    {
        public AbilityLibraryController(AbilityLibraryConfig libraryConfig) : base(libraryConfig)
        {
        }
    }
}
