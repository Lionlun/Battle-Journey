using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerWallBounce : MonoBehaviour
{
	public int NumberOfJumps { get; set; } = 2;
	public bool CanJump { get; set; }

	private Vector3 lastVelocity;
	private Vector3 direction;
	private float currentSpeed;
	private Rigidbody rb;

	[SerializeField] private TouchDetection touchDetection;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void LateUpdate()
	{
		lastVelocity = rb.velocity;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			CanJump = true;
			currentSpeed = lastVelocity.magnitude;
			direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
			Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
			transform.rotation = rotation;
			rb.velocity = new Vector3(direction.x, 0, direction.z) * Mathf.Max(currentSpeed, 0);
		}
	}
}
