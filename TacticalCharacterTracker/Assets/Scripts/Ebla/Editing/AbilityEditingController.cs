using Ebla.Models;
using Ebla.UI.Slots;

namespace Ebla.Editing
{
    public class AbilityEditingController : EditingController<AbilityControls, AbilityConfig>
    {
        protected override void SubscribeEditSlot()
        {
            AbilitySlot.OnEditConfigSlot += HandleEditSlot;
        }
    
        protected override void UnsubscribeEditSlot()
        {
            AbilitySlot.OnEditConfigSlot -= HandleEditSlot;
        }
    }
}
