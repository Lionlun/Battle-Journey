using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(MazeRenderer))]
public class FloorTrap : MonoBehaviour
{
	private List<MazeCellObject> cells = new List<MazeCellObject>();
	private MazeRenderer mazeRenderer;
	private int secondsToReturnFloor = 2;

	private float minimumTimeToTriggerTrap = 5;
	private float maximumTimeToTriggerTrap = 30;

	private IEnumerator currentRoutine;

	private async void Start()
	{
		mazeRenderer = GetComponent<MazeRenderer>();
		await Task.Delay(1000); // ToDo Сделать нормально, временное решение Дожидаемся, когда сформируется список. Т.Е делаем Await, либо Event

		FindAllMazeCells();
		currentRoutine = TrapActivationRoutine();
		StartCoroutine(currentRoutine);
	}

	private void FindAllMazeCells()
	{
		foreach (Transform child in transform)
		{
			if (child.gameObject.GetComponent<MazeCellObject>() != null)
			{
				cells.Add(child.gameObject.GetComponent<MazeCellObject>());
			}
		}
	}
	
	private void StartFloorTrap()
	{
		var cellsWithWall = FindAllCellsWithWall();
		var random = Random.Range(0, cellsWithWall.Count-1);
		var cellFlash = cellsWithWall[random].GetComponentInChildren<FloorFlash>();
		ManageTrapOperation(cellFlash);
	}

	private List<MazeCellObject> FindAllCellsWithWall()
	{
		var cellsWithWall = new List<MazeCellObject>();

		foreach (var cell in MazeCellsDictionary.Cells.Values)
		{
			if (cell.CheckWallsPresence())
			{
				cellsWithWall.Add(cell);
			}
		}
		return cellsWithWall;
	}

	private async void ManageTrapOperation(FloorFlash floor)
	{
		await floor.ActivateTrap();
		await Task.Delay(secondsToReturnFloor*1000);

		if(floor != null)
		{
			floor.ResetTrap();
		}
	}

	private IEnumerator TrapActivationRoutine()
	{
		while(true)
		{
			var randomTime = Random.Range(minimumTimeToTriggerTrap, maximumTimeToTriggerTrap);
			yield return new WaitForSeconds(randomTime);
			StartFloorTrap();
			yield return null;
		}
	}
}
