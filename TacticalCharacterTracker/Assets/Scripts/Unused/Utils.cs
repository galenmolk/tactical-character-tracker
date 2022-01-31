using UnityEngine;

public static class Utils
{
    public static Color? GetColorFromHex(string hex)
    {
        return ColorUtility.TryParseHtmlString(hex, out Color color) ? color : null;
    }
}
