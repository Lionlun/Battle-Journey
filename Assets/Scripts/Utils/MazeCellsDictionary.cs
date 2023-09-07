using System.Collections.Generic;
using UnityEngine;

public static class MazeCellsDictionary

{
	public static Dictionary<Vector2Int, MazeCellObject> Cells { get; set; } = new Dictionary<Vector2Int, MazeCellObject>();
	public static void CreateDictionary(Vector2Int key, MazeCellObject value)
	{
		Cells.Add(key, value);
	}
}
