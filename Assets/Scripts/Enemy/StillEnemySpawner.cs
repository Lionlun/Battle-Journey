using System.Collections;
using UnityEngine;

public class StillEnemySpawner : SpawnerBase
{
	private Vector2Int spawnCoordinates;
	private Vector3 spawnPosition;

	private void Start()
	{
		StartCoroutine(Spawn());
	}

	protected override IEnumerator Spawn()
	{
		spawnCoordinates = GetRandomMiddleAreaCoordinate();
		spawnPosition = spawnCoordinates.ConvertToWorldPosition();
		yield return new WaitForSeconds(objectSpawnTime);

		var enemy = Instantiate(ObjectPrefab, spawnPosition, Quaternion.identity);
		var enemyDirection = GetStillEnemySpawnDirection(enemy);

		yield return new WaitForSeconds(objectSpawnTime);

		StartCoroutine(Spawn());
	}

	private Vector3 GetStillEnemySpawnDirection(GameObject enemy)
	{
		var direction = enemy.transform.forward;
		Debug.Log(direction);
		return direction;
	}

	private Vector2Int GetRandomMiddleAreaCoordinate()
	{
		var randomX = Random.Range(1, WorldXCoordinates);
		var randomZ = Random.Range(1, WorldZCoordinates);

		return new Vector2Int(randomX, randomZ);
	}
}
