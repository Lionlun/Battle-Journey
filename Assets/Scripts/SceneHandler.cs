using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	private void OnEnable()
	{
		GlobalEvents.OnPlayerDestroyed += StartFirstScene;
	}

	private void OnDisable()
	{
		GlobalEvents.OnPlayerDestroyed -= StartFirstScene;
	}
	public void StartFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
