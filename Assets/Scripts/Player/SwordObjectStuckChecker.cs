using UnityEngine;

public class SwordObjectStuckChecker : MonoBehaviour
{
	public bool IsWallStuck { get; set; }
	public bool IsEnemyStuck { get; set; }

	private void ObjectStuck<T>(T type)
	{
		if (type is Enemy)
		{
			IsWallStuck = false;
			IsEnemyStuck = true;
		}
		else if (type is Wall)
		{
			IsWallStuck = true;
			IsEnemyStuck = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy>() != null)
		{
			var enemy = other.gameObject.GetComponent<Enemy>();
			ObjectStuck(enemy);
		}

		if (other.gameObject.GetComponent<Wall>() != null)
		{
			var wall = other.gameObject.GetComponent<Wall>();
			ObjectStuck(wall);
		}
	}
}
