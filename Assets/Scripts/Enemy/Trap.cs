using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			var playerRb = other.gameObject.GetComponent<PlayerController>();
			var playerHealth = other.gameObject.GetComponent<Health>();
			var knockBackComponent = other.gameObject.GetComponent<Knockback>();
			knockBackComponent.KnockBack(other.transform.position, 250);
			playerHealth.TakeDamage(20);
		}
	}
}
