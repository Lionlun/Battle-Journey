using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
	public Wall TopWall;
	public Wall BottomWall;
	public Wall LeftWall;
	public Wall RightWall;
	public GameObject Floor;

	public Vector2Int CellPosition { get; set; }

	private void Start()
	{
		foreach (Transform child in transform)
		{
			if (child.gameObject.GetComponent<Wall>() != null)
			{
				
			}
		}
	}

	public void Init(bool top, bool bottom, bool left, bool right, bool isFloorActive)
	{
		TopWall.gameObject.SetActive(top);
		BottomWall.gameObject.SetActive(bottom);
		LeftWall.gameObject.SetActive(left);
		RightWall.gameObject.SetActive(right);
		Floor.SetActive(isFloorActive);
	}

	public void DeactivateFloor()
	{
		Floor.SetActive(false);
	}

	public void ActivateFloor()
	{
		Floor.SetActive(true);
	}

	public void ActivateWalls(WallSide wallside)
	{
		if(wallside == WallSide.Left)
		{
			LeftWall.gameObject.SetActive(true);
		}
		if(wallside == WallSide.Right)
		{
			RightWall.gameObject.SetActive(true);
		}
		if(wallside == WallSide.Bottom)
		{
			BottomWall.gameObject.SetActive(true);
		}
		if (wallside == WallSide.Top)
		{
			TopWall.gameObject.SetActive(true);
		}
	}

	public void DeactivateWalls(bool top, bool bottom, bool left, bool right)
	{

	}
	public bool CheckWallsPresence()
	{
		if (BottomWall.isActiveAndEnabled || TopWall.isActiveAndEnabled || LeftWall.isActiveAndEnabled || RightWall.isActiveAndEnabled) 
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
