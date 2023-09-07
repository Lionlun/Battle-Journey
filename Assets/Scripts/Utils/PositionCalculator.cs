
using UnityEngine;

public static class PositionCalculator 
{
	public static Vector2Int GetRandomMiddleAreaCoordinate(int worldXCoordinates, int worldZCoordinates)
	{
		var randomX = Random.Range(1, worldXCoordinates);
		var randomZ = Random.Range(1, worldZCoordinates);

		return new Vector2Int(randomX, randomZ);
	}

	public static Vector2Int GetRandomCorner(int worldXCoordinates, int worldZCoordinates)
	{
		var random = Random.Range(0, 4);

		Vector2Int[] corners = new Vector2Int[]
		{
			new Vector2Int(worldXCoordinates, worldZCoordinates),
			new Vector2Int(0, worldZCoordinates),
			new Vector2Int(worldXCoordinates, 0),
			new Vector2Int(0,0),
		};

		return corners[random];
	}
}
