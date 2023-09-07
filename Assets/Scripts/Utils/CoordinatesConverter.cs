using UnityEngine;

public static class CoordinatesConverter
{
	public static Vector3 ConvertToWorldPosition(this Vector2Int coordinates)
	{
		var yOffset = new Vector3(0, 1, 0);
		var hasPosition = MazeCellsDictionary.Cells.TryGetValue(coordinates, out MazeCellObject cell);
		if (hasPosition)
		{
			return cell.transform.position + yOffset;
		}

		return Vector3.zero;
	}
}

