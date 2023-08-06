using UnityEngine;

public class MazeCell : MonoBehaviour
{
	public bool Visited;
	public int X, Z;
	public bool TopWall;
	public bool LeftWall;

	public Vector2Int Position
	{
		get {  return new Vector2Int(X, Z);}
	}

	public MazeCell (int x, int z)
	{
		X = x;
		Z = z;

		Visited = false;
		TopWall = true;
		LeftWall = true;
	}
}
