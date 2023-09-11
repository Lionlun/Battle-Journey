using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneReload
{
	public static Action OnSceneReload;

	public static void RestartCurrentScene()
    {
		OnSceneReload?.Invoke();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
