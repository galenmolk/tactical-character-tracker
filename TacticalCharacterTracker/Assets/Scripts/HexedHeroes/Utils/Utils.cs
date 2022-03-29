using UnityEngine;

namespace HexedHeroes.Utils
{
    public static class Utils
    {
        public static Color? GetColorFromHex(string hex, Color? fallbackColor = null)
        {
            return ColorUtility.TryParseHtmlString(hex, out Color color) ? color : fallbackColor;
        }
    }
}
