using UnityEngine;

[CreateAssetMenu(fileName = "Color Code", menuName = "Custom/Color Code")]
public class ColorCode : ScriptableObject
{
   public string[] GetStrings()
   {
      return stringsToColorCode;
   }

   public Color GetColor()
   {
      return color;
   }
   
   [SerializeField] private string[] stringsToColorCode;
   [SerializeField] private Color color;
}
