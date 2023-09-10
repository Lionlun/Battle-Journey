using UnityEngine;

public class InvisibleBarrier : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<PlayerController>() != null) 
		{
			var player = collision.gameObject.GetComponent<PlayerController>();
			player.MoveBack();
		}
	}
}
