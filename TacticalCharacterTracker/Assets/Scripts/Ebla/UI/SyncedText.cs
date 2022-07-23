using System;
using TMPro;
using UnityEngine;

namespace Ebla.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(TMP_Text))]
    public class SyncedText : MonoBehaviour
    {
        [SerializeField] private FontOption fontOption;
        
        private void OnValidate()
        {
            if (fontOption == null)
            {
                return;
            }

            GetComponent<TMP_Text>().font = fontOption.Font;
        }
    }
}
