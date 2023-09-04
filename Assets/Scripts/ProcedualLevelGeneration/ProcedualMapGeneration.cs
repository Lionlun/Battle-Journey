using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ProcedualMapGeneration : MonoBehaviour
{
    public Transform wallToSpawn;
    public GameObject hole;

    private int worldSizeX = 5;
    private int worldSizeZ = 5;

    private float CellSize = 3f;

	[SerializeField] int holeSpawnPercent;
	[SerializeField] int holeLength = 5;

	MazeRenderer mazeRenderer;

	NewMazeCell[,] maze;

	void Start()
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
		var mazeGenerator = new MazeGenerator(worldSizeX, worldSizeZ);
		maze = mazeGenerator.GetMaze();
		mazeRenderer.RenderWalls(maze, worldSizeX, worldSizeZ, CellSize);
	}

	private void ActivateFloor()
	{
		for (int x = 0; x < worldSizeX; x++)
		{
			for (int y = 0; y < worldSizeZ; y++)
			{
				mazeRenderer.Cells[new Vector2Int(x, y)].ActivateFloor();
			}
		}
	}

	private void SpawnHoles()
	{
		for (int x = 0; x < worldSizeX; x++)
		{
			for (int y = 0; y < worldSizeZ; y++)
			{
				var random = Random.Range(0, 101);

				if (random <= holeSpawnPercent)
				{
					if (maze[x, y].X + 1 < worldSizeX)
					{
						if (maze[x, y].IsLeftWall == true && maze[x + 1, y].IsLeftWall == true)
						{
							mazeRenderer.Cells[new Vector2Int(x, y)].DeactivateFloor();
							Instantiate(hole, mazeRenderer.Cells[new Vector2Int(x, y)].transform.position, Quaternion.identity);
						}
					}

					if (maze[x, y].Y - 1 >= 0)
					{
						if (maze[x, y].IsTopWall == true && maze[x, y - 1].IsTopWall == true)
						{
							mazeRenderer.Cells[new Vector2Int(x, y)].DeactivateFloor();
							Instantiate(hole, mazeRenderer.Cells[new Vector2Int(x, y)].transform.position, Quaternion.identity);
						}
					}
				}
			}
		}
			
	}
}
