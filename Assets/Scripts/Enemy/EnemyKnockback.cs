using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
	Rigidbody rb;
	[SerializeField] float knockbackForce = 2;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	void Knockback(Vector3 direction, float playerVeloctiy)
	{
		var vector2Direction = new Vector3 (direction.x, 0, direction.z);
		rb.AddForce(-vector2Direction * playerVeloctiy* knockbackForce, ForceMode.Impulse);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			var playerRb = collision.gameObject.GetComponent<PlayerController>();
			var direction = playerRb.transform.position - transform.position;
			Knockback(direction, playerRb.Rb.velocity.magnitude);
		}
	}

	/*private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			var playerRb = other.gameObject.GetComponent<PlayerController>();
			var playerHealth = other.gameObject.GetComponent<Health>();
			playerRb.KnockBack(transform.position);
			playerHealth.TakeDamage(20);
		}
	}*/
}
