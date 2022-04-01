using HexedHeroes.Creator;
using UnityEngine;

public class DeleteCharacterParams : ConfirmationParams
{
    private const string DELETE_DUNGEON_HEADER_TEXT = "Are You Sure?";
    private const string DENY_BUTTON_TEXT = "Cancel";
    private const string CONFIRM_BUTTON_TEXT = "Delete";
    private readonly Color? confirmColor = new Color(1f, 0.514151f, 0.514151f, 1f);

    private readonly CharacterCard characterCard;
    
    public DeleteCharacterParams(CharacterCard card)
    {
        characterCard = card;
        headerText = DELETE_DUNGEON_HEADER_TEXT;
        descriptionText = $"Delete Character: <i>{card.Config.name}</i>";
        confirmButtonText = CONFIRM_BUTTON_TEXT;
        denyButtonText = DENY_BUTTON_TEXT;
        confirmButtonColor = confirmColor;
        action = DeleteDungeon;
    }

    private void DeleteDungeon()
    {
        CharacterDisplay.Instance.DeleteCharacter(characterCard);
    }
}
