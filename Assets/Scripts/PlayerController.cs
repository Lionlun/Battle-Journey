
using UnityEngine;

public class PlayerController : MonoBehaviour
{ //TO DO ��������� �� ��������� ������
	public Rigidbody Rb;
    float speed = 500;
 
	[SerializeField]Camera cam;
	[SerializeField] Reticle reticle;
	[SerializeField] TouchDetection touchDetection;
	Animator animator;

	float knockBackForce = 250;
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
		Rb.AddForce(direction * knockBackForce * Time.deltaTime, ForceMode.Force);
	}

	void GatherSwipeInput(SwipeData data)
	{
		var horizontalDirection = data.StartPosition.x - data.EndPosition.x;
		var verticalDirection = data.StartPosition.y - data.EndPosition.y;


		var scaledStart = new Vector2(data.StartPosition.x/Screen.width, data.StartPosition.y/Screen.height);
		var scaledEnd = new Vector2(data.EndPosition.x / Screen.width, data.EndPosition.y / Screen.height);
		var distance = Vector2.Distance(scaledStart, scaledEnd);

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
		distance *= 3;
		Debug.Log(distance);
		if (distance > 300)
		{
			distance = 300;
		}
		Rb.AddForce(transform.forward*distance*speed, ForceMode.Force); //distance deleted
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
		Rb.AddForce(-transform.up * jumpForce*2, ForceMode.Force);
	}
}
