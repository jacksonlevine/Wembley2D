using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMesher : MonoBehaviour
{
    public Drawer draw;
    public List<int> tris = new();
    public List<int> connectIDs = new();

    public float waterlevel = 0;

    public Sprite[] numbersSprites = new Sprite[11];

    // Start is called before the first frame update
    void Start()
    {
        connectIDs.Add(8);
        connectIDs.Add(9);
        mesh = new Mesh();
        Remesh();
    }

    Mesh mesh;
    public void Remesh()
    {
        mesh = new Mesh();
        tris.Clear();
        this.jrtimer = 0;
        this.justremeshed = true;
        Vector3[] verts = {
        new Vector3(0.3f, 0.3f, 0.3f), //0    //inner front
        new Vector3(0.3f, 0.7f, 0.3f), //1
        new Vector3(0.7f, 0.3f, 0.3f), //2
        new Vector3(0.7f, 0.7f, 0.3f), //3
        new Vector3(0.3f, 0.3f, 0),     //4  //outer front
        new Vector3(0.3f, 0.7f, 0),    //5
        new Vector3(0.7f, 0.3f, 0),    //6
        new Vector3(0.7f, 0.7f, 0),   //7

        new Vector3(0.3f, 0.3f, 0.7f), //8   //inner back
        new Vector3(0.3f, 0.7f, 0.7f), //9
        new Vector3(0.7f, 0.3f, 0.7f), //10
        new Vector3(0.7f, 0.7f, 0.7f), //11
        new Vector3(0.3f, 0.3f, 1),     //12  //outer back
        new Vector3(0.3f, 0.7f, 1),    //13
        new Vector3(0.7f, 0.3f, 1),    //14
        new Vector3(0.7f, 0.7f, 1),    //15


        new Vector3(0, 0.3f, 0.3f),    //16    //outer left
        new Vector3(0, 0.7f, 0.3f),    //17
        new Vector3(0, 0.3f, 0.7f),    //18
        new Vector3(0, 0.7f, 0.7f),    //19


        new Vector3(1, 0.3f, 0.3f),    //20  //outer right
        new Vector3(1, 0.7f, 0.3f),    //21
        new Vector3(1, 0.3f, 0.7f),    //22
        new Vector3(1, 0.7f, 0.7f),   //23


        new Vector3(0.3f, 1, 0.3f),   //24    //outer top
        new Vector3(0.7f, 1, 0.3f),   //25
        new Vector3(0.7f, 1, 0.7f),   //26
        new Vector3(0.3f, 1, 0.7f),  //27

        new Vector3(0.3f, 0, 0.3f),  //28      //outer bottom
        new Vector3(0.7f, 0, 0.3f),  //29
        new Vector3(0.7f, 0, 0.7f),  //30
        new Vector3(0.3f, 0, 0.7f)   //31
        };
        mesh.vertices = verts;
        tris.Add(0); //front inner
        tris.Add(1);
        tris.Add(3);
        tris.Add(0);
        tris.Add(3);
        tris.Add(2);

        tris.Add(8); //left inner
        tris.Add(9);
        tris.Add(1);
        tris.Add(8);
        tris.Add(1);
        tris.Add(0);

        tris.Add(2); //right inner
        tris.Add(3);
        tris.Add(11);
        tris.Add(2);
        tris.Add(11);
        tris.Add(10);

        tris.Add(3); //top inner
        tris.Add(1);
        tris.Add(9);
        tris.Add(3);
        tris.Add(9);
        tris.Add(11);

        tris.Add(8); //bottom inner
        tris.Add(0);
        tris.Add(2);
        tris.Add(8);
        tris.Add(2);
        tris.Add(10);

        tris.Add(10); //back inner
        tris.Add(11);
        tris.Add(9);
        tris.Add(10);
        tris.Add(9);
        tris.Add(8);


        Vector3 vec = new Vector3((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z);
        vec.x++;
        if(draw.worldMachines.ContainsKey(vec))
        {
            if (this.connectIDs.Contains(draw.worldMachines[vec].id))
            {
                //show connected pipe;
                tris.Add(2); //front          //left outer
                tris.Add(3);
                tris.Add(21);
                tris.Add(2);
                tris.Add(21);
                tris.Add(20);

                tris.Add(3); //top
                tris.Add(11);
                tris.Add(23);
                tris.Add(3);
                tris.Add(23);
                tris.Add(21);

                tris.Add(10); //bottom
                tris.Add(2);
                tris.Add(20);
                tris.Add(10);
                tris.Add(20);
                tris.Add(22);

                tris.Add(22); //back
                tris.Add(23);
                tris.Add(11);
                tris.Add(22);
                tris.Add(11);
                tris.Add(10);
                if(draw.worldMachines[vec].id == 8 && draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().justremeshed != true)
                {
                    draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().Remesh();
                }
            }
        }
        vec.x-=2;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (this.connectIDs.Contains(draw.worldMachines[vec].id))
            {
                
                tris.Add(16); //front          //right outer
                tris.Add(17);
                tris.Add(1);
                tris.Add(16);
                tris.Add(1);
                tris.Add(0);

                tris.Add(17); //top
                tris.Add(19);
                tris.Add(9);
                tris.Add(17);
                tris.Add(9);
                tris.Add(1);

                tris.Add(18); //bottom
                tris.Add(16);
                tris.Add(0);
                tris.Add(18);
                tris.Add(0);
                tris.Add(8);

                tris.Add(8); //back
                tris.Add(9);
                tris.Add(19);
                tris.Add(8);
                tris.Add(19);
                tris.Add(18);
                if (draw.worldMachines[vec].id == 8 && draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().justremeshed != true)
                {
                    draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().Remesh();
                }
            }
        }
        vec.x++;
        vec.y++;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (this.connectIDs.Contains(draw.worldMachines[vec].id))
            {
                tris.Add(1); //front          //top outer
                tris.Add(24);
                tris.Add(25);
                tris.Add(1);
                tris.Add(25);
                tris.Add(3);

                tris.Add(9); //left
                tris.Add(27);
                tris.Add(24);
                tris.Add(9);
                tris.Add(24);
                tris.Add(1);

                tris.Add(3); //right
                tris.Add(25);
                tris.Add(26);
                tris.Add(3);
                tris.Add(26);
                tris.Add(11);

                tris.Add(11); //back
                tris.Add(26);
                tris.Add(27);
                tris.Add(11);
                tris.Add(27);
                tris.Add(9);
                if (draw.worldMachines[vec].id == 8 && draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().justremeshed != true)
                {
                    draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().Remesh();
                }
            }
        }
        vec.y-=2;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (this.connectIDs.Contains(draw.worldMachines[vec].id))
            {
                tris.Add(28); //front          //bottom outer
                tris.Add(0);
                tris.Add(2);
                tris.Add(28);
                tris.Add(2);
                tris.Add(29);

                tris.Add(31); //left
                tris.Add(8);
                tris.Add(0);
                tris.Add(31);
                tris.Add(0);
                tris.Add(28);

                tris.Add(29); //right
                tris.Add(2);
                tris.Add(10);
                tris.Add(29);
                tris.Add(10);
                tris.Add(30);

                tris.Add(30); //back
                tris.Add(10);
                tris.Add(8);
                tris.Add(30);
                tris.Add(8);
                tris.Add(31);
                if (draw.worldMachines[vec].id == 8 && draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().justremeshed != true)
                {
                    draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().Remesh();
                }
            }
        }
        vec.y++;
        vec.z++;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (this.connectIDs.Contains(draw.worldMachines[vec].id))
            {
                tris.Add(9); //top        //back outer
                tris.Add(13);
                tris.Add(15);
                tris.Add(9);
                tris.Add(15);
                tris.Add(11);

                tris.Add(12); //left
                tris.Add(13);
                tris.Add(9);
                tris.Add(12);
                tris.Add(9);
                tris.Add(8);

                tris.Add(10); //right
                tris.Add(11);
                tris.Add(15);
                tris.Add(10);
                tris.Add(15);
                tris.Add(14);

                tris.Add(12); //bottom
                tris.Add(8);
                tris.Add(10);
                tris.Add(12);
                tris.Add(10);
                tris.Add(14);
                if (draw.worldMachines[vec].id == 8 && draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().justremeshed != true)
                {
                    draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().Remesh();
                }
            }
        }
        vec.z-=2;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (this.connectIDs.Contains(draw.worldMachines[vec].id))
            {
                tris.Add(5); //top        //front outer
                tris.Add(1);
                tris.Add(3);
                tris.Add(5);
                tris.Add(3);
                tris.Add(7);

                tris.Add(0); //left
                tris.Add(1);
                tris.Add(5);
                tris.Add(0);
                tris.Add(5);
                tris.Add(4);

                tris.Add(6); //right
                tris.Add(7);
                tris.Add(3);
                tris.Add(6);
                tris.Add(3);
                tris.Add(2);

                tris.Add(0); //bottom
                tris.Add(4);
                tris.Add(6);
                tris.Add(0);
                tris.Add(6);
                tris.Add(2);
                if (draw.worldMachines[vec].id == 8 && draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().justremeshed != true)
                {
                    draw.worldMachines[vec].linkedObject.GetComponent<PipeMesher>().Remesh();
                }
            }
        }
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
        //this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        
    }

    float jrtimer = 0;
    bool justremeshed = false;
    // Update is called once per frame
    void Update()
    {
        if(jrtimer > 0.3)
        {
            justremeshed = false;
        } else
        {
            jrtimer += Time.deltaTime;
        }

        //this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = numbersSprites[Mathf.FloorToInt(this.waterlevel)];
    }
}
