using System;
using Ebla.Utils;
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

        private void Update()
        {
            if (Input.GetKeyDown(HotKeys.ReduceCooldowns))
            {
                ReduceActiveButtonClicked();
            }
        }
    }
}
