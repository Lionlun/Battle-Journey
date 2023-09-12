using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ProcedualMapGeneration : MonoBehaviour
{
    public Transform wallToSpawn;
    public GameObject hole;

    public int WorldSizeX { get; set; } = 5;
    public int WorldSizeZ { get; set; } = 5;
    public float CellSize { get; set; } = 3f;

	[SerializeField] int holeSpawnPercent;
	[SerializeField] int holeLength = 5;

	MazeRenderer mazeRenderer;

	NewMazeCell[,] maze;

	void Awake()
    {
		mazeRenderer = GetComponent<MazeRenderer>();
		SrartGeneration();
	}
	void SrartGeneration()
    {
		CreateMaze();
		ActivateFloor();
	}
    private void CreateMaze()
    {
		var mazeGenerator = new MazeGenerator(WorldSizeX, WorldSizeZ);
		maze = mazeGenerator.GetMaze();
		mazeRenderer.RenderWalls(maze, WorldSizeX, WorldSizeZ, CellSize);
	}

	private void ActivateFloor()
	{
		for (int x = 0; x < WorldSizeX; x++)
		{
			for (int y = 0; y < WorldSizeZ; y++)
			{
				MazeCellsDictionary.Cells[new Vector2Int(x, y)].ActivateFloor();
			}
		}
	}

	private void SpawnHoles()
	{
		for (int x = 0; x < WorldSizeX; x++)
		{
			for (int y = 0; y < WorldSizeZ; y++)
			{
				var random = Random.Range(0, 101);

				if (random <= holeSpawnPercent)
				{
					if (maze[x, y].X + 1 < WorldSizeX)
					{
						if (maze[x, y].IsLeftWall == true && maze[x + 1, y].IsLeftWall == true)
						{
							MazeCellsDictionary.Cells[new Vector2Int(x, y)].DeactivateFloor();
							Instantiate(hole, MazeCellsDictionary.Cells[new Vector2Int(x, y)].transform.position, Quaternion.identity);
						}
					}

					if (maze[x, y].Y - 1 >= 0)
					{
						if (maze[x, y].IsTopWall == true && maze[x, y - 1].IsTopWall == true)
						{
							MazeCellsDictionary.Cells[new Vector2Int(x, y)].DeactivateFloor();
							Instantiate(hole, MazeCellsDictionary.Cells[new Vector2Int(x, y)].transform.position, Quaternion.identity);
						}
					}
				}
			}
		}
			
	}
}
