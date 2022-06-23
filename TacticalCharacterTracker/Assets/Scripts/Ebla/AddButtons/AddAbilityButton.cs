using Ebla.Editing;
using Ebla.Models;

namespace Ebla.AddButtons
{
    public class AddAbilityButton : AddConfigButton<AbilityConfig>
    {
        protected override AbilityConfig GetNewConfig()
        {
            return ConfigFactory.NewAbility();
        }
    }
}
