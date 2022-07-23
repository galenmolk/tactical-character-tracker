using Ebla.Models;
using Ebla.UI;

namespace Ebla.Editing
{
    public class EnemyEditingController : EditingController<EnemyControls, EnemyConfig>
    {
        protected override void SubscribeEditSlot()
        {
            EnemySlot.OnEditConfigSlot += HandleEditSlot;
        }

        protected override void UnsubscribeEditSlot()
        {
            EnemySlot.OnEditConfigSlot -= HandleEditSlot;
        }
    }
}
