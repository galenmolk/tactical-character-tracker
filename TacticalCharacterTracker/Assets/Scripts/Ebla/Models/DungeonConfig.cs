namespace Ebla.Models
{
    public class DungeonConfig : BaseConfig
    {
        public DungeonConfig()
        {
        }

        public override string BaseName { get; }
        public override Type ConfigType => Type.Dungeon;
    }
}
