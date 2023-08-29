
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ //TO DO разделить на отдельные классы
	public bool IsUnstucking { get; set; }
	[HideInInspector] public Rigidbody Rb { get; private set; }

    [SerializeField] private float speed = 15;
 
	[SerializeField] private TouchDetection touchDetection;
	[SerializeField] Sword sword;

	private bool isHolding;
	
	private bool isWallJumping;

	private void OnEnable()
	{
		InputEvents.OnSwipe += Move;
		InputEvents.OnTouch += TurnHoldOn;
		InputEvents.OnEndTouch += TurnHoldOff;
	}
	private void OnDisable()
	{
		InputEvents.OnSwipe -= Move;
		InputEvents.OnTouch -= TurnHoldOn;
		InputEvents.OnEndTouch -= TurnHoldOff;

	}
	private void Start()
	{
		Rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Rotate();
	}

	public void FreezePlayer()
	{
		Rb.isKinematic = true;
	}
	public void UnfreezePlayer() 
	{
		Rb.isKinematic = false;
	}

	public void MoveBack()
	{
		Rb.velocity += -transform.forward * 3;
	}

	private void Rotate()
    {
		if (touchDetection.CurrentDirection != Vector3.zero && isHolding && !sword.IsStuck)
		{
			var relative = (transform.position + touchDetection.CurrentDirection.ToIso()) - transform.position;
			var rot = Quaternion.LookRotation(relative, Vector2.up);
			transform.rotation = rot;
		}
	}

	private async void Move (SwipeData data)
	{
		var scaledStart = new Vector2(data.StartPosition.x / Screen.width, data.StartPosition.y / Screen.height); //TODO возможно заскейлить distance раньше
		var scaledEnd = new Vector2(data.EndPosition.x / Screen.width, data.EndPosition.y / Screen.height);
		var distance = Vector2.Distance(scaledStart, scaledEnd);

		if (!sword.IsStuck && !isWallJumping)
		{
			var smoothedDistance = SmoothDistance(distance);
			Vector3 velocity = transform.forward * smoothedDistance * speed;
			Rb.velocity = velocity;
			await Task.Delay(10);
		}
	}

	private float SmoothDistance(float distance)
	{
		distance *= 4;
		var smoothedDistance = Mathf.Clamp(distance, 0.4f, 4f);
		return smoothedDistance;
	}

	private void TurnHoldOn()
	{
		isHolding = true;
	}
	private void TurnHoldOff()
	{
		isHolding = false;
	}
}
