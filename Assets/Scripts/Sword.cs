using System.Threading.Tasks;
using UnityEngine;

public class Sword : MonoBehaviour
{
	private int damage = 25;
	private float minimumSpeedToPenetrate = 6;

	[SerializeField] PlayerController player;

	private async void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			Debug.Log(player.Rb.velocity.magnitude);
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
	}
}
