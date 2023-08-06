using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MazeCreator : MonoBehaviour
{
    int width = 10;
    int height = 10;
    int size = 1;
    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate(width, height);
    }

    public void Create(WallState cell, Vector3 pos, Transform wall)
    {
     
                if (cell.HasFlag(WallState.Up))
                {
                    var topWall = Instantiate(wall, transform);
                    topWall.position = pos + new Vector3(0,0,size/2);
                    topWall.localScale = new Vector3 (size, topWall.localScale.y, topWall.localScale.z);

                }
				if (cell.HasFlag(WallState.Left))
				{
                    var leftWall = Instantiate(wall, transform);
                    leftWall.position = pos + new Vector3(-size / 2, 0, 0);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
				}

				if (cell.HasFlag(WallState.Right))
				{

				}

				if (cell.HasFlag(WallState.Down))
				{

				}
		
    }
}
