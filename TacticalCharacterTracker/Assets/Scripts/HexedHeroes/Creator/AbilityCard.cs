using HexedHeroes.Models;
using TMPro;
using UnityEngine;

namespace HexedHeroes.Creator
{
    public class AbilityCard : MonoBehaviour
    {
        public AbilityConfig AbilityConfig { get; private set; }

        [SerializeField] private TMP_Text cardName;
        [SerializeField] private TMP_Text cooldownText;

        public void Initialize(AbilityConfig _abilityConfig)
        {
            Display(_abilityConfig);
        }

        public void Display(AbilityConfig _abilityConfig)
        {
            AbilityConfig = _abilityConfig;
            cardName.text = AbilityConfig.name;
            cooldownText.text = AbilityConfig.isPassive ? AbilityConfig.PASSIVE_TEXT : AbilityConfig.cooldown.ToString();
        }
        
        public void Delete()
        {
            Debug.Log("Delete");
            CharacterEditor.Instance.DeleteAbilityCard(this);
        }
    }
}
