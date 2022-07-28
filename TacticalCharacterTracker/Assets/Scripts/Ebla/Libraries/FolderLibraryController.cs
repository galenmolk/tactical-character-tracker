using System.Collections.Generic;
using Ebla.Models;

namespace Ebla.Libraries
{
    public class FolderLibraryController : LibraryController<FolderConfig>
    {
        public FolderLibraryController(List<FolderConfig> libraryConfig) : base(libraryConfig)
        {
        }
    }
}
