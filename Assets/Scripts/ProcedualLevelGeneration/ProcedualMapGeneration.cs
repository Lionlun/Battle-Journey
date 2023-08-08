using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ProcedualMapGeneration : MonoBehaviour
{
	public GameObject blockGameObject;
    public Transform wallToSpawn;
    public GameObject outerWall;
    public GameObject hole;

    private int worldSizeX = 10;
    private int worldSizeZ = 10;

    private int CellSize = 3;

    private List<Vector3> blockPositions = new List<Vector3>();
    private List<GameObject> blocks = new List<GameObject>();

    private byte[,] blocksGrid;

	[SerializeField] int holeSpawnPercent = 10;
	[SerializeField] int holeLength = 5;

	MazeRenderer mazeRenderer;

    private int holesNumber = 4;

    void Start()
    {
		mazeRenderer = GetComponent<MazeRenderer>();
		InitiateGrid();	
		SrartGeneration();
	}
    async void SrartGeneration()
    {
		SpawnWall();
	}
    private void SpawnWall()
    {
		var mazeGenerator = new MazeGenerator(worldSizeX, worldSizeZ);
		NewMazeCell[,] maze = mazeGenerator.GetMaze();
		mazeRenderer.RenderWalls(maze, worldSizeX, worldSizeZ, CellSize);
	}

	private Vector3 ObjectSpawnLocation()
    {
        int randomIndex = Random.Range(0, blockPositions.Count);

        Vector3 newPosition = new Vector3(blockPositions[randomIndex].x, blockPositions[randomIndex].y + 2.5f, blockPositions[randomIndex].z);
        blockPositions.RemoveAt(randomIndex);
        return newPosition;
    }
    async void SpawnHoles()
    {
		for (int x = 0; x < worldSizeX; x++)
		{
			for (int z = 0; z < worldSizeZ; z++)
			{
				Vector3 pos = new Vector3(x * CellSize, transform.position.y, z * CellSize);

				if (blocksGrid[x, z] == 2)
				{
					GameObject newHole = Instantiate(hole, pos, Quaternion.identity);
					blockPositions.Add(newHole.transform.position);
					blocks.Add(newHole);
					newHole.transform.SetParent(this.transform);
				}
			}
		}
	}

    async Task SpawnOuterWalls()
    {
		Quaternion spawnRotation90 = Quaternion.Euler(0, 90, 0);
		Quaternion spawnRotationMinus90 = Quaternion.Euler(0, -90, 0);
        Quaternion spawnRotation180 = Quaternion.Euler(0, 180, 0);

		for (int x = 0; x < worldSizeX; x++)
        {
            for (int z = 0; z < worldSizeZ; z++)
            {
				Vector3 pos = new Vector3(x * CellSize, transform.position.y, z * CellSize);

				if (x == 0)
                {
					Instantiate(outerWall, pos, Quaternion.identity);
				}

				if (x == worldSizeX-1)
				{
					Instantiate(outerWall, pos, spawnRotation180);
				}

				if (z == 0)
				{
					Instantiate(outerWall, pos, spawnRotationMinus90 );
				}
				if (z == worldSizeZ-1)
				{
					Instantiate(outerWall, pos, spawnRotation90);
				}
			}
		}
        await Task.Yield();
    }

    void SpawnFloor()
    {
        for (int x = 0; x < worldSizeX; x++)
        {
            for (int z = 0; z < worldSizeZ; z++)
            {
				Vector3 pos = new Vector3(x * CellSize, transform.position.y, z * CellSize);

				if (blocksGrid[x, z] == 1)
                {
					GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity);
					blockPositions.Add(block.transform.position);
					blocks.Add(block);

					block.transform.SetParent(this.transform);
				}
            }
        }
    }

    void InitiateGrid()
    {
		blocksGrid = new byte[worldSizeX, worldSizeZ];

		for (int x = 0; x < worldSizeX; x++)
		{
			for (int z = 0; z < worldSizeZ; z++)
			{
				//set holes
				int random = Random.Range(1, 101); 
				if (x < worldSizeX -1 && x != 0 && z < worldSizeZ -1 && z != 0)
				{
					if (random < holeSpawnPercent)
					{
						for (int i = 0; i < holeLength; i++)
						{
							if (z + i < worldSizeZ)
							{
								blocksGrid[x, z + i] = 2;
							}
						}
					}

					//set floor
					else
					{
						if (blocksGrid[x,z] == 0)
						{
							blocksGrid[x, z] = 1;
						}
					}
					
				}
				//set floor
				else
				{
					blocksGrid[x, z] = 1;
				}

				//set inner walls
				if (blocksGrid[x,z] == 1)
				{
					blocksGrid[x,z] = 3;
				}

			}
		}
	}
}
