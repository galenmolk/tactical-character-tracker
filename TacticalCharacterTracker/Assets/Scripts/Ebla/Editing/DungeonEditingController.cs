using Ebla.Models;
using Ebla.UI.Slots;

namespace Ebla.Editing
{
    public class DungeonEditingController : EditingController<DungeonControls, DungeonConfig>
    {
        protected override void SubscribeEditSlot()
        {
            DungeonSlot.OnEditConfigSlot += HandleEditSlot;
        }

        protected override void UnsubscribeEditSlot()
        {
            DungeonSlot.OnEditConfigSlot -= HandleEditSlot;
        }
    }
}
