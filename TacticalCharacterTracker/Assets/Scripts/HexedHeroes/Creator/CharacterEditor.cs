using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class CharacterEditor : MainPanel<CharacterEditor>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_InputField nameText;
    [SerializeField] private TMP_InputField defenseText;
    [SerializeField] private TMP_InputField healthText;
    [SerializeField] private TMP_InputField speedText;

    [SerializeField] private AbilityButton abilityButtonPrefab;
    [SerializeField] private Transform abilityParent;

    private CharacterCard characterCard;
    private readonly List<AbilityCard> abilityCards = new();

    private AbilityButton activeButton;

    public override void Open()
    {
        canvasGroup.SetIsActive(true);
    }

    public override void Close()
    {
        canvasGroup.SetIsActive(false);
    }
    
    public void Back()
    {
        Close();
    }
    
    public void Initialize(CharacterCard card)
    {
        characterCard = card;
        DisplayStats();
        DisplayAbilities();
    }

    public void AddAbilityButtonClicked()
    {
        AbilityConfig abilityConfig = new AbilityConfig();
        characterCard.Config.abilities.Add(abilityConfig);
        AbilityCreator.Instance.Open(abilityConfig);
    }
    
    private void AbilityButtonClicked(AbilityButton abilityButton)
    {
        activeButton = abilityButton;
        AbilityCreator.Instance.Open(abilityButton.AbilityConfig);
    }

    public void Save()
    {
        if (string.IsNullOrWhiteSpace(nameText.text))
            return;
        
        characterCard.Config.name = nameText.text;
        characterCard.Config.defense = int.Parse(defenseText.text);
        characterCard.Config.health = int.Parse(healthText.text);
        characterCard.Config.speed = int.Parse(speedText.text);
        string json = JsonConvert.SerializeObject(characterCard.Config);
        Debug.Log(json);
    }

    public void UpdateActiveAbilityButton(AbilityConfig abilityConfig)
    {
        if (activeButton == null || activeButton.AbilityConfig != abilityConfig)
            CreateActiveButton(abilityConfig);
        else
            activeButton.Display(abilityConfig);
    }
    
    public void ClearActiveAbilityButton()
    {
        activeButton = null;
    }

    private void DisplayStats()
    {
        nameText.text = characterCard.Config.name;
        defenseText.text = characterCard.Config.defense.ToString();
        healthText.text = characterCard.Config.health.ToString();
        speedText.text = characterCard.Config.speed.ToString();
    }
    
    private void DisplayAbilities()
    {
        foreach (var ability in characterCard.Config.abilities)
        {
            CreateButton(ability);
        }
    }

    private void CreateActiveButton(AbilityConfig config)
    {
        activeButton = CreateButton(config);
    }

    private AbilityButton CreateButton(AbilityConfig config)
    {
        AbilityButton button = Instantiate(abilityButtonPrefab, abilityParent);
        button.Initialize(config, AbilityButtonClicked);
        return button;
    }

}