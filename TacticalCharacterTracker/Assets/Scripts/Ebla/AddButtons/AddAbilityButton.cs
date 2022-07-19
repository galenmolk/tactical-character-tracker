using UnityEngine;

namespace Ebla.AddButtons
{
    public class AddAbilityButton : AddConfigButton
    {
        protected override void AddNewConfig()
        {
            Debug.Log("GetNewConfig");
            ConfigFactory.Ability();
        }
    }
}
