using System.Threading.Tasks;
using UnityEngine;

public class Sword : MonoBehaviour
{
	public bool IsStuck { get; set; }

	[SerializeField] private PlayerController player;

	[SerializeField] private PlayerSwordGrab playerGrab;
	[SerializeField] private PlayerAttack playerAttack;

	private int damage = 25;
	private float minimumSpeedToPenetrate = 6;
	private float knockbackCooldown = 0;
	private float knockbackCooldownRefresh = 0.5f;

	[SerializeField] Transform swordDownPosition;
	[SerializeField] Hinge hinge;

	Vector3 positionAtCollision = Vector3.zero;
	
	private void Update()
	{
		if (IsStuck) //не в апдейте
		{
			transform.position = positionAtCollision;
		}
		else
		{
			transform.position = swordDownPosition.position; //weapon point который изменяется не в апдейте
			transform.rotation = swordDownPosition.rotation;
		}
	}

	public void Unstuck()
	{
		hinge.Deactivate();
		IsStuck = false;
	}

	private void ObjectStuck()
	{
		if (!player.IsUnstucking)
		{
			positionAtCollision = transform.position; //в процессе
			IsStuck = true;
			hinge.Deactivate();
			playerGrab.ReleaseSword();
		}
	}

	private async void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy>() != null)
		{
			var enemy = other.gameObject.GetComponent<Enemy>();

			if (playerAttack.IsAttacking)
			{
				var enemyHealth = other.gameObject.GetComponent<Health>();

				await Task.Delay(100);
					
				if (other != null)
				{
					ObjectStuck();
				
					enemyHealth.TakeDamage(damage);
				}
			}
		}

		if (other.gameObject.GetComponent<Wall>() != null)
		{
			var wall = other.gameObject.GetComponent<Wall>();

			if (playerAttack.IsAttacking && !player.IsUnstucking)
			{
				ObjectStuck();
				player.Stop();
			}
		}
	}


	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy>() != null)
		{
			if (player.Rb.velocity.magnitude < minimumSpeedToPenetrate && knockbackCooldown <=0 && !IsStuck)
			{
				knockbackCooldown = knockbackCooldownRefresh;
				var knockBackComponent = player.gameObject.GetComponent<Knockback>();
				knockBackComponent.KnockBack(other.transform.position, 250);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.GetComponent<Wall>() != null)
		{
			IsStuck = false;
		}
	}
}
