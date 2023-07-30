using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedualGeneration : MonoBehaviour
{
    Vector3 roomSize;
	Vector3 wallSize;
    List<Matrix4x4> wallMatricesN = new List<Matrix4x4>();
    [SerializeField] Mesh wallMesh;
    [SerializeField] Material wallMaterial0;
	[SerializeField] Material wallMaterial1;


	void Start()
    {
        CreateWalls();

	}

    void Update()
    {
        RenderWalls();

	}

    void CreateWalls()
    {

        int wallCount = Mathf.Max(1, (int)(roomSize.x / wallSize.x));
        float scale = (roomSize.x / wallCount) / wallSize.x;

        for (int i = 0; i < wallCount; i++)
        {
            var t = transform.position + new Vector3(-roomSize.x/2 + wallSize.x*scale/2 + i*wallSize.x, 0, roomSize.y/2);
            var r = transform.rotation;
            var s = new Vector3(scale, 1, 1);

            var matrix = Matrix4x4.TRS(t, r, s);
            wallMatricesN.Add(matrix);

        }
    }

    void RenderWalls()
    {
        if (wallMatricesN!=null)
        {
            Graphics.DrawMeshInstanced(wallMesh, 0, wallMaterial0, wallMatricesN.ToArray(), wallMatricesN.Count);
            Graphics.DrawMeshInstanced(wallMesh, 1, wallMaterial1, wallMatricesN.ToArray(), wallMatricesN.Count);
        }
    }
}
