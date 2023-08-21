using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	Animator animator;
	bool isAttackPhase;
	int attackDamage = 10;
	float attackCooldown = 0;
	float attackCooldownRefresh = 1f;
	float tapAttackCooldown = 0;
	float tapAttackCooldownRefresh = 1f;
	bool isEnemyInRange;

	private void OnEnable()
	{
		InputEvents.OnTouch += TapAttack;
	}
	private void OnDisable()
	{
		InputEvents.OnTouch -= TapAttack;
	}
	private void Start()
	{
		animator = GetComponent<Animator>();
	}
	private void Update()
	{
		RefreshAttackCooldown();
		RefreshTapAttackCooldown();
	}

	public void ActivateAttackPhase()
	{
		isAttackPhase = true;
	}

	public void DeactivateAttackPhase()
	{
		isAttackPhase = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			isEnemyInRange = true;

			if (attackCooldown <= 0) 
			{
				animator.SetTrigger("Attack");
				attackCooldown = attackCooldownRefresh;
			}
			
			var enemyHealth = other.gameObject.GetComponent<Health>();

			if (isAttackPhase)
			{
				enemyHealth.TakeDamage(attackDamage);	
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			isEnemyInRange = false;
		}
	}

	void TapAttack()
	{
		if (isEnemyInRange && tapAttackCooldown <= 0)
		{
			Debug.Log("Tap Attack");
			animator.SetTrigger("TapAttack");
			tapAttackCooldown = tapAttackCooldownRefresh;
		}
	}

	void RefreshAttackCooldown()
	{
		if (attackCooldown > 0)
		{
			attackCooldown -= Time.deltaTime;
		}
	}
	void RefreshTapAttackCooldown()
	{
		if (tapAttackCooldown > 0)
		{
			tapAttackCooldown -= Time.deltaTime;
		}
	}
}
