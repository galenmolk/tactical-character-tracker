using MolkExtras;
using UnityEngine;

namespace HexedHeroes.EncounterRunner
{
    [CreateAssetMenu(fileName = "ResolutionAppEvent", menuName = "Custom Assets/ResolutionAppEvent")]
    public class ResolutionAppEvent : GenericAppEvent<(int, int, bool)>
    {
        
    }
}
