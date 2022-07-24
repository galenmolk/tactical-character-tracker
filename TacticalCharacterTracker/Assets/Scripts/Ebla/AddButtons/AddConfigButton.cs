using Ebla.UI;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.AddButtons
{
    [RequireComponent(typeof(Button))]
    public abstract class AddConfigButton : MonoBehaviour
    {
        [SerializeField] private ConfigParams configParams;
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;
        
        private Button button;
        
        // Called by ContextMenuBehaviour.
        [UsedImplicitly]
        public abstract void AddNewConfig();

        private void Awake()
        {
            text.text = configParams.ConfigName;
            image.color = configParams.Color;
            button = GetComponent<Button>();
            button.onClick.AddListener(AddNewConfig);
        }
    }
}
