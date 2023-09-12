using UnityEngine;

public class TouchDetection : MonoBehaviour
{
	public Vector3 CurrentDirection;
	public float Distance { get; set; }

	private Vector3 startPosition;
	private Vector3 currentPosition;

	private void Update()
    {
		if (Input.touchCount > 0)
		{
			GetDirection();
			GetDistance();

			foreach (Touch touch in Input.touches)
			{
				if (touch.phase == UnityEngine.TouchPhase.Began)
				{
					InputEvents.SendTouch();
					startPosition = touch.position;
					InputEvents.SendTouchPosition(startPosition);

				}

				if (touch.phase == UnityEngine.TouchPhase.Ended)
				{
					InputEvents.SendEndTouch();
					Distance = 0;
				}

				currentPosition = touch.position;
			}
		
		}

	}

	private void GetDirection()
	{
		if (Input.touchCount > 0)
		{
			var horizontalDirection = startPosition.x - currentPosition.x;
			var verticalDirection = startPosition.y - currentPosition.y;

			CurrentDirection = new Vector3(horizontalDirection, 0, verticalDirection);
		}
	}

	private void GetDistance()
	{
		Distance = Vector2.Distance(startPosition, currentPosition);
	}
}
