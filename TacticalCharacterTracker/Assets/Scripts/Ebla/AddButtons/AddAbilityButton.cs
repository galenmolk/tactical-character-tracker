using UnityEngine;

namespace Ebla.AddButtons
{
    public class AddAbilityButton : AddConfigButton
    {
        public override void AddNewConfig()
        {
            Debug.Log("GetNewConfig");
            ConfigFactory.Ability();
        }
    }
}
