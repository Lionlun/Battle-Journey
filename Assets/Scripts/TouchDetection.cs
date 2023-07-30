using UnityEngine;

public class TouchDetection : MonoBehaviour
{
	public Vector3 CurrentDirection;
	public float Distance;

	Vector3 startPosition;
	Vector3 currentPosition;

	void Update()
    {
		if (Input.touchCount > 0)
		{
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
				}

				currentPosition = touch.position;
			}
		}

		GetDirection();
		GetDistnace();
	}

	void GetDirection()
	{
		if (Input.touchCount > 0)
		{
			var horizontalDirection = startPosition.x - currentPosition.x;
			var verticalDirection = startPosition.y - currentPosition.y;

			CurrentDirection = new Vector3(horizontalDirection, 0, verticalDirection);
		}
	}

	void GetDistnace()
	{
		Distance = Vector2.Distance(startPosition, currentPosition);
	}
}
