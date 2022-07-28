using Ebla.Utils;
using UnityEngine;

namespace Ebla.AddButtons
{
    public class AddFolderButton : AddConfigButton
    {
        [SerializeField] private string buttonName;
        
        public override void AddNewConfig()
        {
            ConfigFactory.Folder();
        }

        protected override string GetButtonName()
        {
            return buttonName;
        }
    }
}
