using UnityEngine;

namespace HexedHeroes.Utils
{
    public static class GameObjectExtensions
    {
        public static void DestroyChildrenOfType<T>(this GameObject go) where T : Behaviour
        {
            var components = go.GetComponentsInChildren<T>();
            foreach (var c in components)
            {
                c.transform.SetParent(null);
                Object.Destroy(c.gameObject);
            }
        }

        public static void TrySetActive(this GameObject go, bool active)
        {
            if (go.activeSelf == active)
                return;

            go.SetActive(active);
        }
    }
}