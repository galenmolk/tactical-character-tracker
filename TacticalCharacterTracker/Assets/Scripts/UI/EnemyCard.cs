using TMPro;
using UnityEngine;

public class EnemyCard : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyTypeText;
    [SerializeField] private TMP_InputField quantityInputField;
    
    public EnemyConfig Config { get; private set; }

    public void Initialize(EnemyConfig config)
    {
        Config = config;
        enemyTypeText.text = config.characterType.name;
        quantityInputField.text = config.quantity.ToString();
    }
    
    public void Delete()
    {
        Debug.Log("Delete");
        DungeonEditor.Instance.DeleteEnemyCard(this);
    }

    public void Edit()
    {
        Debug.Log("Editing Enemy");
    }

    public void OnQuantityEdited(string text)
    {
        bool isTextValid = int.TryParse(text, out int quantity);
        
        if (!isTextValid || quantity < 1)
        {
            quantity = EnemyConfig.DEFAULT_QUANTITY;
            quantityInputField.text = quantity.ToString();
        }

        Config.quantity = quantity;
    }
}
