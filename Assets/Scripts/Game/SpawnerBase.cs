using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerBase : MonoBehaviour
{
	[SerializeField] private ProcedualMapGeneration procedualMapGeneration;
	[SerializeField] protected GameObject ObjectPrefab;
	[SerializeField] protected int objectSpawnTime = 10;

	protected Dictionary<Vector2Int, MazeCellObject> Cells { get; set; }
	protected int WorldXCoordinates { get; set; }
	protected int WorldZCoordinates { get; set; }


	private void Awake()
	{
		WorldXCoordinates = procedualMapGeneration.WorldSizeX - 1;
		WorldZCoordinates = procedualMapGeneration.WorldSizeZ - 1;
		Cells = MazeCellsDictionary.Cells;
	}
	protected abstract IEnumerator Spawn();
}
