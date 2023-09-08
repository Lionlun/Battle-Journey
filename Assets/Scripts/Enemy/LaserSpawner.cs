using System.Collections;
using UnityEngine;

public class LaserSpawner : SpawnerBase
{
	private void Start()
	{
		StartCoroutine(Spawn());
	}

	protected override IEnumerator Spawn()
	{
		yield return new WaitForSeconds(objectSpawnTime);
		var randomCornerCoordinates = PositionCalculator.GetRandomCorner(WorldXCoordinates, WorldZCoordinates);
		var spawnPosition = randomCornerCoordinates.ConvertToWorldPosition();
		Instantiate(ObjectPrefab, spawnPosition, Quaternion.identity);
	}
}
