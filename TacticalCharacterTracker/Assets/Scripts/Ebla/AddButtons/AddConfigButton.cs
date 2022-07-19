using UnityEngine;
using UnityEngine.UI;

namespace Ebla.AddButtons
{
    [RequireComponent(typeof(Button))]
    public abstract class AddConfigButton : MonoBehaviour
    {
        private Button button;

        protected abstract void AddNewConfig();

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(AddNewConfig);
        }
    }
}
