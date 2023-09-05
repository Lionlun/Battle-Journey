using UnityEngine;

public class GameTimer: MonoBehaviour
{
    [SerializeField] private float gameTime = 60;

	private void Update()
	{
		CountDown();
	}

	private void CountDown()
	{
		if (gameTime > 0)
		{
			gameTime -= Time.deltaTime;
		}

		else
		{
			SceneReload.RestartCurrentScene();
		}
	}
}
