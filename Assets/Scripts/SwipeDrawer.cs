using UnityEngine;

public class SwipeDrawer : MonoBehaviour
{
   private LineRenderer lineRenderer;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
		InputEvents.OnSwipe += OnSwipe;
	}

	private void OnDisable()
	{
		InputEvents.OnSwipe -= OnSwipe;
	}
	void OnSwipe(SwipeData data)
	{
		Vector3[] positions = new Vector3[2];
		positions[0] = Camera.main.ScreenToWorldPoint(new Vector3(data.StartPosition.x, data.StartPosition.y, -10));
		positions[1] = Camera.main.ScreenToWorldPoint(new Vector3(data.EndPosition.x, data.EndPosition.y, -10));
		lineRenderer.positionCount = 2;
		lineRenderer.SetPositions(positions);
	}
}
