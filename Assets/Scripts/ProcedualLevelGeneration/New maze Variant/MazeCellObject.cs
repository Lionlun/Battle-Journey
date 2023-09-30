using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
	public GameObject Floor;

	public Vector2Int CellPosition { get; set; }

	private void Start()
	{
	}

	public void DeactivateFloor()
	{
		Floor.SetActive(false);
	}

	public void ActivateFloor()
	{
		Floor.SetActive(true);
	}

}
