using System.Collections.Generic;
using System.Linq;

namespace Ebla
{
    public static class FileSorter
    {
        public static IEnumerable<IFileable> SortByName(IFileable[] files)
        {
            if (files.Length < 2)
                return files;

            return files.OrderBy(file => file.Name);
        }
    }
}
