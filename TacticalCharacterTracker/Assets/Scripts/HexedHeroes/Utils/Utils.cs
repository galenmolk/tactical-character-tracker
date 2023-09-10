using UnityEngine;

namespace HexedHeroes.Utils
{
    public static class Utils
    {
        public static Color? GetColorFromHex(string hex, Color? fallbackColor = null)
        {
            return ColorUtility.TryParseHtmlString(hex, out Color color) ? color : fallbackColor;
        }

        public static Color RandomColor()
        {
            System.Random random = new();
            float r = random.Next(0, 101) / 100f;
            float g = random.Next(0, 101) / 100f;
            float b = random.Next(0, 101) / 100f;
            return new Color(r, g, b);
        }
    }
}
