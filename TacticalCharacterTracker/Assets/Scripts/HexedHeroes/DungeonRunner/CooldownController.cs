using UnityEngine;

namespace HexedHeroes.DungeonRunner
{
    public class CooldownController : MonoBehaviour
    {
        public delegate void ReduceActive();

        public static event ReduceActive reduceActive;

        public void ReduceActiveCooldownsButtonClicked()
        {
            reduceActive?.Invoke();
        }
    }
}
