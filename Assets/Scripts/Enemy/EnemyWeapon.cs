using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
	private bool isAttackPhase;
	private int damage = 10;
	[SerializeField] private Transform enemy;
	[SerializeField] float knockBackForce = 10;

	public void SetAttackPhaseTrue()
	{
		isAttackPhase = true;
	}
	public void SetAttackPhaseFalse()
	{
		isAttackPhase = true;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.GetComponent<PlayerController>() != null)
		{
			var player = other.GetComponent<PlayerController>();
			var playerKnockback = other.GetComponent<Knockback>();
			var playerHealth = player.GetComponent<Health>();
			if (isAttackPhase)
			{
				playerHealth.TakeDamage(damage);
				playerKnockback.KnockBack(enemy.position, knockBackForce);
				
				Debug.Log("Damage Taken");
			}
		}
	}
}
