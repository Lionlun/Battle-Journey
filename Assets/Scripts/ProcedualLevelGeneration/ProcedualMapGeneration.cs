using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ProcedualMapGeneration : MonoBehaviour
{
	[SerializeField] MazeCreator mazeCreator;



	public GameObject blockGameObject;
    public Transform wallToSpawn;
    public GameObject outerWall;
    public GameObject hole;

    private int worldSizeX = 10;
    private int worldSizeZ = 10;

    private int gridOffset = 4;

    private List<Vector3> blockPositions = new List<Vector3>();
    private List<GameObject> blocks = new List<GameObject>();

    private byte[,] blocksGrid;

	[SerializeField] int holeSpawnPercent = 10;
	[SerializeField] int holeLength = 5;


    private int holesNumber = 4;

    void Start()
    {
		

		InitiateGrid();
	
		SrartGeneration();

	}
    async void SrartGeneration()
    {
		SpawnFloor();
		await SpawnOuterWalls();
		SpawnHoles();
		SpawnWall();
	}
    private void SpawnWall()
    {
		var maze = MazeGenerator.Generate(worldSizeX, worldSizeZ);

		for (int x = 0; x < worldSizeX; x++)
		{
			for (int z = 0; z < worldSizeZ; z++)
			{
				Vector3 pos = new Vector3(x * gridOffset, transform.position.y, z * gridOffset);


				if (blocksGrid[x, z] == 3)
				{

					mazeCreator.Create(maze[x,z], pos, wallToSpawn);

					GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity);
					blockPositions.Add(block.transform.position);
					blocks.Add(block);

					block.transform.SetParent(this.transform);
				}
			}
		}





		/*for (int x = 0; x < worldSizeX; x++)
		{
			for (int z = 0; z < worldSizeZ; z++)
			{
				Vector3 pos = new Vector3(x * gridOffset, transform.position.y, z * gridOffset);

				if (blocksGrid[x, z] == 3)
				{
					GameObject wall = Instantiate(wallToSpawn, pos, Quaternion.identity);

					GameObject block = Instantiate(blockGameObject, pos, Quaternion.identity);
					blockPositions.Add(block.transform.position);
					blocks.Add(block);

					block.transform.SetParent(this.transform);
				}
			}
		}*/
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
				Vector3 pos = new Vector3(x * gridOffset, transform.position.y, z * gridOffset);

				if (blocksGrid[x, z] == 2)
				{
					GameObject newHole = Instantiate(hole, pos, Quaternion.identity);
					blockPositions.Add(newHole.transform.position);
					blocks.Add(newHole);
					newHole.transform.SetParent(this.transform);
				}
			}
		}

		/*for (int i = 0; i < holesNumber;  i++)
        {
			int randomIndex = Random.Range(0, blocks.Count);

            while (randomIndex == worldSizeX || randomIndex == worldSizeZ)
            {
                randomIndex = Random.Range(0, blocks.Count);
                await Task.Delay(20);
			}
          
			Instantiate(hole, blocks[randomIndex].transform.position, Quaternion.identity);
			
			if (blocks[randomIndex+1] != null) //out of range
            {
				Instantiate(hole, blocks[randomIndex + 1].transform.position, Quaternion.identity);
				blockPositions.RemoveAt(randomIndex + 1);
				Destroy(blocks[randomIndex + 1].gameObject);
				blocks.RemoveAt(randomIndex + 1);
			}

			blockPositions.RemoveAt(randomIndex);
			Destroy(blocks[randomIndex].gameObject);
			blocks.RemoveAt(randomIndex);
		}*/

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
				Vector3 pos = new Vector3(x * gridOffset, transform.position.y, z * gridOffset);

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
				Vector3 pos = new Vector3(x * gridOffset, transform.position.y, z * gridOffset);

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
