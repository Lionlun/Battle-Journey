
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    public int MazeWidth;
    public int MazeHeight;
    public int StartX, StartY;
    NewMazeCell[,] maze;
    Vector2Int currentCell;

	List<Direction> directions = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

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
            for(int y = 0; y < MazeHeight; y++)
            {
                maze[x, y] = new NewMazeCell(x, y);
            }
        }
        CreatePath(StartX, StartY);

        return maze;
	}


    List<Direction> GetRandomDirections()
    {
        List<Direction> dir = new List<Direction>(directions);
        List<Direction> randomDirection = new List<Direction>();

        while (dir.Count > 0)
        {
            int random = Random.Range(0, dir.Count);
            randomDirection.Add(dir[random]);
            dir.RemoveAt(random);
        }
        return randomDirection;
    }

    bool IsCellValid(int x, int y)
    {
        if (x < 0 || y < 0 || x>MazeWidth-1|| y > MazeHeight-1 || maze[x, y].Visited)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    Vector2Int CheckNeighbours()
    {
        List<Direction> randomDirection = GetRandomDirections();
 

        for (int i = 0; i < randomDirection.Count; i++)
        {
			Vector2Int neighbour = currentCell;

			switch (randomDirection[i])
			{
				case Direction.Up:
					neighbour.y++;
                    break;
				case Direction.Down:
					neighbour.y--;
					break;
				case Direction.Right:
					neighbour.x++;
					break;
				case Direction.Left:
					neighbour.x--;
					break;
			}

            if (IsCellValid(neighbour.x, neighbour.y))
            {
                return neighbour;
            }
		}

        return currentCell;
    }

    void BreakWalls (Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if (primaryCell.x > secondaryCell.x)
        {
            maze[primaryCell.x, primaryCell.y].LeftWall = false;
        }
        else if (primaryCell.x < secondaryCell.x)
        {
			maze[secondaryCell.x, secondaryCell.y].LeftWall = false;
		}

		else if (primaryCell.y < secondaryCell.y)
		{
			maze[primaryCell.x, primaryCell.y].TopWall = false;
		}
		else if (primaryCell.y > secondaryCell.y)
		{
			maze[secondaryCell.x, secondaryCell.y].TopWall = false;
		}
	}

    void CreatePath(int x, int y)
    {
        if (x < 0||y<0 ||x > MazeWidth -1 || y > MazeHeight - 1)
        {
            x = 0;
            y = 0;
        }

        currentCell = new Vector2Int(x, y);
        List<Vector2Int> path = new List<Vector2Int>();

        bool deadEnd  = false;
        while (!deadEnd)
        {
            Vector2Int nextCell = CheckNeighbours();
            if (nextCell == currentCell)
            {
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbours();

                    if (nextCell != currentCell)
                    {
                        break;
                    }
                }
                if (nextCell == currentCell)
                {
                    deadEnd = true;
                }
            }
            else
            {
                BreakWalls(currentCell, nextCell);
                maze[currentCell.x, currentCell.y].Visited = true;
                currentCell = nextCell;
                path.Add(currentCell);
            }
        }
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}
public class NewMazeCell
{
    public bool Visited;
    public int X, Y;
    public bool TopWall;
    public bool LeftWall;

    public Vector2Int Position
    {
        get { return new Vector2Int(X, Y); }
    }

    public NewMazeCell(int x, int y)
    {
        this.X = x;
        this.Y = y;
        Visited = false;
        TopWall = true;
        LeftWall = true;
    }
}
