using System.Collections.Generic;
using UnityEngine;

public static class MazeCellsDictionary
{

	public static Dictionary<Vector2Int, MazeCellObject> Cells { get; set; } = new Dictionary<Vector2Int, MazeCellObject>();
	public static void CreateDictionary(Vector2Int key, MazeCellObject value)
	{
		SceneReload.OnSceneReload += ClearDictionary; //Скорее всего как-то переработать нормально
		Cells.Add(key, value);
	}

	public static void ClearDictionary()
	{
		Cells.Clear();
		SceneReload.OnSceneReload -= ClearDictionary; //Сделать отписку оптимальнее
	}
}
