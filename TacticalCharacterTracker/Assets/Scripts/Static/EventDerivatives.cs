using System.Collections.Generic;
using UnityEngine.Events;

public class AbilityConfigEvent : UnityEvent<AbilityConfig> { }

public class CooldownAbilitySlotEvent : UnityEvent<CooldownAbilitySlot> { }

public class CharacterListEvent : UnityEvent<List<CharacterConfig>> { }

public class CharacterConfigEvent : UnityEvent<CharacterConfig> { }
