using Ebla.Utils;
using TMPro;
using UnityEngine;

namespace Ebla.UI
{
    [CreateAssetMenu(fileName = NAME, menuName = MENU)]
    public class FontOption : ScriptableObject
    {
        private const string NAME = nameof(FontOption);
        private const string MENU = EblaConsts.MENU_PATH + NAME;

        public TMP_FontAsset Font => font;
        [SerializeField] private TMP_FontAsset font;
    }
}
