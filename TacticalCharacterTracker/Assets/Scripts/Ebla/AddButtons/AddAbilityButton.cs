using Ebla.AddButtons;
using Ebla.Models;

namespace Ebla
{
    public class AddAbilityButton : AddConfigButton<AbilityConfig>
    {
        protected override AbilityConfig GetNewConfig()
        {
            return ConfigFactory.NewAbility();
        }
    }
}
