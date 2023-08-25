using UnityEngine;

public class PlayerKick : MonoBehaviour
{
	private Rigidbody rb;
	private float kickForce = 6;

	private void Start()
    {
		rb = GetComponent<Rigidbody>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Ball")
		{
			var ball = other.GetComponent<Ball>();
			var direction = (other.transform.position - transform.position).normalized;
			var force = rb.velocity.magnitude * kickForce;
			ball.GetKicked(direction, force);
		}
	}
}
