using System.Collections.Generic;

namespace Ebla
{
    public static class FileSorter
    {
        private static readonly List<IFile> FileList = new();
        
        public static IEnumerable<IFile> SortByName(IFile[] files)
        {
            int fileCount = files.Length;
            
            if (fileCount < 2)
                return files;
            
            FileList.Clear();
            FileList.AddRange(files);
            FileList.Sort();
            
            return FileList.ToArray();
        }
    }
}
