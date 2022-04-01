using System.Collections.Generic;
using HexedHeroes.Creator;
using UnityEngine.Events;

namespace HexedHeroes.Utils
{
    public class AbilityConfigEvent : UnityEvent<AbilityConfig> { }

    public class CooldownAbilitySlotEvent : UnityEvent<AbilitySlot> { }

    public class CharacterListEvent : UnityEvent<List<CharacterConfig>> { }

    public class CharacterConfigEvent : UnityEvent<CharacterConfig> { }

    public class EnemyOptionCardEvent : UnityEvent<EnemyOptionCard>
    {
    }

    public class AbilityOptionCardEvent : UnityEvent<AbilityOptionCard>
    {
    }
}