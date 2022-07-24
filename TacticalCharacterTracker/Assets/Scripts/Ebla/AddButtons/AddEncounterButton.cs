using Ebla.Utils;

namespace Ebla.AddButtons
{
    public class AddEncounterButton : AddConfigButton
    {
        public override void AddNewConfig()
        {
            ConfigFactory.Encounter();
        }
    }
}
