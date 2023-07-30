
using UnityEngine;

public class PlayerController : MonoBehaviour
{ //TO DO разделить на отдельные классы
	public Rigidbody Rb;
    float speed = 5;
 
	[SerializeField]Camera cam;
	[SerializeField] Reticle reticle;
	[SerializeField] TouchDetection touchDetection;
	Animator animator;

	float knockBackForce = 8;
	float jumpForce = 10;
	float jumpCooldown = 0;
	float jumpCooldownRefresher = 1f;

	bool isHolding;

	private void OnEnable()
	{
		InputEvents.OnSwipe += GatherSwipeInput;
		InputEvents.OnDoubleTap += ActivateAbility;
		InputEvents.OnDoubleTap += StopJump;
		InputEvents.OnTap += Jump;
		InputEvents.OnHold += AccumulateForce;
	}
	private void OnDisable()
	{
		InputEvents.OnSwipe -= GatherSwipeInput;
		InputEvents.OnDoubleTap -= ActivateAbility;
		InputEvents.OnDoubleTap -= StopJump;
		InputEvents.OnTap -= Jump;
		InputEvents.OnHold -= AccumulateForce;

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

		if (isHolding)
		{
			Debug.Log("Accumulate FORCE");
		}
	}

	private void FixedUpdate()
	{
		Rotate();
	}

	public void Attack()
	{
		animator.SetTrigger("Attack");
	}

	public void KnockBack(Vector3 objectPosition)
	{
		var direction = transform.position - objectPosition;
		Rb.AddForce(direction * knockBackForce, ForceMode.Impulse);
	}

	void GatherSwipeInput(SwipeData data)
	{
		var horizontalDirection = data.StartPosition.x - data.EndPosition.x;
		var verticalDirection = data.StartPosition.y - data.EndPosition.y;
	
		var distance = Vector2.Distance(data.StartPosition, data.EndPosition);

		Move(distance);
	}

	void Rotate()
    {
		if (touchDetection.CurrentDirection != Vector3.zero)
		{
			var relative = (transform.position + touchDetection.CurrentDirection.ToIso()) - transform.position;
			var rot = Quaternion.LookRotation(relative, Vector2.up);
			transform.rotation = rot;
		}
	}

    void Move(float distance)
    {
		
		Rb.AddForce(transform.forward*speed*distance/1.5f);
	}

	void ActivateAbility()
	{
		Debug.Log("Ability Activated");
	}

	void AccumulateForce(bool isHolding)
	{
		this.isHolding = isHolding;
		Debug.Log("Acumulating force");
	}

	void Jump()
	{
		if (jumpCooldown <= 0)
		{
			Rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
			Debug.Log("Jump");
			jumpCooldown = jumpCooldownRefresher;
		}
		
	}

	void StopJump()
	{
		Rb.AddForce(-transform.up * jumpForce*2, ForceMode.Impulse);
	}
}
