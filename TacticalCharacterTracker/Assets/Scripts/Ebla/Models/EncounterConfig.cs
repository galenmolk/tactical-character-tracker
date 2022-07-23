namespace Ebla.Models
{
    public class EncounterConfig : BaseConfig
    {
        public EncounterConfig()
        {
        }

        public override string BaseName => "Untitled Encounter";
        protected override void RemoveConfigFromLibrary()
        {
            throw new System.NotImplementedException();
        }
    }
}