using Ebla.Models;
using UnityEngine;

namespace Ebla.UI
{
    [CreateAssetMenu(fileName = NAME, menuName = MENU)]
    public class ConfigParams : ScriptableObject
    {
        private const string NAME = "Config Params";
        private const string MENU = "Ebla/" + NAME;

        public BaseConfig.Type Type => type;
        public Color Color => color;

        [SerializeField] private BaseConfig.Type type;
        [SerializeField] private Color color;
    }
}
