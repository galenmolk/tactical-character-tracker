using Ebla.Utils;
using UnityEngine;

namespace Ebla.UI
{
    [CreateAssetMenu(fileName = NAME, menuName = MENU)]
    public class ConfigParams : ScriptableObject
    {
        private const string NAME = "Config Params";
        private const string MENU = EblaConsts.MENU_PATH + NAME;

        public string ConfigName => configName;
        public Color Color => color;
        public Sprite Icon => icon;
        
        [SerializeField] private string configName;
        [SerializeField] private Color color;
        [SerializeField] private Sprite icon;
    }
}
