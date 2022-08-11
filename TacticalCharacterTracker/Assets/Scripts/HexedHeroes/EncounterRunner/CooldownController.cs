using System;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    public class CooldownController : MonoBehaviour
    {
        public static event Action ReduceActive;

        public void ReduceActiveButtonClicked()
        {
            ReduceActive?.Invoke();
        }
    }
}
