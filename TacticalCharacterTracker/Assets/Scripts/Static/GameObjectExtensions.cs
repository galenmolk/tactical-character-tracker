using UnityEngine;

public static class GameObjectExtensions
{
    public static void DestroyChildrenOfType<T>(this GameObject go) where T : Behaviour
    {
        T[] components = go.GetComponentsInChildren<T>();
        for (int index = 0; index < components.Length; index++)
        {
            T c = components[index];
            c.transform.SetParent(null);
            Object.Destroy(c.gameObject);
        }
    }
}