using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleGearController : MonoBehaviour
{
    public int rotationIndex = 0;
    public int rotationIndex2 = 0;

    Quaternion[] rots = new Quaternion[6];
    Vector3[] poses = new Vector3[6];
    public Drawer draw;

    public bool is1Running = false;
    public bool is2Running = false;
    public float power = 0.1f;
    public float power2 = 0.1f;
    public bool opposite = false;
    public bool opposite2 = false;

    public bool jam1 = false;
    public bool jam2 = false;

    public bool unjam1 = false;
    public bool unjam2 = false;

    public void GetSurrounders()
    {
        
        Vector3 vec = new Vector3((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z);
        vec.x++;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (draw.worldMachines[vec].id == 4)
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 0 && this.rotationIndex2 != 3)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
            if (draw.worldMachines[vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.is1Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 0 && this.rotationIndex2 != 3)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex2 && this.rotationIndex2 != 0 && this.rotationIndex2 != 3)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power2 = gear.power2;
                        this.opposite2 = !gear.opposite2;
                        this.is2Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
        }
        vec.x -= 2;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (draw.worldMachines[vec].id == 4)
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 0 && this.rotationIndex2 != 3)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
            if (draw.worldMachines[vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.is1Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 0 && this.rotationIndex2 != 3)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex2 && this.rotationIndex2 != 0 && this.rotationIndex2 != 3)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power2 = gear.power2;
                        this.opposite2 = !gear.opposite2;
                        this.is2Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
        }
        vec.x++; //reset
        vec.y++;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (draw.worldMachines[vec].id == 4)
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 5 && this.rotationIndex2 != 2)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
            if (draw.worldMachines[vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.is1Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 5 && this.rotationIndex2 != 2)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex2 && this.rotationIndex2 != 5 && this.rotationIndex2 != 2)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power2 = gear.power2;
                        this.opposite2 = !gear.opposite2;
                        this.is2Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
        }
        vec.y -= 2;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (draw.worldMachines[vec].id == 4)
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 5 && this.rotationIndex2 != 2)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
            if (draw.worldMachines[vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.is1Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 5 && this.rotationIndex2 != 2)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex2 && this.rotationIndex2 != 5 && this.rotationIndex2 != 2)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power2 = gear.power2;
                        this.opposite2 = !gear.opposite2;
                        this.is2Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
        }
        vec.y++; //reset
        vec.z++;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (draw.worldMachines[vec].id == 4)
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 1 && this.rotationIndex2 != 4)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
            if (draw.worldMachines[vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.is1Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 4 && this.rotationIndex2 != 1)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex2 && this.rotationIndex2 != 4 && this.rotationIndex2 != 1)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power2 = gear.power2;
                        this.opposite2 = !gear.opposite2;
                        this.is2Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
        }
        vec.z -= 2;
        if (draw.worldMachines.ContainsKey(vec))
        {
            if (draw.worldMachines[vec].id == 4)
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 4 && this.rotationIndex2 != 1)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
            if (draw.worldMachines[vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.is1Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.is1Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is1Running = false;
                        this.jam1 = true;
                    }
                }
                if (gear.rotationIndex == this.rotationIndex2 && this.rotationIndex2 != 4 && this.rotationIndex2 != 1)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power2 = gear.power;
                        this.opposite2 = !gear.opposite;
                        this.is2Running = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex2 && this.rotationIndex2 != 4 && this.rotationIndex2 != 1)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power2 = gear.power2;
                        this.opposite2 = !gear.opposite2;
                        this.is2Running = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.is2Running = false;
                        this.jam2 = true;
                    }
                }
            }
        }
        if (this.is1Running && !this.jam1)
        {
            //turn 2 on for 1
            if (this.rotationIndex == 0)
            {
                if (this.rotationIndex2 != 3 && this.rotationIndex2 != 0)
                {
                    this.is2Running = this.is1Running;
                    this.power2 = this.power;
                    this.opposite2 = !this.opposite;
                }
            }
            if (this.rotationIndex == 1)
            {
                if (this.rotationIndex2 != 1 && this.rotationIndex2 != 4)
                {
                    this.is2Running = this.is1Running;
                    this.power2 = this.power;
                    this.opposite2 = !this.opposite;
                }
            }
            if (this.rotationIndex == 2)
            {
                if (this.rotationIndex2 != 2 && this.rotationIndex2 != 5)
                {
                    this.is2Running = this.is1Running;
                    this.power2 = this.power;
                    this.opposite2 = !this.opposite;
                }
            }
            if (this.rotationIndex == 3)
            {
                if (this.rotationIndex2 != 3 && this.rotationIndex2 != 0)
                {
                    this.is2Running = this.is1Running;
                    this.power2 = this.power;
                    this.opposite2 = !this.opposite;
                }
            }
            if (this.rotationIndex == 4)
            {
                if (this.rotationIndex2 != 4 && this.rotationIndex2 != 1)
                {
                    this.is2Running = this.is1Running;
                    this.power2 = this.power;
                    this.opposite2 = !this.opposite;
                }
            }
            if (this.rotationIndex == 5)
            {
                if (this.rotationIndex2 != 5 && this.rotationIndex2 != 2)
                {
                    this.is2Running = this.is1Running;
                    this.power2 = this.power;
                    this.opposite2 = !this.opposite;
                }
            }
        }
        else if (this.jam1 && !this.unjam1)
        {
            this.is2Running = false;
            this.jam2 = true;
        }
        {

        }
        if (this.is2Running && !this.jam2)
        {
            //turn 1 on for 2
            if (this.rotationIndex2 == 0)
            {
                if (this.rotationIndex != 3 && this.rotationIndex != 0)
                {
                    this.is1Running = this.is2Running;
                    this.power = this.power2;
                    this.opposite = !this.opposite2;
                }
            }
            if (this.rotationIndex2 == 1)
            {
                if (this.rotationIndex != 1 && this.rotationIndex != 4)
                {
                    this.is1Running = this.is2Running;
                    this.power = this.power2;
                    this.opposite = !this.opposite2;
                }
            }
            if (this.rotationIndex2 == 2)
            {
                if (this.rotationIndex != 2 && this.rotationIndex != 5)
                {
                    this.is1Running = this.is2Running;
                    this.power = this.power2;
                    this.opposite = !this.opposite2;
                }
            }
            if (this.rotationIndex2 == 3)
            {
                if (this.rotationIndex != 3 && this.rotationIndex != 0)
                {
                    this.is1Running = this.is2Running;
                    this.power = this.power2;
                    this.opposite = !this.opposite2;
                }
            }
            if (this.rotationIndex2 == 4)
            {
                if (this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    this.is1Running = this.is2Running;
                    this.power = this.power2;
                    this.opposite = !this.opposite2;
                }
            }
            if (this.rotationIndex2 == 5)
            {
                if (this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    this.is1Running = this.is2Running;
                    this.power = this.power2;
                    this.opposite = !this.opposite2;
                }
            }
        }
        else if (this.jam2 && !this.unjam2)
        {
            this.is1Running = false;
            this.jam1 = true;
        }
    }
    void Start()
    {

        rots[0] = Quaternion.LookRotation(this.transform.right);
        rots[1] = Quaternion.LookRotation(this.transform.forward);
        rots[2] = Quaternion.LookRotation(this.transform.up);
        rots[3] = Quaternion.LookRotation(-1 * this.transform.right);
        rots[4] = Quaternion.LookRotation(-1 * this.transform.forward);
        rots[5] = Quaternion.LookRotation(-1 * this.transform.up);

        poses[0] = new Vector3(0.1f, 0.5f, 0.5f);
        poses[1] = new Vector3(0.5f, 0.5f, 0.9f);
        poses[2] = new Vector3(0.5f, 0.1f, 0.5f);
        poses[3] = new Vector3(0.9f, 0.5f, 0.5f);
        poses[4] = new Vector3(0.5f, 0.5f, 0.1f);
        poses[5] = new Vector3(0.5f, 0.9f, 0.5f);
        this.transform.GetChild(0).rotation = rots[rotationIndex];
        this.transform.GetChild(0).localPosition = poses[rotationIndex];

    }

    public void UpdateRot1Index(int rot)
    {
        this.rotationIndex = rot;
        this.transform.GetChild(0).rotation = rots[rot];
        this.transform.GetChild(0).localPosition = poses[rot];
    }
    public void UpdateRot2Index(int rot)
    {
        this.rotationIndex2 = rot;
        this.transform.GetChild(1).rotation = rots[rot];
        this.transform.GetChild(1).localPosition = poses[rot];
    }
    Vector3 rotate = new();
    float jamtimer = 0;
    float jam2timer = 0;
    float unjamtimer = 0;
    float unjam2timer = 0;
    float ticktimer = 0;
    void Update()
    {
        if (unjam1)
        {
            unjamtimer += Time.deltaTime;
            if (unjamtimer > 2)
            {
                this.unjam1 = false;
                unjamtimer = 0;
            }
        }
        if (jam1)
        {
            jamtimer += Time.deltaTime;
            if (jamtimer > 1)
            {
                this.unjam1 = true;
                this.jam1 = false;
                jamtimer = 0;
            }
        }

        if (unjam2)
        {
            unjam2timer += Time.deltaTime;
            if (unjam2timer > 2)
            {
                this.unjam2 = false;
                unjam2timer = 0;
            }
        }
        if (jam2)
        {
            jam2timer += Time.deltaTime;
            if (jam2timer > 1)
            {
                this.unjam2 = true;
                this.jam2 = false;
                jam2timer = 0;
            }
        }
        if (ticktimer > 0.5f)
        {
            GetSurrounders();
        }
        else
        {
            ticktimer += Time.deltaTime;
        }
        if (is1Running)
        {
            rotate.x = 0;
            rotate.y = 0;
            rotate.z = power;
            if (opposite)
            {
                this.transform.GetChild(0).GetChild(0).Rotate(-1 * rotate);
            }
            else
            {
                this.transform.GetChild(0).GetChild(0).Rotate(rotate);
            }
        }
        if (is2Running)
        {
            rotate.x = 0;
            rotate.y = 0;
            rotate.z = power;
            if (opposite2)
            {
                this.transform.GetChild(1).GetChild(0).Rotate(-1 * rotate);
            }
            else
            {
                this.transform.GetChild(1).GetChild(0).Rotate(rotate);
            }
        }
    }
}
