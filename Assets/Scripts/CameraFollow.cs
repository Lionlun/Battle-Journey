using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] TouchDetection touchDetection;
	private Vector3 cameraInitPosition;
	private Vector3 cameraAimOffset;

	private float smoothTime = 0.25f;
    Vector3 velocity = Vector3.zero;
	private bool isHolding;
	private float cameraMaxOffset = 0.15f;
	private float cameraOffsetMultiplier = 20;


	private void OnEnable()
	{
		InputEvents.OnTouch += SaveInitPosition;
		InputEvents.OnEndTouch += ReturnToInitialPosition;
		InputEvents.OnTouch += HoldOn;
		InputEvents.OnEndTouch += HoldOff;
	}
	private void OnDisable()
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
			var aimOffset = ((transform.position + touchDetection.CurrentDirection.ToIso()) - transform.position);
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
