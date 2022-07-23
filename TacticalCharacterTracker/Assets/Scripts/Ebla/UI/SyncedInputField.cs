using TMPro;
using UnityEngine;

namespace Ebla.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(TMP_InputField))]
    public class SyncedInputField : MonoBehaviour
    {
        [SerializeField] private FontOption fontOption;
        
        private TMP_InputField inputField;

        private void OnValidate()
        {
            if (fontOption == null)
            {
                return;
            }

            GetComponent<TMP_InputField>().fontAsset = fontOption.Font;
        }       
    }
}
