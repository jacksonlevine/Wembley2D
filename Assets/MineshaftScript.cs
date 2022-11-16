using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineshaftScript : MonoBehaviour
{
    void Start()
    {
        Vector3[] verts =
        {
            new(0, 0, 0), //0
            new(1, 0, 0), //1
            new(1, 1, 0), //2
            new(0, 1, 0), //3
            new(0, 0, 1), //4
            new(1, 0, 1), //5
            new(1, 1, 1), //6
            new(0, 1, 1), //7
        };
        //inside out mesh cube
        int[] tris =
        {
            0, 3, 7, 0, 7, 4,
            4, 7, 6, 4, 6, 5,
            5, 6, 2, 5, 2, 1,
            1, 2, 3, 1, 3, 0,
            0, 4, 5, 0, 5, 1
        };

        Vector2[] uvs =
        {
            new(0, 0),
            new(0.5f, 0),
            new(0.5f, 0.5f),
            new(0, 0.5f),
            new(0, 0.5f),
            new(0.5f, 0.5f),
            new(0.5f, 1),
            new(0, 1),

        };
        Mesh mesh = new Mesh();
        
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        this.GetComponent<MeshFilter>().mesh = mesh;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
