using System;
using UnityEngine;

public static class CustomAbilityLibrary
{
    public static void TryAddCustomAbility(GameObject abilitySlotGameObject, string abilityName)
    {
        if (TryGetCustomAbilityForName(abilityName, out Type customAbility))
            abilitySlotGameObject.AddComponent(customAbility);
    }
    
    private static bool TryGetCustomAbilityForName(string name, out Type customAbility)
    {
        customAbility = name switch
        {
            CustomAbilityKeys.UNSTOPPABLE => typeof(Unstoppable),
            _ => null
        };

        return customAbility != null;
    }
}
