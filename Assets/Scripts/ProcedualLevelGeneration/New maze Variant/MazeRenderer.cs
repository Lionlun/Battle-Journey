using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
	[SerializeField] GameObject mazeCellPrefab;

	bool isFloorActive;
	bool top;
	bool left;
	bool right;
	bool bottom;
	float CellSize;

	[SerializeField] int absentWallSpawnPercent = 10;


	public void RenderWalls(NewMazeCell[,] maze, int mazeWidth, int mazeHeight, float cellSize)
	{
		CellSize = cellSize;

		for (int x  = 0; x < mazeWidth; x++)
		{
			for (int y = 0; y <  mazeHeight; y++)
			{
				GameObject newCell = Instantiate(mazeCellPrefab, new Vector3((float) x * CellSize, transform.position.y, (float)y * CellSize), Quaternion.identity, transform);
				MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

				top = false;
				left = false;
				right = false;
				bottom = false;

				SpawnOuterWalls(mazeWidth, mazeHeight, x, y);

				mazeCell.Init(top, bottom, left, right, isFloorActive);
				mazeCell.CellPosition = new Vector2Int(x, y);

				MazeCellsDictionary.CreateDictionary(mazeCell.CellPosition, mazeCell); //Добавляет второй раз?
			}
		}
	}

	private void SpawnOuterWalls(int mazeWidth, int mazeHeight, int x, int y)
	{
		if (x == mazeWidth - 1) right = true;
		if (y == 0) bottom = true;
		if (y == mazeHeight - 1) top = true;
		if (x == 0) left = true;
	}
}
