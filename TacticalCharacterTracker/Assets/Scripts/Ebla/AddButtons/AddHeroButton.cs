using Ebla.Utils;

namespace Ebla.AddButtons
{
    public class AddHeroButton : AddConfigButton
    {
        public override void AddNewConfig()
        {
            ConfigFactory.Hero();
        }
    }
}
