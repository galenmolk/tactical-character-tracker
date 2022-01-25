using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class ColorCoder
{
    private static Dictionary<string, Color> colorCodeRegistry;

    private const string CLOSING_COLOR_TAG = "</color>";
    
    public static void SetColorCodeRegistry(Dictionary<string, Color> _colorCodeRegistry)
    {
        colorCodeRegistry = _colorCodeRegistry;
    }

    public static string GetColorCodedText(string text)
    {
        foreach (var code in colorCodeRegistry)
        {
            int codedWordLength = code.Key.Length;
            
            int index = 0;
            int max = text.Length - codedWordLength;
            
            while (index < max)
            {
                if (string.Equals(code.Key, text.ToLower().Substring(index, codedWordLength)))
                {
                    string openingColorTag = GetOpeningColorTag(code.Value);
                    int openingColorTagLength = openingColorTag.Length;
                    text = text.Insert(index, openingColorTag);
                    int closingIndex = index + codedWordLength + openingColorTagLength;
                    text = text.Insert(closingIndex, CLOSING_COLOR_TAG);
                    index = closingIndex + CLOSING_COLOR_TAG.Length;
                    max += openingColorTagLength + CLOSING_COLOR_TAG.Length;
                }
                else
                {
                    index++;
                }
            }
        }
        
        return text;
    }

    private static string GetOpeningColorTag(Color color)
    {
        string hex = ColorUtility.ToHtmlStringRGBA(color);
        return $"<color=#{hex}>";
    }
}

public class ColorCodeRegistryBuilder : MonoBehaviour
{
    [SerializeField] private ColorCode[] colorCodes;
    
    private static Dictionary<string, Color> colorCodeRegistry = new Dictionary<string, Color>();
    
    private void Awake()
    {
        BuildColorCodeRegistry();
        ColorCoder.SetColorCodeRegistry(colorCodeRegistry);
    }

    private void BuildColorCodeRegistry()
    {
        foreach (var colorCode in colorCodes)
        {
            AddColorCode(colorCode);
        }
    }

    private void AddColorCode(ColorCode colorCode)
    {
        Color color = colorCode.GetColor();
        string[] strings = colorCode.GetStrings();
        
        foreach (var stringToCode in strings)
        {
            colorCodeRegistry.Add(stringToCode, color);
        }
    }
}
