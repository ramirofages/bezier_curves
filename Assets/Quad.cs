using UnityEngine;
using System.Collections;

public class Quad
{
    public Vector3[] vertices;
    public Vector3[] normals = new Vector3[]
    {
                new Vector3( 0, 1, 0),
                new Vector3( 0, 1, 0),
                new Vector3( 0, 1, 0),
                new Vector3( 0, 1, 0)
    };
    public Vector2[] uvs = new Vector2[]
        {
                new Vector2(0,1),
                new Vector2(0,0),
                new Vector2(1,1),
                new Vector2(1,0)
        };
    public int[] triangleIndices = new int[]
        {
                0,1,2,
                0,2,3
        };

    public Quad(Vector3 start, Vector3 end, float width = 0.2f)
    {
        Initialize(start, end, width);
    }

    public void Initialize(Vector3 start, Vector3 end, float width)
    {
        Vector3 direction = (end - start).normalized;
        Vector3 tangente = new Vector3(-direction.y, direction.x, 0) * width;
        vertices = new Vector3[]
        {
            start+tangente,
            end+tangente,
            end-tangente,
            start-tangente

        };
    }
    public int[] triangleIteration(int number)
    {
        int[] indices = new int[triangleIndices.Length];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = triangleIndices[i]  + number * 4;
        }
        return indices;
    }
}
