using Ebla.UI;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.AddButtons
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public abstract class AddConfigButton : MonoBehaviour
    {
        [SerializeField] private ConfigParams configParams;
        
        private Button button;
        private Image image;
        
        // Called by ContextMenuBehaviour.
        [UsedImplicitly]
        public abstract void AddNewConfig();

        private void Awake()
        {
            image = GetComponent<Image>();
            image.color = configParams.Color;
            button = GetComponent<Button>();
            button.onClick.AddListener(AddNewConfig);
        }
    }
}
