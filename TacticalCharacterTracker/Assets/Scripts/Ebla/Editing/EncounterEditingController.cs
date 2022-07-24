using Ebla.Models;
using Ebla.UI.Slots;

namespace Ebla.Editing
{
    public class EncounterEditingController : EditingController<EncounterControls, EncounterConfig>
    {
        protected override void SubscribeEditSlot()
        {
            EncounterSlot.OnEditConfigSlot += HandleEditSlot;
        }

        protected override void UnsubscribeEditSlot()
        {
            EncounterSlot.OnEditConfigSlot -= HandleEditSlot;
        }
    }
}
