using UnityEngine;

public class HoldDetection : MonoBehaviour
{
	private Vector2 fingerDownPosition;
	public Vector2 fingerCurrentPosition;
	private float distance = 0;
	bool isHolding;

   void Update()
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
