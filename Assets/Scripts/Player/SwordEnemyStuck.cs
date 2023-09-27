using UnityEngine;

public class SwordEnemyStuck : MonoBehaviour
{
	public bool IsInEnemy { get; set; }
	public bool IsUnstucking { get; set; }
	[SerializeField] private PlayerAttackDash playerAttack;
	[SerializeField] private PlayerController player;
	private int damage = 40;

	private void OnEnable()
	{
		InputEvents.OnEndTouch += FinishUnstucking;
	}

	private void OnDisable()
	{
		InputEvents.OnEndTouch -= FinishUnstucking;
	}

	private void EnemyStuck()
	{
		IsInEnemy = true;
		player.Stop();
	}

	public void Unstuck()
	{
		IsInEnemy = false;
		IsUnstucking = true;
		player.MoveBack();
	}

	private void FinishUnstucking()
	{
		IsUnstucking = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy>() != null)
		{
			if (playerAttack.IsAttacking)
			{
				var enemyHealth = other.gameObject.GetComponent<Health>();

				if (enemyHealth != null)
				{
					if(enemyHealth.CurrentHealth < damage)
					{
						enemyHealth.TakeDamage(damage);
					}
					else
					{
						enemyHealth.TakeDamage(damage);
						EnemyStuck();
					}
					
					
				}
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (IsUnstucking)
		{
			if (other.gameObject.GetComponent<Enemy>() != null)
			{
				var enemy = other.gameObject.GetComponent<Enemy>();
				enemy.GetPushed(player.transform.forward);
			}
		}
	}
}
