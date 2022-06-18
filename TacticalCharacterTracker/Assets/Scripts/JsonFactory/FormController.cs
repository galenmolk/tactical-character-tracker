using UnityEngine;

namespace JsonFactory
{
    public class FormController : MonoBehaviour
    {
        [SerializeField] private FormAsset formAsset;

        [ContextMenu("Serialize")]
        public void Serialize()
        {
            Debug.Log(formAsset.Serialize());
        }
    }
}
