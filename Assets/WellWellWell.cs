using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellWellWell : MonoBehaviour
{
    public Drawer draw;
    public bool isOn = false;
    public GameObject waterball;
    void Start()
    {
        
    }
    public void CheckMechEnerg()
    {
        var me = new Vector3(Mathf.FloorToInt(this.transform.position.x), Mathf.FloorToInt(this.transform.position.y), Mathf.FloorToInt(this.transform.position.z));
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
            if (draw.worldMachines.ContainsKey(me + vec))
            {
                if (draw.worldMachines[me + vec].id == 4)
                {
                    var thisgear = draw.worldMachines[me + vec].linkedObject.GetComponent<GearController>();
                    if (thisgear.isRunning && !thisgear.jam)
                    {
                        bool thing = false;
                        this.isOn = true;
                        if (draw.worldMachines.ContainsKey(me + transform.up))
                        {
                            if (draw.worldMachines[me + transform.up].id == 8)
                            {
                                GameObject wb = Instantiate(waterball, me + transform.up, Quaternion.identity);
                                wb.SetActive(true);
                                wb.transform.GetChild(0).GetComponent<WaterBall>().myprevpos = me;
                                thing = true;
                            }

                        }
                        if (!thing)
                        {
                            this.transform.GetChild(0).gameObject.SetActive(true);
                        }

                    }
                    else if (thisgear.jam)
                    {
                        this.isOn = false;
                        this.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                if (draw.worldMachines[me + vec].id == 6)
                {
                    var thisgear = draw.worldMachines[me + vec].linkedObject.GetComponent<DoubleGearController>();
                    if ((thisgear.is1Running && !thisgear.jam1) || (thisgear.is2Running && !thisgear.jam2) || (thisgear.is2Running && !thisgear.jam2 && thisgear.is1Running && !thisgear.jam1))
                    {
                        bool thing = false;
                        this.isOn = true;
                        if (draw.worldMachines.ContainsKey(me + transform.up))
                        {
                            if (draw.worldMachines[me + transform.up].id == 8)
                            {
                                thing = true;
                            }

                        }
                        if (!thing)
                        {
                            this.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        
                    }
                    else if (thisgear.jam1 || thisgear.jam2)
                    {
                        this.isOn = false;
                        this.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    float welltimer = 0;
    void Update()
    {
        if(welltimer > 2f)
        {
            welltimer = 0;
            this.CheckMechEnerg();
        } else
        {
            welltimer += Time.deltaTime;
        }
    }
}
