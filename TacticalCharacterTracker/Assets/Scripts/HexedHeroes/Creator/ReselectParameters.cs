using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Reselect", menuName = "Custom/Confirmation Parameters/Reselect")]
public class ReselectParameters : ConfirmationParameters
{
    public override void InvokeConfirmationAction()
    {
        SceneManager.LoadScene(SceneKeys.CHARACTER_SELECT);
    }
}
