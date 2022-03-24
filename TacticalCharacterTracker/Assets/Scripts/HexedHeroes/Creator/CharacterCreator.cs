using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class CharacterCreator : Singleton<CharacterCreator>
{
    [SerializeField] private TMP_InputField nameText;
    [SerializeField] private TMP_InputField defenseText;
    [SerializeField] private TMP_InputField healthText;
    [SerializeField] private TMP_InputField speedText;

    [SerializeField] private AbilityButton abilityButtonPrefab;
    [SerializeField] private Transform abilityParent;

    private CharacterConfig config;
    private AbilityButton activeButton;

    protected override void OnAwake()
    {
        base.OnAwake();
        OpenCharacter(new CharacterConfig());
    }

    public void OpenCharacter(CharacterConfig _config)
    {
        config = _config;
        DisplayStats();
        DisplayAbilities();
    }

    public void AddAbilityButtonClicked()
    {
        AbilityConfig abilityConfig = new AbilityConfig();
        config.abilities.Add(abilityConfig);
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
        
        config.name = nameText.text;
        config.defense = int.Parse(defenseText.text);
        config.health = int.Parse(healthText.text);
        config.speed = int.Parse(speedText.text);
        string json = JsonConvert.SerializeObject(config);
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
        nameText.text = config.name;
        defenseText.text = config.defense.ToString();
        healthText.text = config.health.ToString();
        speedText.text = config.speed.ToString();
    }
    
    private void DisplayAbilities()
    {
        foreach (var ability in config.abilities)
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
