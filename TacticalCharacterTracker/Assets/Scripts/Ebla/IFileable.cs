using System;

namespace Ebla
{
    public interface IFileable
    {
        public string Name { get; }
        public string ParentFolderName { get; }
        public DateTime DateCreated { get; }
        public DateTime DateModified { get; }

        public void SetFolder(FolderSlot folder);
    }
}
