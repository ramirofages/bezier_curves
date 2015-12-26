using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateQuad : MonoBehaviour {

	


    public void GenerateMeshFromSegments(List<Segment> segments)
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf.sharedMesh == null)
            mf.sharedMesh = new Mesh();
        Mesh mesh = mf.sharedMesh;

        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> triangles = new List<int>();

        
        for ( int i=0; i< segments.Count; i++)
        {
            
            Quad quad;
            if (segments[i].isBranch)
            {
                quad = new Quad(segments[i].start, segments[i].end, 0.1f);
                
            }
                
            else
                quad = new Quad(segments[i].start, segments[i].end);

           vertices.AddRange(quad.vertices);
            normals.AddRange(quad.normals);
            uvs.AddRange(quad.uvs);
            triangles.AddRange(quad.triangleIteration(i));
        }

        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangles.ToArray();
    }
}


