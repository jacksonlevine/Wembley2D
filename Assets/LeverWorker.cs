using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverWorker : MonoBehaviour
{
    public bool isOn = false;
    public Drawer draw;
    public int dimension = 0;
    void Start()
    {
        
    }

    public void Toggle()
    {
        if (!isOn) {
            isOn = true;
            this.transform.GetChild(1).rotation = Quaternion.LookRotation(this.transform.up + (this.transform.right / 2));
                } else
        {
            isOn = false;
            this.transform.GetChild(1).rotation = Quaternion.identity;
        }
    }

    public void GetSurrounders()
    {
        Vector3 vec = new Vector3((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z);
        vec.x++;
        if (!draw.worldMachines.ContainsKey(dimension))
        {
            draw.worldMachines.Add(dimension, new Dictionary<Vector3, Drawer.MachineNode>());
        }
        if (draw.worldMachines[dimension].ContainsKey(vec))
            {
                if (this.isOn)
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().isRunning = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().is1Running = true;
                    }
                } else
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().jam = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().jam1 = true;
                    }
                }
            }
            vec.x -= 2;
            if (draw.worldMachines[dimension].ContainsKey(vec))
            {
                if (this.isOn)
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().isRunning = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().is1Running = true;
                    }
                }
                else
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().jam = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().jam1 = true;
                    }
                }
            }
            vec.x++; //reset
            vec.y++;
            if (draw.worldMachines[dimension].ContainsKey(vec))
            {
                if (this.isOn)
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().isRunning = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().is1Running = true;
                    }
                }
                else
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().jam = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().jam1 = true;
                    }
                }
            }
            vec.y -= 2;
            if (draw.worldMachines[dimension].ContainsKey(vec))
            {
                if (this.isOn)
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().isRunning = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().is1Running = true;
                    }
                }
                else
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().jam = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().jam1 = true;
                    }
                }
            }
            vec.y++; //reset
            vec.z++;
            if (draw.worldMachines[dimension].ContainsKey(vec))
            {
                if (this.isOn)
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().isRunning = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().is1Running = true;
                    }
                }
                else
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().jam = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().jam1 = true;
                    }
                }
            }
            vec.z -= 2;
            if (draw.worldMachines[dimension].ContainsKey(vec))
            {
                if (this.isOn)
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().isRunning = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().is1Running = true;
                    }
                }
                else
                {
                    if (draw.worldMachines[dimension][vec].id == 4)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>().jam = true;
                    }
                    if (draw.worldMachines[dimension][vec].id == 6)
                    {
                        draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>().jam1 = true;
                    }
                }
            }
    }
    float checktimer = 0;
    // Update is called once per frame
    void Update()
    {
        if(checktimer > 0.5)
        {
            checktimer = 0;
            this.GetSurrounders();
        } else
        {
            checktimer += Time.deltaTime;
        }
    }
}
