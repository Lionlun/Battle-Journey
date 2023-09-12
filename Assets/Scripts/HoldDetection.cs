using UnityEngine;

public class HoldDetection : MonoBehaviour
{
	public Vector2 fingerCurrentPosition { get; set; }
	private Vector2 fingerDownPosition;
	private float distance = 0;
	private bool isHoldingStill;
	private bool isHolding;
   private void Update()
    {
		HandleHold();
	}

	private void HandleHold()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == UnityEngine.TouchPhase.Began)
			{
				fingerDownPosition = touch.position;
				fingerCurrentPosition = touch.position;
				isHoldingStill = true;
				isHolding = true;

				InputEvents.SendHold(isHolding);
				InputEvents.SendHoldStill(isHoldingStill);
			}

			fingerCurrentPosition = touch.position;

			distance = Mathf.Abs(fingerDownPosition.magnitude - fingerCurrentPosition.magnitude);

			if (distance > 30)
			{
				isHoldingStill = false;
				InputEvents.SendHoldStill(isHoldingStill);
			}

			if (touch.phase == UnityEngine.TouchPhase.Ended)
			{
				isHoldingStill = false;
				isHolding = false;
				distance = 0;
				InputEvents.SendHold(isHolding);
				InputEvents.SendHoldStill(isHoldingStill);
			}
		}
	}
}
