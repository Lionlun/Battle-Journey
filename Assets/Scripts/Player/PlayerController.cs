
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ //TO DO разделить на отдельные классы

	public bool IsStuck;
	[HideInInspector] public Rigidbody Rb;
    [SerializeField] float speed = 15;
 
	[SerializeField] Camera cam;
	[SerializeField] Reticle reticle;
	[SerializeField] TouchDetection touchDetection;
	Animator animator;

	float jumpForce = 10;
	float jumpCooldown = 0;
	float jumpCooldownRefresher = 1f;
	float kickForce = 6;

	bool isHoldingStill;
	bool isHolding;
	public bool IsUnstucking;

	private void OnEnable()
	{
		InputEvents.OnSwipe += GatherSwipeInput;
		InputEvents.OnSwipe += WallUnstuck;
		InputEvents.OnDoubleTap += ActivateAbility;
		InputEvents.OnDoubleTap += StopJump;
		InputEvents.OnHold += AccumulateForce;
		InputEvents.OnTouch += TurnHoldOn;
		InputEvents.OnEndTouch += TurnHoldOff;
	}
	private void OnDisable()
	{
		InputEvents.OnSwipe -= GatherSwipeInput;
		InputEvents.OnSwipe -= WallUnstuck;
		InputEvents.OnDoubleTap -= ActivateAbility;
		InputEvents.OnDoubleTap -= StopJump;
		InputEvents.OnHold -= AccumulateForce;
		InputEvents.OnTouch -= TurnHoldOn;
		InputEvents.OnEndTouch -= TurnHoldOff;

	}
	private void Start()
	{
		animator = GetComponent<Animator>();
		Rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (jumpCooldown > 0)
		{
			jumpCooldown -= Time.deltaTime;
		}
		animator.SetFloat("Speed", Rb.velocity.magnitude);

		if (isHoldingStill)
		{
			Debug.Log("Accumulate FORCE");
		}
	}

	private void FixedUpdate()
	{
		Rotate();
	}

	public void KnockBack(Vector3 objectPosition, float knockbackForce)
	{
		var direction = transform.position - objectPosition;
		Rb.AddForce(direction * knockbackForce, ForceMode.Force);
	}

	public void Stuck(Transform enemyTransform)
	{
		var direction = enemyTransform.position - transform.position;
		transform.position += direction.normalized/2;
		Rb.isKinematic = true;
		IsStuck = true;
	}

	public async void Unstuck()
	{
		Rb.velocity += -transform.forward * 3;
		await Task.Delay(300);
		Rb.isKinematic = false;
		IsStuck = false;
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

	void GatherSwipeInput(SwipeData data)
	{
		var scaledStart = new Vector2(data.StartPosition.x / Screen.width, data.StartPosition.y / Screen.height); //TODO возможно заскейлить distance раньше
		var scaledEnd = new Vector2(data.EndPosition.x / Screen.width, data.EndPosition.y / Screen.height);
		var distance = Vector2.Distance(scaledStart, scaledEnd);

		Move(distance);
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

	async void Move(float distance)
	{
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



	/*
	 	void Jump()
	{
		if (jumpCooldown <= 0)
		{
			Rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
			jumpCooldown = jumpCooldownRefresher;
		}
		
	}
	 */

	void StopJump()
	{
		Rb.AddForce(-transform.up * jumpForce*2, ForceMode.Force);
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
