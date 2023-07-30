using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
	private Vector2 fingerDownPosition;
	private Vector2 fingerUpPosition;
	public Vector2 fingerCurrentPosition;
	[SerializeField] private float minDistanceForSwipe = 20f;

	private void Update()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == UnityEngine.TouchPhase.Began)
			{
				fingerUpPosition = touch.position;
				fingerDownPosition = touch.position;
				fingerCurrentPosition = touch.position;
			}

			fingerCurrentPosition = touch.position;
			

			if(touch.phase == UnityEngine.TouchPhase.Ended)
			{
				fingerDownPosition = touch.position;
				DetectSwipe();
			}
		}
	}

	private void DetectSwipe()
	{
		if (SwipeDistanceCheckMet())
		{
			if (IsVerticalSwipe())
			{
				var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
				SendSwipe(direction);
			}
			else
			{
				var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
				SendSwipe(direction);
			}
		}
	}
	private bool IsVerticalSwipe()
	{
		return VerticalMovementDistance() > HorizontalMovementDistance();
	}
	private bool SwipeDistanceCheckMet()
	{
		return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
	}

	private float VerticalMovementDistance()
	{
		return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
	}
	private float HorizontalMovementDistance() 
	{
		return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
	}

	private void SendSwipe(SwipeDirection direction)
	{
		SwipeData swipeData = new SwipeData()
		{
			Direction = direction,
			StartPosition = fingerDownPosition,
			EndPosition = fingerUpPosition,
		};

		InputEvents.SendSwipe(swipeData); //OnSwipe.Invoke(swipeData);
	}
}

public struct SwipeData
{
	public Vector2 StartPosition;
	public Vector2 EndPosition;
	public SwipeDirection Direction;
}

public enum SwipeDirection
{
	Up, 
	Down, 
	Left, 
	Right
}
