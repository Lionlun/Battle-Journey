using System.Collections;
using UnityEngine;

public class TapDetection : MonoBehaviour
{
	private float startTime;
	private float endTime;
	private float doubleTapCheckTime = 0.2f;
	private float singleTapCheckTime = 0.1f;
	int tapCounter = 0;

	Vector3 tapStartPosition;
	Vector3 tapEndPosition;

	private void Update()
	{
		InputCheck();
	}

	private void InputCheck()
	{
		if (Input.touchCount > 0)
		{
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase == UnityEngine.TouchPhase.Began)
				{
					startTime = Time.time;
					tapStartPosition = touch.position;

				}
				if (touch.phase == UnityEngine.TouchPhase.Ended)
				{
					tapEndPosition = touch.position;
					endTime = Time.time;
				}

				if (endTime - startTime < .2f && endTime!= 0)
				{
					var distance = (tapEndPosition - tapStartPosition).magnitude;

					if (distance < 1f)
					{
						InputEvents.SendSingleTap();
					}

					endTime = 0;
					tapCounter++;

				}
				endTime = 0;
			}
		}
	}
}
