namespace Ebla.Models
{
    public class EncounterConfig : BaseConfig
    {
        public override Type ConfigType => Type.Encounter;
        
        public EncounterConfig()
        {
        }

        public override string BaseName => "Untitled Encounter";
    }
}