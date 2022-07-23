namespace Ebla.Models
{
    public class DungeonConfig : BaseConfig
    {
        public DungeonConfig()
        {
        }

        public override string BaseName { get; }
        protected override void RemoveConfigFromLibrary()
        {
            throw new System.NotImplementedException();
        }
    }
}
