
using System.Threading.Tasks;
using UnityEngine;

public class StillEnemyAttack : MonoBehaviour
{
	[SerializeField] EnemyWeapon weapon;
    private float attackCooldown = 2;
    private float rotationSpeed = 5;
    private bool isAttacking;
    private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}
	private void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            if (attackCooldown < 0)
            {
                isAttacking = true;
				Attack();
			}
		
	        if(!isAttacking)
            {
				RotateTowardsTarget(other.transform.position);
			}	
        }
	}
	public void FinishAttacking()
	{
		isAttacking = false;
	}
	public void SetAttackPhaseTrue()
	{
		weapon.SetAttackPhaseTrue();
	}
	public void SetAttackPhaseFalse()
	{
		weapon.SetAttackPhaseFalse();
	}
	private void RotateTowardsTarget(Vector3 targetPosition)
    {
		var directionToTraget = targetPosition - transform.position;
		var vector2Direction = new Vector3(directionToTraget.x, 0, directionToTraget.z);
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, vector2Direction, rotationSpeed * Time.deltaTime, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDirection);

	}
    private void Attack()
    {
		Debug.Log("Attack target");
        animator.SetTrigger("Attack");
		attackCooldown = 2;
	}
}
