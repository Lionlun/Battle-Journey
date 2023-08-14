using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    private Rigidbody rb;
    public int NumberOfJumps =2;

    private Vector3 lastVelocity;
    private float currentSpeed;
    private Vector3 direction;
    private int currenJumps = 0;

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
			currentSpeed = lastVelocity.magnitude;
			direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
			Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
			transform.rotation = rotation;
			rb.velocity = direction*Mathf.Max(currentSpeed, 0);
			currenJumps++;
		}
	}
}
