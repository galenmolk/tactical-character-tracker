using HexedHeroes.Utils;
using UnityEngine;

public abstract class MainPanel<T> : Singleton<T> where T : MonoBehaviour
{
    public abstract void Open();
    public abstract void Close();
}
