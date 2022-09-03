using System;
using System.Collections.Generic;
using Ebla.Libraries;
using Ebla.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public class DungeonConfig : BaseConfig
    {
        [UsedImplicitly]
        public static event Action<DungeonConfig> OnLoadIntoFolder;
        
        public override string BaseName => "Untitled Dungeon";

        [JsonProperty(ConfigKeys.ENCOUNTERS_KEY)]
        public List<EncounterConfig> Encounters { get; private set; }

        public void UpdateEncounters(List<EncounterConfig> newEncounters)
        {
            Encounters = newEncounters;
            InvokeConfigModified();
        }
        
        protected override void RemoveConfigFromLibrary()
        {
            DungeonLibrarian.Instance.Remove(this);
        }
    }
}
