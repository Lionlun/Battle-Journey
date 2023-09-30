using UnityEngine;

public class MazeGenerator
{
    public int MazeWidth;
    public int MazeHeight;
    NewFloorTile[,] floorTiles;

    public MazeGenerator(int mazeWidth, int mazeHeight)
    {
        this.MazeWidth = mazeWidth;
        this.MazeHeight = mazeHeight;
    }

    public NewFloorTile[,] GetMaze()
    {
        floorTiles = new NewFloorTile[MazeWidth, MazeHeight];

        for (int x = 0; x < MazeWidth; x++)
        {
            for (int y = 0; y < MazeHeight; y++)
            {
                floorTiles[x, y] = new NewFloorTile(x, y);
            }
        }

        return floorTiles;
    }
}
public class NewFloorTile
{
	public int X, Y;

	public Vector2Int Position
	{
		get { return new Vector2Int(X, Y); }
	}

	public NewFloorTile(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}
}