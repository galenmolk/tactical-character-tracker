using UnityEngine;

namespace Ebla.UI
{
    public abstract class Window : MonoBehaviour
    {
        public virtual void Open()
        {
            BackController.Instance.RegisterWindow(this);
        }

        public virtual void Close()
        {
            BackController.Instance.UnregisterWindow(this);
            ClearWindow();
        }
        
        public abstract void ClearWindow();
    }
}
