using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternScript : MonoBehaviour
{
    // Start is called before the first frame update
    Mesh mesh;
    List<Vector3> verts = new();
    List<Vector2> uvs = new();

    float fivepixels = 0.3125f;

    void Start()
    {
        mesh = new Mesh();
        verts.Add(new(fivepixels, fivepixels, fivepixels));     //0
        verts.Add(new(fivepixels, fivepixels, 1-fivepixels));   //1
        verts.Add(new(fivepixels, 1-fivepixels, fivepixels));   //2
        verts.Add(new(fivepixels, 1-fivepixels, 1 - fivepixels));  //3
        uvs.Add(new(0.5f, 0));
        uvs.Add(new(0, 0));
        uvs.Add(new(0.5f, 0.5f));
        uvs.Add(new(0, 0.5f));
        verts.Add(new(1-fivepixels, fivepixels, fivepixels));     //4
        verts.Add(new(1-fivepixels, fivepixels, 1 - fivepixels)); //5
        verts.Add(new(1-fivepixels, 1 - fivepixels, fivepixels)); //6
        verts.Add(new(1-fivepixels, 1 - fivepixels, 1 - fivepixels)); //7
        uvs.Add(new(0.5f, 0.5f));
        uvs.Add(new(0, 0.5f));
        uvs.Add(new(0.5f, 0));
        uvs.Add(new(0, 0));
        int[] tris =
        {
            1, 3, 2, //left
            1, 2, 0,
            0, 2, 6, //front
            0, 6, 4,
            4, 6, 7, //right
            4, 7, 5,
            5, 7, 3,//back
            5, 3, 1,
            2, 3, 7, //top
            2, 7, 6
        };

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris;
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();


        this.GetComponent<MeshFilter>().mesh = mesh;
        this.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Vector3.Distance(this.transform.position, Camera.main.transform.position) > 60)
        {
            if(this.transform.GetChild(0).GetComponent<Light>().enabled == true)
            {
                this.transform.GetChild(0).GetComponent<Light>().enabled = false;
            }
        } else
        {
            if (this.transform.GetChild(0).GetComponent<Light>().enabled == false)
            {
                this.transform.GetChild(0).GetComponent<Light>().enabled = true;
            }
        }*/
    }
}
