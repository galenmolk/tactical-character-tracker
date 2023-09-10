using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class ColorCoder
{
    private static Dictionary<string, Color> colorCodeRegistry = new();

    private const string CLOSING_COLOR_TAG = "</color>";

    private static ColorCode[] colorCodes;

    public static void SetColorCodeRegistryOld(Dictionary<string, Color> _colorCodeRegistry)
    {
        colorCodeRegistry = _colorCodeRegistry;
    }

    public static void SetColorCodes(ColorCode[] _colorCodes)
    {
        colorCodes = _colorCodes;
    }

    // public static string GetColorCodedText2(string text)
    // {
    //     foreach ((string word, var color) in colorCodeRegistry)
    //     {
    //         if (word != "moves" || word != "move")
    //             continue;
    //         
    //         int occurenceCount = Regex.Matches(text, word).Count;
    //         Debug.Log($"{word} occurenceCount: {occurenceCount}");
    //         
    //         if (occurenceCount == 0)
    //             continue;
    //
    //         string replacement = GetRichTextWord(word, color);
    //         int wordLength = word.Length;
    //         
    //         for (int i = 0; i < occurenceCount; i++)
    //         {
    //             int index = text.IndexOf(word, StringComparison.Ordinal);
    //             
    //             if (index > 0 && (IsInvalid(text[index - 1]) || IsInvalid(text[index + wordLength])))
    //                 continue;
    //             
    //             text = text.Replace(word, replacement);
    //         }
    //     }
    //
    //     return text;
    // }

    private static string GetRichTextWord(string paddedWord, Color color)
    {
        return $"{GetOpeningColorTag(color)}{paddedWord}{CLOSING_COLOR_TAG}";
    }

    private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
    
    private static bool IsInvalid(char c)
    {
        if (c == '<' || c == '>')
            return true;
        
        string lowerC = c.ToString().ToLower();
        return ALPHABET.Contains(lowerC);
    }
    
    public static string GetColorCodedTextOld(string text)
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

    public static string GetColorCodedText(string inputText)
    {
        foreach (var colorCode in colorCodes)
        {
            foreach (var wordToColorCode in colorCode.GetStrings())
            {
                var pattern = $@"\b{Regex.Escape(wordToColorCode)}\b";
                var colorCodeStart = $"<color=#{ColorUtility.ToHtmlStringRGBA(colorCode.GetColor())}>";
                var colorCodeEnd = "</color>";
                inputText = Regex.Replace(inputText, pattern, $"{colorCodeStart}$&{colorCodeEnd}", RegexOptions.IgnoreCase);
            }
        }

        return inputText;
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
    
    private static readonly Dictionary<string, Color> ColorCodeRegistry = new();
    
    private void Awake()
    {
        //BuildColorCodeRegistry();
        ColorCoder.SetColorCodes(colorCodes);
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
            ColorCodeRegistry.TryAdd(stringToCode, color);
        }
    }
}
