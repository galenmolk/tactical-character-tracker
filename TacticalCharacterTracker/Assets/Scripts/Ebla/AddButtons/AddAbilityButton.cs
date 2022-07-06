using Ebla.Models;
using UnityEngine;

namespace Ebla.AddButtons
{
    public class AddAbilityButton : AddConfigButton<AbilityConfig>
    {
        protected override AbilityConfig AddNewConfig()
        {
            Debug.Log("GetNewConfig");
            return ConfigFactory.NewAbility();
        }
    }
}
