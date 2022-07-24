using Ebla.Utils;

namespace Ebla.AddButtons
{
    public class AddEnemyButton : AddConfigButton
    {
        public override void AddNewConfig()
        {
            ConfigFactory.Enemy();
        }
    }
}
