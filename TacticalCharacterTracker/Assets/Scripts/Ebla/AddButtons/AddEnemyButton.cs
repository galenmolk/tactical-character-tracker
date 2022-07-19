namespace Ebla.AddButtons
{
    public class AddEnemyButton : AddConfigButton
    {
        protected override void AddNewConfig()
        {
            ConfigFactory.Enemy();
        }
    }
}
