
using UnityEngine;

public class StillEnemyButton : MonoBehaviour
{
	public bool IsPressed { get; set; }
	public float Timer { get; set; } = 10;

	private void Update()
	{
		Timer-=Time.deltaTime;
		CheckTimer();
	}

	private void CheckTimer()
	{
		if (Timer <= 0)
		{
			SceneReload.RestartCurrentScene();
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<PlayerController>() != null)
		{
			IsPressed = true;
			Debug.Log(IsPressed);
		}
	}
}
