using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
    Vector3 mymachpos = Vector3.zero;
    public Vector3 myprevpos = Vector3.zero;
    public Drawer draw;
    void Start()
    {
        mymachpos = new Vector3(Mathf.FloorToInt(this.transform.position.x), Mathf.FloorToInt(this.transform.position.y), Mathf.FloorToInt(this.transform.position.z));
    }

    float movetimer = 0;
    List<Vector3> possibleroutes = new();
    void MoveNext()
    {
        possibleroutes.Clear();
        Vector3[] vecs =
        {
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
        };
        foreach(var vec in vecs)
        {
            if (mymachpos + vec != myprevpos)
            {
                if (draw.worldMachines.ContainsKey(mymachpos + vec))
                {
                    if (draw.worldMachines[mymachpos + vec].id == draw.blockstore.pipeItem.id)
                    {
                        possibleroutes.Add(vec);
                    }
                }
            }
        }
        if (possibleroutes.Count > 0)
        {
            var thing = possibleroutes[Random.Range(0, possibleroutes.Count)];
            this.myprevpos = this.mymachpos;
            this.transform.position += thing;
            this.mymachpos += thing;
        } else
        {

        }
    }

    void Update()
    {
        if(movetimer > 1f)
        {
            MoveNext();
            movetimer = 0;
        } else
        {
            movetimer += Time.deltaTime;
        }
    }
}
