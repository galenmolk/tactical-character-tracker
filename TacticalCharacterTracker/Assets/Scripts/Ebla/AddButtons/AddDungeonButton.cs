using Ebla.Utils;

namespace Ebla.AddButtons
{
    public class AddDungeonButton : AddConfigButton
    {
        public override void AddNewConfig()
        {
            ConfigFactory.Dungeon();
        }
    }
}
