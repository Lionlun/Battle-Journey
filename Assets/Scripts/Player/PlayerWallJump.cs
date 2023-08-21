using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    public int NumberOfJumps =2;

    Vector3 lastVelocity;
	Vector3 direction;
	float currentSpeed;
    int currentJumps = 0;
	int maxWallJumps = 3;
	Rigidbody rb;

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
			if (currentJumps <= maxWallJumps)
			{
				currentSpeed = lastVelocity.magnitude;
				direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
				Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
				rb.velocity = new Vector3(direction.x, 0, direction.z) * Mathf.Max(currentSpeed, 0);
				rb.velocity += new Vector3(0, 7, 0);
				transform.rotation = rotation;
				currentJumps++;
			}
			else
			{
				currentJumps = 0;
			}
		}
	}
}
