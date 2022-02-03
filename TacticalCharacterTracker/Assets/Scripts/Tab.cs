using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Studiosaurus {
    public static class Tab
    {
        private static readonly List<Selectable> currentSelectables = new List<Selectable>();
        private static readonly List<GameObject> currentSelectablesAsGameObjects = new List<GameObject>(); 
        private static int selectableIndex = -1;

        public static void SetCurrentSelectables(SelectableWindow window)
        {
            Reset();
            for (int i = 0, count = window.Selectables.Length; i < count; i++)
            {
                Selectable selectable = window.Selectables[i];
                currentSelectables.Add(selectable);
                currentSelectablesAsGameObjects.Add(selectable.gameObject);
            }
        }

        public static void SelectNext()
        {
            int currentCount = currentSelectables.Count;

            if (currentCount <= 0)
                return;

            int direction = Input.GetKey(KeyCode.LeftShift) ? -1 : 1;
            selectableIndex += direction;

            if (selectableIndex >= currentCount)
                selectableIndex = 0;

            if (selectableIndex < 0)
                selectableIndex = currentCount - 1;

            currentSelectables[selectableIndex]?.Select();
        }

        public static void NewObjectSelected(GameObject gameObject)
        {
            if (!currentSelectablesAsGameObjects.Contains(gameObject))
                return;

            selectableIndex = currentSelectablesAsGameObjects.IndexOf(gameObject);
        }

        private static void Reset()
        {
            currentSelectables.Clear();
            currentSelectablesAsGameObjects.Clear();
            selectableIndex = -1;
        }
    }
}
