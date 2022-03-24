using UnityEngine.Events;

public abstract class ConfirmationParams
{
    public string HeaderText => headerText;
    public string DescriptionText => descriptionText;
    public string ConfirmButtonText => confirmButtonText;
    public string DenyButtonText => denyButtonText;
    public UnityAction Action => action;

    protected string headerText;
    protected string descriptionText;
    protected string denyButtonText;
    protected string confirmButtonText;
    protected UnityAction action;
}

public class DeleteDungeonParams : ConfirmationParams
{
    private const string DELETE_DUNGEON_HEADER_TEXT = "Are You Sure?";
    private const string DENY_BUTTON_TEXT = "Cancel";
    private const string CONFIRM_BUTTON_TEXT = "Delete";
    
    private DungeonCard dungeonCard;
    
    public DeleteDungeonParams(DungeonCard dungeon)
    {
        dungeonCard = dungeon;
        headerText = DELETE_DUNGEON_HEADER_TEXT;
        descriptionText = GetDescriptionText(dungeon.Config);
        confirmButtonText = CONFIRM_BUTTON_TEXT;
        denyButtonText = DENY_BUTTON_TEXT;
        action = DeleteDungeon;
    }

    private string GetDescriptionText(DungeonConfig config)
    {
        return $"Delete Dungeon: <i>{config.name}</i>";
    }
    
    private void DeleteDungeon()
    {
        DungeonDisplay.Instance.DeleteDungeon(dungeonCard);
    }
}
