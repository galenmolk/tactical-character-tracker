using Ebla.LibraryControllers;
using Ebla.Models;

namespace Ebla
{
    public class AbilityController : LibraryController<AbilityConfig, AbilityLibraryConfig>
    {
        public AbilityController(AbilityLibraryConfig libraryConfig) : base(libraryConfig)
        {
        }
    }
}
