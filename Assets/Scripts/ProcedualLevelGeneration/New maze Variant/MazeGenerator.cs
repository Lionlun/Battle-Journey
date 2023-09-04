
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    public int MazeWidth;
    public int MazeHeight;
    public int StartX, StartY;
    NewMazeCell[,] maze;

    public MazeGenerator(int mazeWidth, int mazeHeight)
    {
        this.MazeWidth = mazeWidth;
        this.MazeHeight = mazeHeight;
    }

    public NewMazeCell[,] GetMaze()
    {
        maze = new NewMazeCell[MazeWidth, MazeHeight];

        for (int x = 0; x < MazeWidth; x++)
        {
            for (int y = 0; y < MazeHeight; y++)
            {
                maze[x, y] = new NewMazeCell(x, y);
            }
        }

        return maze;
    }
}
public class NewMazeCell
{
	public bool Visited;
	public int X, Y;
	public bool IsTopWall;
	public bool IsLeftWall;

	public Vector2Int Position
	{
		get { return new Vector2Int(X, Y); }
	}

	public NewMazeCell(int x, int y)
	{
		this.X = x;
		this.Y = y;
		Visited = false;
		IsTopWall = false;
		IsLeftWall = false;
	}
}