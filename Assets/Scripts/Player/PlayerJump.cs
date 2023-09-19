using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
	private float jumpForce = 10f;
	private Rigidbody rb;

	private void OnEnable()
	{
		InputEvents.OnTap += Jump;
	}
	private void OnDisable()
	{
		InputEvents.OnTap -= Jump;
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, 0.55f);
	}

	private void Jump()
	{
		if (IsGrounded())
		{
			rb.velocity += new Vector3(0, jumpForce, 0);
		}
	}
}
