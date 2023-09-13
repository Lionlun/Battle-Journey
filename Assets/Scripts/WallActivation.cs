using System.Collections.Generic;
using UnityEngine;

public class WallActivation : MonoBehaviour
{
	[SerializeField] private ProcedualMapGeneration procedualMapGeneration;
	private Dictionary<Vector2Int, MazeCellObject> Cells { get; set; }
	public int WorldXCoordinates { get; set; }
	public int WorldZCoordinates { get; set; }

	private void Start()
	{
		WorldXCoordinates = procedualMapGeneration.WorldSizeX - 1;
		WorldZCoordinates = procedualMapGeneration.WorldSizeZ - 1;
		Cells = MazeCellsDictionary.Cells;
	}

	private void ActivateWallsInLine(Vector3 direction, Vector2Int middleArea)
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
}
