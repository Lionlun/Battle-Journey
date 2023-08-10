using UnityEngine;

public class HealthUI : MonoBehaviour
{
	private float timeToDeactivate;
	private float timeToDeactivateRefresh = 1f;

	private void OnEnable()
	{
		timeToDeactivate = timeToDeactivateRefresh;
	}

	private void Update()
	{
		if (timeToDeactivate > 0)
		{
			timeToDeactivate -= Time.deltaTime;
		}
		else
		{
			timeToDeactivate = timeToDeactivateRefresh;
			this.gameObject.SetActive(false);
		}
	}
}
