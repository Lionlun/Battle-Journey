using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using UnityEngine;

public class ProcedualMapGeneration : MonoBehaviour
{
    public GameObject blockGameObject;
    public GameObject wallToSpawn;
    public GameObject outerWall;
    public GameObject hole;

    private int worldSizeX = 10;
    private int worldSizeZ = 10;

    private int gridOffset = 4;

    private List<Vector3> blockPositions = new List<Vector3>();
    private List<GameObject> blocks = new List<GameObject>();

    private byte[,] blocksGrid;


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
        for (int c = 0; c < 20; c++)
        {
            GameObject toPlaceObject = Instantiate (wallToSpawn, ObjectSpawnLocation(), Quaternion.identity);
        }
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
		for (int i = 0; i < holesNumber;  i++)
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
				blocksGrid[x, z] = 1;
			}
		}
	}
}
