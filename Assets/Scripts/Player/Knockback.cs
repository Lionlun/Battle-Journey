using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Knockback : MonoBehaviour
{
	Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	public void KnockBack(Vector3 objectPosition, float knockbackForce)
	{
		var direction = transform.position - objectPosition;
		rb.AddForce(direction * knockbackForce, ForceMode.Force);
	}
}
