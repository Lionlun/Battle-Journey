using System.Collections.Generic;
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

	public Dictionary<Vector2Int,MazeCellObject> Cells = new Dictionary<Vector2Int, MazeCellObject>();

	public void RenderWalls(NewMazeCell[,] maze, int mazeWidth, int mazeHeight, float cellSize)
	{;
		CellSize = cellSize;
	
		for (int x  = 0; x < mazeWidth; x++)
		{
			for (int y = 0; y <  mazeHeight; y++)
			{
				GameObject newCell = Instantiate(mazeCellPrefab, new Vector3((float) x * CellSize, transform.position.y, (float)y* CellSize), Quaternion.identity, transform);
				MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

				top = maze[x, y].TopWall;
				left = maze[x, y].LeftWall;
				right = false;
				bottom = false;

				SpawnNoWall(maze, x, y);

				if (x == mazeWidth - 1) right = true;
				if (y == 0) bottom = true;
				if (y == mazeHeight - 1) top = true;
				if (x == 0) left = true;

				
				mazeCell.Init(top, bottom, left, right, isFloorActive);
				mazeCell.CellPosition = new Vector2Int(x, y);
				Cells.Add(mazeCell.CellPosition, mazeCell);
			}
		}
	}

	private void SpawnNoWall(NewMazeCell[,] maze, int x, int y)
	{
		var random = Random.Range(0, 101);

		if (random < absentWallSpawnPercent)
		{
			maze[x,y].TopWall = false;
			maze[x,y].LeftWall = false;
			top = false;
			left = false;
			bottom = false;
			right = false;
		}
		else
		{
			return;
		}
	}
}
