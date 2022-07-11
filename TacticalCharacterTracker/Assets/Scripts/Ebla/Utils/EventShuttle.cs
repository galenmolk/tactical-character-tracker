using System;

namespace Ebla.Utils
{
    public class EventShuttle
    {
        public event Action OnInvoke;

        public void Invoke()
        {
            OnInvoke?.Invoke();
        }

        public void Subscribe(Action action)
        {
            OnInvoke += action;
        }

        public void Unsubscribe(Action action)
        {
            OnInvoke -= action;
        }
    }
}
