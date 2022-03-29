using TMPro;
using UnityEngine;

public class DungeonCard : MonoBehaviour
{
    [SerializeField] private TMP_Text dungeonNameText;

    public DungeonConfig Config { get; private set; }

    public string DungeonName => dungeonNameText.text;
    
    public void Initialize(DungeonConfig config)
    {
        Config = config;
        dungeonNameText.text = config.name;
    }
    
    public void TryDelete()
    {
        Debug.Log("Try Delete");
        ConfirmationPanel.Instance.Open(new DeleteDungeonParams(this));
    }

    public void Edit()
    {
        DungeonEditor.Instance.Initialize(this);
        DungeonEditor.Instance.Open();
        DungeonDisplay.Instance.Close();
    }

    public void Play()
    {
        Debug.Log("Play");
    }

    public void UpdateName(string name)
    {
        Debug.Log("UpdateName");
        Config.name = name;
        dungeonNameText.text = name;
    }
}
