using System.Threading.Tasks;
using UnityEngine;

public class Sword : MonoBehaviour
{
	[HideInInspector] public bool IsUp;
	[SerializeField] Transform swordAttackPosition;
	[SerializeField] Transform swordUpPosition;

	private int damage = 25;
	private float minimumSpeedToPenetrate = 6;

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
					Debug.Log("Stuck");
					if (other != null)
					{
						player.Stuck(other.gameObject.transform);
						await Task.Delay(200);
						enemyHealth.TakeDamage(damage);

						player.Unstuck();

					}
				}

				else
				{
					player.KnockBack(other.transform.position);
				}
			}

			if (other.gameObject.tag == "Wall")
			{
				player.Stuck(other.gameObject.transform);
				await Task.Delay(200);
				player.Unstuck();
			}

		}
	}

	void ToggleWeapon()
	{
		IsUp = !IsUp;
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
}
