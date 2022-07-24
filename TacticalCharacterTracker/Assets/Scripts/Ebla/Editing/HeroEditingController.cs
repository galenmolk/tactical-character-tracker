using Ebla.Models;
using Ebla.UI.Slots;

namespace Ebla.Editing
{
    public class HeroEditingController : EditingController<HeroControls, HeroConfig>
    {
        protected override void SubscribeEditSlot()
        {
            HeroSlot.OnEditConfigSlot += HandleEditSlot;
        }

        protected override void UnsubscribeEditSlot()
        {
            HeroSlot.OnEditConfigSlot -= HandleEditSlot;
        }
    }
}
