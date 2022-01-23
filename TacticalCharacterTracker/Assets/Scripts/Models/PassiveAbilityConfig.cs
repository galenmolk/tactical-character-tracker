using System;

[Serializable]
public class PassiveAbilityConfig : AbilityConfig
{
     private const string PASSIVE_TEXT = "(Passive)";

     public override string GetCooldownDescription()
     {
          return PASSIVE_TEXT;
     }
}
