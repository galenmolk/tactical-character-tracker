using UnityEngine;
using UnityEngine.Events;

namespace HexedHeroes.EncounterRunner
{
    public class QaBuildBehaviour : MonoBehaviour
    {
        [SerializeField] private UnityEvent onQaBuildEnabled;
        [SerializeField] private UnityEvent onQaBuildDisabled;

        private void Awake()
        {
            #if QA_BUILD
            onQaBuildEnabled?.Invoke();
            #else
            onQaBuildDisabled?.Invoke();
            #endif
        }
    }
}
