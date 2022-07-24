using Ebla.Utils;

namespace Ebla.AddButtons
{
    public class AddAbilityButton : AddConfigButton
    {
        public override void AddNewConfig()
        {
            ConfigFactory.Ability();
        }
    }
}
