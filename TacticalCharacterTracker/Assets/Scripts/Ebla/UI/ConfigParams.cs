using UnityEngine;

namespace Ebla.UI
{
    [CreateAssetMenu(fileName = NAME, menuName = MENU)]
    public class ConfigParams : ScriptableObject
    {
        private const string NAME = "Config Params";
        private const string MENU = "Ebla/" + NAME;

        public string ConfigName => configName;
        public Color Color => color;

        [SerializeField] private string configName;
        [SerializeField] private Color color;
        
    }
}
