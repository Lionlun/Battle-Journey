using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum WallState
{
	Left = 1,
	Right = 2,
	Up = 4,
	Down = 8,
	Visited = 128,
}

public struct Position
{
	public int X;
	public int Z;
}

public struct Neighbour
{
	public Position Position;
	public WallState SharedWall;
}

public static class MazeGenerator
{

	private static WallState GetOppositeWall(WallState wall)
	{
		switch(wall)
		{
			case WallState.Right: return WallState.Left;
			case WallState.Left: return WallState.Right;
			case WallState.Up: return WallState.Down;
			case WallState.Down: return WallState.Up;

			default: return WallState.Left;
		}
	}

	private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
	{
		var random = new System.Random(/*seed*/);
		var positionStack = new Stack<Position>();
		var position = new Position {  X = random.Next(0,width), Z = random.Next(0,height) };

		maze[position.X, position.Z] |= WallState.Visited;
		positionStack.Push(position);

		while (positionStack.Count > 0)
		{
			var current = positionStack.Pop();
			var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

			if(neighbours.Count > 0)
			{
				positionStack.Push(current);

				var randomIndex = random.Next(0, neighbours.Count);
				var randomNeighbour = neighbours[randomIndex];

				var neighbourPosition = randomNeighbour.Position;
				maze[current.X, current.Z] &= ~randomNeighbour.SharedWall;
				maze[neighbourPosition.X, neighbourPosition.Z] &= ~GetOppositeWall(randomNeighbour.SharedWall);

				maze[neighbourPosition.X, neighbourPosition.Z] |= WallState.Visited;

				positionStack.Push(neighbourPosition);

			}
		}

		return maze;
	}

	private static List<Neighbour> GetUnvisitedNeighbours(Position pos, WallState[,] maze, int width, int height)
	{
		var list = new List<Neighbour>();

		if(pos.X >  0)
		{
			if (!maze[pos.X-1, pos.Z].HasFlag(WallState.Visited))
			{
				list.Add(new Neighbour
				{
					Position = new Position
					{
						X = pos.X - 1,
						Z = pos.Z
					},
					SharedWall = WallState.Left
				}) ;
			}
		}
		if (pos.Z > 0)
		{
			if (!maze[pos.X, pos.Z - 1].HasFlag(WallState.Visited))
			{
				list.Add(new Neighbour
				{
					Position = new Position
					{
						X = pos.X,
						Z = pos.Z-1
					},
					SharedWall = WallState.Down
				});
			}
		}

		if (pos.Z < height - 1)
		{
			if (!maze[pos.X, pos.Z+1].HasFlag(WallState.Visited))
			{
				list.Add(new Neighbour
				{
					Position = new Position
					{
						X = pos.X,
						Z = pos.Z+1
					},
					SharedWall = WallState.Up
				});
			}
		}

		if (pos.X < width - 1)
		{
			if (!maze[pos.X + 1, pos.Z].HasFlag(WallState.Visited))
			{
				list.Add(new Neighbour
				{
					Position = new Position
					{
						X = pos.X + 1,
						Z = pos.Z
					},
					SharedWall = WallState.Right
				});
			}
		}


		return list;
	}

 public static WallState[,] Generate(int width, int height)
	{
		WallState[,] maze = new WallState[width, height];
		WallState initial = WallState.Right | WallState.Left | WallState.Up | WallState.Down;

		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				maze[i, j] = initial;
			}
		}

		return ApplyRecursiveBacktracker(maze, width,height);
	}
}


