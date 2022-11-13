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

    public bool leaking = false;
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
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.leaking = false;
        } else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.leaking = true;
        }
    }
    float leaktimer = 0;

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
        if(this.leaking)
        {
            if(leaktimer > 2)
            {
                Destroy(this.gameObject);

            } else
            {
                leaktimer += Time.deltaTime;
            }
        }
    }
}
