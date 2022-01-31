using UnityEngine;

[CreateAssetMenu(fileName = "Reselect", menuName = "Custom/Confirmation Parameters/Reselect")]
public class ReselectParameters : ConfirmationParameters
{
    public override void InvokeConfirmationAction()
    {
        SceneLoadManager.Instance.LoadScene(Scenes.CHARACTER_SELECT);
    }
}
