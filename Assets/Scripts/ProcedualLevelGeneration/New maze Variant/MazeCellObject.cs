using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
	[SerializeField] GameObject topWall;
	[SerializeField] GameObject bottomWall;
	[SerializeField] GameObject leftWall;
	[SerializeField] GameObject rightWall;
	[SerializeField] GameObject floor;

	public Vector2Int CellPosition { get; set; }

	public void Init(bool top, bool bottom, bool left, bool right, bool isFloorActive)
	{
		topWall.SetActive(top);
		bottomWall.SetActive(bottom);
		leftWall.SetActive(left);
		rightWall.SetActive(right);
		floor.SetActive(isFloorActive);
	}

	public void DeactivateFloor()
	{
		floor.SetActive(false);
	}

	public void ActivateFloor()
	{
		floor.SetActive(true);
	}
}
