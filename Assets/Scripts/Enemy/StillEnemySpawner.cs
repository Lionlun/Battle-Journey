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
		ActivateWalls(enemyDirection, spawnCoordinates);

		yield return new WaitForSeconds(objectSpawnTime);

		StartCoroutine(Spawn());
	}

	private void ActivateWalls(Vector3 direction, Vector2Int middleArea)  //отдельный класс?
	{

		var vector2IntDirection = new Vector2Int(-(int)direction.x, -(int)direction.z);
		var neighbourCellVector = middleArea + vector2IntDirection;

		if (Cells[middleArea] == null || Cells[neighbourCellVector] == null)
		{
			return;
		}
		var isCell = Cells.TryGetValue(middleArea, out MazeCellObject cell); //?????????
		var isNeighbourCell = Cells.TryGetValue(neighbourCellVector, out MazeCellObject neighbourCellObj);

		if (direction == new Vector3(0, 0, 1) && isNeighbourCell && isCell)
		{
			cell.ActivateWalls(WallSide.Left);
			cell.ActivateWalls(WallSide.Right);
			neighbourCellObj.ActivateWalls(WallSide.Left);
			neighbourCellObj.ActivateWalls(WallSide.Right);
		}
		if (direction == new Vector3(1, 0, 0) && isNeighbourCell && isCell)
		{
			cell.ActivateWalls(WallSide.Top);
			cell.ActivateWalls(WallSide.Bottom);
			neighbourCellObj.ActivateWalls(WallSide.Top);
			neighbourCellObj.ActivateWalls(WallSide.Bottom);
		}
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
