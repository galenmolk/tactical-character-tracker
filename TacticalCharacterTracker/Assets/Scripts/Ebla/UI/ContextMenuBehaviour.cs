using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ebla.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ContextMenuBehaviour : MonoBehaviour
    {
        [Serializable]
        public class Option
        {
            public string GetLabel()
            {
                if (!platformDependent)
                {
                    return label;
                }
                
                #if UNITY_STANDALONE_WIN
                return winLabel;
                #elif UNITY_STANDALONE_OSX
                return osxLabel;
                #else
                return label;
                #endif
            }

            [SerializeField] private bool platformDependent;
            [SerializeField] private string osxLabel;
            [SerializeField] private string winLabel;
            
            [SerializeField] private string label;
            
            public UnityEvent Action => action;
            [SerializeField] private UnityEvent action;

            public bool AddDividerAbove => addDividerAbove;
            [SerializeField] private bool addDividerAbove;

            public bool AddDividerBelow => addDividerBelow;
            [SerializeField] private bool addDividerBelow;
        }

        public static event Action<ContextMenuBehaviour> OnContextMenuRequested;

        public Option[] Options => options;
        [SerializeField] private Option[] options;

        public RectTransform RectTransform { get; private set; }

        public void OpenContextMenu()
        {
            Vector2 position = RectTransformUtility.WorldToScreenPoint(null, RectTransform.position);
            OnContextMenuRequested?.Invoke(this);
        }

        private void Awake()
        {
            RectTransform = transform as RectTransform;
        }
    }
}
