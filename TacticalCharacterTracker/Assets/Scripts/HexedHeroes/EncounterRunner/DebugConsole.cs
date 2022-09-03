using MolkExtras;
using TMPro;
using UnityEngine;
using System.Diagnostics;

namespace HexedHeroes.EncounterRunner
{
    public class DebugConsole : Singleton<DebugConsole>
    {
        private int index;
        [SerializeField] private TMP_Text output;
        
        [Conditional(DefineSymbols.QA_BUILD)]
        public void Log(object obj)
        {
            output.text += $"{index++}: {obj}\n";
        }
    }
}
