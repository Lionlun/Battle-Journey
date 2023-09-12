using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Knockback : MonoBehaviour
{
	private Rigidbody rb;
	private float knockbackCooldown = 0;
	private float knockbackCooldownRefresh = 1;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	private void Update()
	{
		CheckCooldown();
	}
	public void KnockBack(Vector3 objectPosition, float knockbackForce)
	{
		if (knockbackCooldown <= 0)
		{
			var direction = transform.position - objectPosition;
			rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
			knockbackCooldown = knockbackCooldownRefresh;
		}
	}
	
	private void CheckCooldown()
	{
		if (knockbackCooldown >= 0)
		{
			knockbackCooldown -= Time.deltaTime;
		}
	}
}
