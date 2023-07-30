using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StillEnemyAttack : MonoBehaviour
{
    float attackDamage = 10;
    float attackCooldown = 2;
    float rotationSpeed = 5;
    bool isAttacking;



    void Start()
    {
	}

   
    void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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

    void RotateTowardsTarget(Vector3 targetPosition)
    {
		var directionToTraget = targetPosition - transform.position;
		var vector2Direction = new Vector3(directionToTraget.x, 0, directionToTraget.z);
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, vector2Direction, rotationSpeed * Time.deltaTime, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDirection);

	}
    async void Attack()
    {
		Debug.Log("Attack target");
        await Task.Delay(2000);
		attackCooldown = 2;
		isAttacking = false;
	}
}
