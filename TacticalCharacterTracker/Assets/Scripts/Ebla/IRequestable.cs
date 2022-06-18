using System;

namespace Ebla
{
    public interface IRequestable<T>
    {
        public static event Action<Action<T>> OnRequested;

        public static void Request(Action<T> handleRequestReceived)
        {
            OnRequested?.Invoke(handleRequestReceived);
        }
        
        public void HandleRequested(Action<T> requestAction);
    }
}
