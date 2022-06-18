using System;

namespace Ebla
{
    public interface IRequester<T>
    {
        Action<T> OnRequestReceived { get; }
    }
}
