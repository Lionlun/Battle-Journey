using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
	int currentDeflects = 0;
	int maxDeflects = 3;
	Vector3 lastVelocity;
	Vector3 direction;
	float currentSpeed;
	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
		lastVelocity = rb.velocity;
	}

    public void GetKicked(Vector3 direction, float force)
    {
        Debug.Log("BALL KICKED");
        rb.AddForce(force * direction, ForceMode.Impulse);
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			if (currentDeflects <= maxDeflects)
			{
				currentSpeed = lastVelocity.magnitude;
				direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
				Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
				rb.velocity = new Vector3(direction.x, 0, direction.z) * Mathf.Max(currentSpeed, 0);
				transform.rotation = rotation;
				currentDeflects++;
			}
			else
			{
				currentDeflects = 0;
			}
		}
	}
}
