using UnityEngine;

public class MazeRenderer: MonoBehaviour
{
	[SerializeField] MazeCellObject mazeCellPrefab;
	float CellSize;

	[SerializeField] int absentWallSpawnPercent = 10;


	public void RenderFloor(int mazeWidth, int mazeHeight, float cellSize)
	{
		CellSize = cellSize;

		for (int x  = 0; x < mazeWidth; x++)
		{
			for (int y = 0; y <  mazeHeight; y++)
			{
				MazeCellObject newCell = Instantiate(mazeCellPrefab, new Vector3((float) x * CellSize, transform.position.y, (float)y * CellSize), Quaternion.identity, transform);

				newCell.CellPosition = new Vector2Int(x, y);

				MazeCellsDictionary.AddToDictionary(newCell.CellPosition, newCell); //Добавляет второй раз?
			}
		}
	}
}
