using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] Transform target;
	[SerializeField] TouchDetection touchDetection;
	Vector3 cameraInitPosition;
	Vector3 cameraAimOffset;
	Vector3 velocity = Vector3.zero;

	float smoothTime = 0.12f;
	float cameraMaxOffset = 0.05f;
	float cameraOffsetMultiplier = 20;

	bool isHolding;

	void OnEnable()
	{
		InputEvents.OnTouch += SaveInitPosition;
		InputEvents.OnEndTouch += ReturnToInitialPosition;
		InputEvents.OnTouch += HoldOn;
		InputEvents.OnEndTouch += HoldOff;
	}
	void OnDisable()
	{
		InputEvents.OnTouch -= SaveInitPosition;
		InputEvents.OnEndTouch -= ReturnToInitialPosition;
		InputEvents.OnTouch -= HoldOn;
		InputEvents.OnEndTouch -= HoldOff;
	}
	void Update()
    {
        Follow();
		CalculateCameraOffset();
	}

	void Follow()
    {
		if (target != null)
		{
			Vector3 targetPosition = target.position;
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition + cameraAimOffset* cameraOffsetMultiplier, ref velocity, smoothTime);
		}
	}

	void CalculateCameraOffset()
	{
		if (isHolding)
		{
			var aimOffset = transform.position - (transform.position + touchDetection.CurrentDirection.ToIso());
			cameraAimOffset.x = Mathf.Clamp(aimOffset.x, -cameraMaxOffset, cameraMaxOffset);
			cameraAimOffset.z = Mathf.Clamp(aimOffset.z, -cameraMaxOffset, cameraMaxOffset);
		}
	}
	void SaveInitPosition()
	{
		cameraInitPosition = transform.position;
	}
	void ReturnToInitialPosition()
	{
		cameraAimOffset = Vector3.zero;
		transform.position = Vector3.MoveTowards(transform.position, cameraInitPosition, 0.1f);
	}

	void HoldOn()
	{
		isHolding = true;
	}
	void HoldOff()
	{
		isHolding = false;
	}
}
