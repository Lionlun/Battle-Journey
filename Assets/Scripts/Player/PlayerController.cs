
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{ //TO DO разделить на отдельные классы
	public bool IsUnstucking { get; set; }
	[HideInInspector] public Rigidbody Rb { get; private set; }

    [SerializeField] private float speed = 15;
 
	[SerializeField] private TouchDetection touchDetection;
	[SerializeField] Sword sword;

	private bool isHolding;

	private void OnEnable()
	{
		InputEvents.OnTouch += TurnHoldOn;
		InputEvents.OnEndTouch += TurnHoldOff;
	
	}
	private void OnDisable()
	{
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
		Move();
	}

	public void MoveBack()
	{
		Rb.velocity += -transform.forward * 3;
	}

	public void Stop()
	{
		Rb.velocity = Vector3.zero;
	}

	private void Rotate()
    {
		if (touchDetection.CurrentDirection != Vector3.zero && isHolding)
		{
			var relative = transform.position - (transform.position + touchDetection.CurrentDirection.ToIso());
			var rot = Quaternion.LookRotation(relative, Vector2.up);
			transform.rotation = rot;
		}
	}

	private void Move()
	{
		if (isHolding)
		{
			var distance = touchDetection.Distance;
			var smoothedDistance = SmoothDistance(distance);
			if (distance > 20f)
			{
				Vector3 velocity = transform.forward * smoothedDistance * speed;
				Rb.velocity = velocity;
			}
			else
			{
				Rb.velocity = Vector3.zero; //Может конфликтовать с Attack
			}
		}
	}

	private float SmoothDistance(float distance)
	{
		if (distance > 100)
		{
			distance = 100;
		}
		distance /= 90;
		//var smoothedDistance = Mathf.Clamp(distance, 0.6f, 1.5ff);
		return distance;
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
