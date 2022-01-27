using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [Scene]
    public string scene;

    private void Start()
    {
        LoadScene(scene);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
