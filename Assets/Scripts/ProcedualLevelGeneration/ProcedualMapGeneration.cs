using UnityEngine;

public class ProcedualMapGeneration : MonoBehaviour
{
    public int WorldSizeX { get; set; } = 5;
    public int WorldSizeZ { get; set; } = 5;
    public float CellSize { get; set; } = 3f;

	private MazeRenderer mazeRenderer;

	void Awake()
    {
		mazeRenderer = GetComponent<MazeRenderer>();
		SrartGeneration();
	}
	void SrartGeneration()
    {
		CreateFloor();
	}
    private void CreateFloor()
    {
		mazeRenderer.RenderFloor(WorldSizeX, WorldSizeZ, CellSize);
	}
}
