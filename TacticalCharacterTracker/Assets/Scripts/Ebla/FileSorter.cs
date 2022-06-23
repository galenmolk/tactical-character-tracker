using System.Collections.Generic;
using System.Linq;

namespace Ebla
{
    public static class FileSorter
    {
        public static IEnumerable<FileSlot> SortByName(IEnumerable<FileSlot> files)
        {
            var sortByName = files.ToList();
            if (sortByName.Count() < 2)
                return sortByName;

            return sortByName.OrderBy(fileSlot => fileSlot.File.Name);
        }
    }
}
