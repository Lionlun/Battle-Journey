using System.Threading.Tasks;
using UnityEngine;

public class Sword : MonoBehaviour
{
	[HideInInspector] public bool IsUp;
	[SerializeField] Transform swordAttackPosition;
	[SerializeField] Transform swordUpPosition;

	private int damage = 25;
	private float minimumSpeedToPenetrate = 6;
	private float knockbackCooldown = 0;
	private float knockbackCooldownRefresh = 0.5f;

	[SerializeField] PlayerController player;


	private void OnEnable()
	{
		InputEvents.OnTap += ToggleWeapon;
		IsUp = true;
		transform.localPosition = swordUpPosition.localPosition;
		transform.localRotation = swordUpPosition.localRotation;

	}
	private void OnDisable()
	{
		InputEvents.OnTap -= ToggleWeapon;
	}

	private void Update()
	{
		if (knockbackCooldown > 0)
		{
			knockbackCooldown -= Time.deltaTime;
		}

		if (IsUp)
		{
			transform.localPosition = swordUpPosition.localPosition;
			transform.localRotation = swordUpPosition.localRotation;
		}
		else
		{
			transform.localPosition = swordAttackPosition.localPosition;
			transform.localRotation = swordAttackPosition.localRotation;
		}
	}

	private async void OnTriggerEnter(Collider other)
	{
		if (!IsUp)
		{
			if (other.gameObject.tag == "Enemy")
			{
				if (player.Rb.velocity.magnitude > minimumSpeedToPenetrate)
				{
					var enemyHealth = other.gameObject.GetComponent<Health>();

					await Task.Delay(100);
					
					if (other != null)
					{
						player.Stuck(other.gameObject.transform);
						await Task.Delay(200);
						enemyHealth.TakeDamage(damage);

						player.Unstuck();

					}
				}
			}

			if (other.gameObject.tag == "Wall")
			{
				if (player.Rb.velocity.magnitude > minimumSpeedToPenetrate && !player.IsUnstucking)
				{
					player.Stuck(other.gameObject.transform);
				}
			}

		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (!IsUp)
		{
			if (other.gameObject.tag == "Enemy")
			{
				if (player.Rb.velocity.magnitude < minimumSpeedToPenetrate && knockbackCooldown <=0)
				{
					knockbackCooldown = knockbackCooldownRefresh;
					var enemyHealth = other.gameObject.GetComponent<Health>();
					player.KnockBack(other.transform.position, 250);

				}
			}

			if (other.gameObject.tag == "Wall")
			{
				if (player.Rb.velocity.magnitude < minimumSpeedToPenetrate)
				{
					var enemyHealth = other.gameObject.GetComponent<Health>();
					player.KnockBack(other.transform.position, 25);

				}
			}

		}
	}

	void ToggleWeapon()
	{
		IsUp = !IsUp;
	
	}
}
