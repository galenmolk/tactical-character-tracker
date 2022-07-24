using System.Collections.Generic;
using System.Linq;
using Ebla.UI;
using Ebla.UI.Slots;

namespace Ebla
{
    public static class FileSorter
    {
        public static IEnumerable<AbilitySlot> SortByName(IEnumerable<AbilitySlot> abilitySlots)
        {
            List<AbilitySlot> sortByName = abilitySlots.ToList();
            if (sortByName.Count < 2)
                return sortByName;

            return sortByName.OrderBy(fileSlot => fileSlot.Config.Name);
        }
        
        public static IEnumerable<EnemySlot> SortByName(IEnumerable<EnemySlot> enemySlots)
        {
            List<EnemySlot> sortByName = enemySlots.ToList();
            if (sortByName.Count < 2)
                return sortByName;

            return sortByName.OrderBy(fileSlot => fileSlot.Config.Name);
        }
    }
}
