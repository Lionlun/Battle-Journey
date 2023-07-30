using UnityEngine;

public class SwipeLogger : MonoBehaviour
{
	private void Awake()
	{
		InputEvents.OnSwipe += OnSwipe;
	}
	private void OnDisable()
	{
		InputEvents.OnSwipe -= OnSwipe;
	}
	private void OnSwipe(SwipeData data)
	{
		Debug.Log("Swipe in Direction: " + data.Direction);
	}
}
