using TMPro;
using UnityEngine;

public class EnemyCard : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyTypeText;
    [SerializeField] private TMP_InputField quantityInputField;
    
    public EnemyTypeConfig TypeConfig { get; private set; }

    public void Initialize(EnemyTypeConfig typeConfig)
    {
        TypeConfig = typeConfig;
        enemyTypeText.text = typeConfig.character.name;
        quantityInputField.text = typeConfig.quantity.ToString();
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
            quantity = EnemyTypeConfig.DEFAULT_QUANTITY;
            quantityInputField.text = quantity.ToString();
        }

        TypeConfig.quantity = quantity;
    }
}
