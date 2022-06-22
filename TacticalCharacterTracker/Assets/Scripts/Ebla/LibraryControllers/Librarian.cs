using Ebla.Models;
using MolkExtras;
using Newtonsoft.Json;
using UnityEngine;

namespace Ebla
{
    public class Librarian : Singleton<Librarian>
    {
        public AbilityController Abilities { get; private set; }
        
        [SerializeField] private TextAsset abilitiesJson;
        
        private void Awake()
        {
            CreateControllers();
        }

        private void CreateControllers()
        {
            var abilityLibraryConfig = JsonConvert.DeserializeObject<AbilityLibraryConfig>(abilitiesJson.text);
            Abilities = new AbilityController(abilityLibraryConfig ?? new AbilityLibraryConfig());
        }
    }
}
