using System;

namespace Ebla
{
    public interface IFile : IComparable
    {
        public string Name { get; }
        public string LowerCaseName { get; }
        public DateTime DateCreated { get; }
        public DateTime DateModified { get; }
    }
}
