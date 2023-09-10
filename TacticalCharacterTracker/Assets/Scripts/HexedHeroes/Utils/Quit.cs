using UnityEditor;
using UnityEngine;

public class Quit : MonoBehaviour
{
	public void QuitGame()
	{
		Application.Quit();
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#endif
	}
}
