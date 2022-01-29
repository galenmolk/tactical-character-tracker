using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
