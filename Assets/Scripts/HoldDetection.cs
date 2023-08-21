using UnityEngine;

public class HoldDetection : MonoBehaviour
{
	public Vector2 fingerCurrentPosition;
	Vector2 fingerDownPosition;
	float distance = 0;
	bool isHolding;

   void Update()
    {
		HandleHold();
	}

	void HandleHold()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == UnityEngine.TouchPhase.Began)
			{
				fingerDownPosition = touch.position;
				fingerCurrentPosition = touch.position;
				isHolding = true;

				InputEvents.SendHold(isHolding);
			}

			distance = Mathf.Abs(fingerDownPosition.magnitude - fingerCurrentPosition.magnitude);
			fingerCurrentPosition = touch.position;

			if (distance > 30)
			{
				isHolding = false;
				InputEvents.SendHold(isHolding);
			}

			if (touch.phase == UnityEngine.TouchPhase.Ended)
			{
				isHolding = false;
				InputEvents.SendHold(isHolding);
			}
		}
	}
}
