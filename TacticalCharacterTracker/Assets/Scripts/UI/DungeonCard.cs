using TMPro;
using UnityEngine;

public class DungeonCard : MonoBehaviour
{
    [SerializeField] private TMP_Text dungeonNameText;

    private DungeonConfig DungeonConfig { get; set; }

    public string DungeonName => dungeonNameText.text;
    
    public void Initialize(DungeonConfig config)
    {
        DungeonConfig = config;
        dungeonNameText.text = config.name;
    }
    
    public void Delete()
    {
        Debug.Log("Delete");
        DungeonDisplay.Instance.DeleteDungeon(this);
    }

    public void Edit()
    {
        DungeonDisplay.Instance.Close();
        DungeonEditor.Instance.Open();
        DungeonEditor.Instance.Initialize(this);
    }

    public void Play()
    {
        Debug.Log("Play");
    }

    public void UpdateName(string name)
    {
        Debug.Log("UpdateName");
        DungeonConfig.name = name;
        dungeonNameText.text = name;
    }
}
