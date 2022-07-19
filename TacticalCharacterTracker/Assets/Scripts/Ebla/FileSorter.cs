using System.Collections.Generic;
using System.Linq;
using Ebla.UI;

namespace Ebla
{
    public static class FileSorter
    {
        public static IEnumerable<ConfigSlot> SortByName(IEnumerable<ConfigSlot> files)
        {
            var sortByName = files.ToList();
            if (sortByName.Count() < 2)
                return sortByName;

            return sortByName.OrderBy(fileSlot => fileSlot.Config.Name);
        }
    }
}
