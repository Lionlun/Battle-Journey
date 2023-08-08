
using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
	[SerializeField] GameObject topWall;
	[SerializeField] GameObject bottomWall;
	[SerializeField] GameObject leftWall;
	[SerializeField] GameObject rightWall;
	[SerializeField] GameObject floor;

	public void Init(bool top, bool bottom, bool left, bool right, bool isFloorActive)
	{
		topWall.SetActive(top);
		bottomWall.SetActive(bottom);
		leftWall.SetActive(left);
		rightWall.SetActive(right);
		floor.SetActive(isFloorActive);
	}
}
