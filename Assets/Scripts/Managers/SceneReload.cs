using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneReload
{
    public static void RestartCurrentScene()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
