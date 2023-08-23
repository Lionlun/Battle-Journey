
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ //TO DO разделить на отдельные классы

	public bool IsStuck;
	[HideInInspector] public Rigidbody Rb { get; private set; }
    [SerializeField] float speed = 15;
 
	[SerializeField] Camera cam;
	[SerializeField] TouchDetection touchDetection;

	float kickForce = 6;

	bool isHoldingStill;
	bool isHolding;
	public bool IsUnstucking;

	private void OnEnable()
	{
		InputEvents.OnSwipe += Move;
		InputEvents.OnSwipe += WallUnstuck;
		InputEvents.OnDoubleTap += ActivateAbility;
		InputEvents.OnHold += AccumulateForce;
		InputEvents.OnTouch += TurnHoldOn;
		InputEvents.OnEndTouch += TurnHoldOff;
	}
	private void OnDisable()
	{
		InputEvents.OnSwipe -= Move;
		InputEvents.OnSwipe -= WallUnstuck;
		InputEvents.OnDoubleTap -= ActivateAbility;
		InputEvents.OnHold -= AccumulateForce;
		InputEvents.OnTouch -= TurnHoldOn;
		InputEvents.OnEndTouch -= TurnHoldOff;

	}
	private void Start()
	{
		Rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (isHoldingStill)
		{
			Debug.Log("Accumulate FORCE");
		}
	}

	private void FixedUpdate()
	{
		Rotate();
	}

	public async void WallUnstuck(SwipeData data)
	{
		if (IsStuck)
		{
			IsUnstucking = true;
			Rb.isKinematic = false;
			IsStuck = false;
			var scaledStart = new Vector2(data.StartPosition.x / Screen.width, data.StartPosition.y / Screen.height); //TODO возможно заскейлить distance раньше
			var scaledEnd = new Vector2(data.EndPosition.x / Screen.width, data.EndPosition.y / Screen.height);
			var distance = Vector2.Distance(scaledStart, scaledEnd);

			var relative = (transform.position + touchDetection.CurrentDirection.ToIso()) - transform.position;
			Rb.velocity = relative/10;
			var rot = Quaternion.LookRotation(relative, Vector2.up);
			transform.rotation = rot;
			await Task.Delay(200);
			IsUnstucking = false;
		}
	}

	public void FreezePlayer()
	{
		Rb.isKinematic = true;
		IsStuck = true;
	}
	public void UnfreezePlayer() 
	{
		Rb.isKinematic = false;
		IsStuck = false;
	}

	public void MoveBack()
	{
		Rb.velocity += -transform.forward * 3;
	}

	void Rotate()
    {
		if (touchDetection.CurrentDirection != Vector3.zero && isHolding && !IsStuck)
		{
			var relative = (transform.position + touchDetection.CurrentDirection.ToIso()) - transform.position;
			var rot = Quaternion.LookRotation(relative, Vector2.up);
			transform.rotation = rot;
		}
	}

	async void Move (SwipeData data)
	{
		var scaledStart = new Vector2(data.StartPosition.x / Screen.width, data.StartPosition.y / Screen.height); //TODO возможно заскейлить distance раньше
		var scaledEnd = new Vector2(data.EndPosition.x / Screen.width, data.EndPosition.y / Screen.height);
		var distance = Vector2.Distance(scaledStart, scaledEnd);

		if (!IsStuck)
		{
			var smoothedDistance = SmoothDistance(distance);
			Vector3 velocity = transform.forward * smoothedDistance * speed;
			Rb.velocity = velocity;
			await Task.Delay(10);
		}
	}

	float SmoothDistance(float distance)
	{
		distance *= 3;

		if (distance > 3)
		{
			distance = 3;
		}
		if (distance < 0.3f)
		{
			distance = 0.35f;
		}
		return distance;
	}

	void ActivateAbility()
	{
		Debug.Log("Ability Activated");
	}

	void AccumulateForce(bool isHolding)
	{
		this.isHoldingStill = isHolding;
	}

	void TurnHoldOn()
	{
		isHolding = true;
	}
	void TurnHoldOff()
	{
		isHolding = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Ball")
		{
			var ball = other.GetComponent<Ball>();
			var direction = (other.transform.position - transform.position).normalized;
			var force = Rb.velocity.magnitude*kickForce;
			ball.GetKicked(direction, force);
		}
	}
}
