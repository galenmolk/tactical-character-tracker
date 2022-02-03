using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCreator : Singleton<AbilityCreator>
{
    [SerializeField] private TMP_InputField nameText;
    [SerializeField] private GameObject cooldownArea;
    [SerializeField] private TMP_InputField cooldownText;
    [SerializeField] private Toggle passiveToggle;
    [SerializeField] private TMP_InputField descriptionText;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private AbilityConfig abilityConfig;

    public void Open(AbilityConfig abilityConfig)
    {
        canvasGroup.SetIsActive(true);
        this.abilityConfig = abilityConfig;
        nameText.text = abilityConfig.name;
        descriptionText.text = abilityConfig.description;
        cooldownText.text = abilityConfig.cooldown.ToString();
        SetIsPassive(abilityConfig.isPassive);
    }

    public void Close()
    {
        canvasGroup.SetIsActive(false);
        CharacterCreator.Instance.ClearActiveAbilityButton();
    }

    public void Save()
    {
        if (string.IsNullOrWhiteSpace(nameText.text))
            return;
        
        abilityConfig.name = nameText.text;
        abilityConfig.description = descriptionText.text;
        abilityConfig.isPassive = passiveToggle.isOn;
        abilityConfig.cooldown = abilityConfig.isPassive ? 0 : int.Parse(cooldownText.text);
        CharacterCreator.Instance.UpdateActiveAbilityButton(abilityConfig);
        string json = JsonConvert.SerializeObject(abilityConfig);
        Debug.Log(json);
    }
    
    public void SetIsPassive(bool isPassive)
    {
        cooldownArea.gameObject.SetActive(!isPassive);
        passiveToggle.SetIsOnWithoutNotify(isPassive);
    }
    
    protected override void OnAwake()
    {
        base.OnAwake();
        canvasGroup.SetIsActive(false);
    }
}
