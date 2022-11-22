using UnityEngine;

public class GearController : MonoBehaviour
{
    public int rotationIndex = 0;

    private Quaternion[] rots = new Quaternion[6];
    private Vector3[] poses = new Vector3[6];

    public bool isRunning = false;
    public float power = 0.1f;
    public bool opposite = false;
    public Drawer draw;
    public bool jam = false;
    public bool unjam = false;

    public int dimension = 0;

    public void GetSurrounders()
    {
        Vector3 vec = new Vector3((int)this.transform.position.x, (int)this.transform.position.y, (int)this.transform.position.z);
        vec.x++;
        if (!draw.worldMachines.ContainsKey(dimension))
        {
            draw.worldMachines.Add(dimension, new System.Collections.Generic.Dictionary<Vector3, Drawer.MachineNode>());
        }
        if (draw.worldMachines[dimension].ContainsKey(vec))
        {
            if (draw.worldMachines[dimension][vec].id == 4)
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.isRunning && !gear.jam) 
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    } else if(gear.jam && !gear.unjam)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
            if (draw.worldMachines[dimension][vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.isRunning = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
        }
        vec.x -= 2;
        if (draw.worldMachines[dimension].ContainsKey(vec))
        {
            if (draw.worldMachines[dimension][vec].id == 4)
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
            if (draw.worldMachines[dimension][vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 0 && this.rotationIndex != 3)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.isRunning = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
        }
        vec.x++; //reset
        vec.y++;
        if (draw.worldMachines[dimension].ContainsKey(vec))
        {
            if (draw.worldMachines[dimension][vec].id == 4)
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
            if (draw.worldMachines[dimension][vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.isRunning = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
        }
        vec.y-=2;
        if (draw.worldMachines[dimension].ContainsKey(vec))
        {
            if (draw.worldMachines[dimension][vec].id == 4)
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
            if (draw.worldMachines[dimension][vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 5 && this.rotationIndex != 2)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.isRunning = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
        }
        vec.y++; //reset
        vec.z++;
        if (draw.worldMachines[dimension].ContainsKey(vec))
        {
            if (draw.worldMachines[dimension][vec].id == 4)
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
            if (draw.worldMachines[dimension][vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.isRunning = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
        }
        vec.z -= 2;
        if (draw.worldMachines[dimension].ContainsKey(vec))
        {
            if (draw.worldMachines[dimension][vec].id == 4)
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<GearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.isRunning && !gear.jam)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam && !gear.unjam)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
            if (draw.worldMachines[dimension][vec].id == 6) //double gear
            {
                var gear = draw.worldMachines[dimension][vec].linkedObject.GetComponent<DoubleGearController>();
                if (gear.rotationIndex == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.is1Running && !gear.jam1)
                    {
                        this.power = gear.power;
                        this.opposite = !gear.opposite;
                        this.isRunning = true;
                    }
                    else if (gear.jam1 && !gear.unjam1)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
                if (gear.rotationIndex2 == this.rotationIndex && this.rotationIndex != 4 && this.rotationIndex != 1)
                {
                    if (gear.is2Running && !gear.jam2)
                    {
                        this.power = gear.power2;
                        this.opposite = !gear.opposite2;
                        this.isRunning = true;
                    }
                    else if (gear.jam2 && !gear.unjam2)
                    {
                        this.isRunning = false;
                        this.jam = true;
                    }
                }
            }
        }
    }
    private float ticktimer = 0;

    private void Start()
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

    public void UpdateRotIndex(int rot)
    {
        this.rotationIndex = rot;
        this.transform.GetChild(0).rotation = rots[rot];
        this.transform.GetChild(0).localPosition = poses[rot];
    }

    private Vector3 rotate = new();
    private float jamtimer = 0;
    private float unjamtimer = 0;
    private void Update()
    {
        if(unjam)
        {
            unjamtimer += Time.deltaTime;
            if(unjamtimer > 2)
            {
                this.unjam = false;
                unjamtimer = 0;
            }
        }
        if(jam)
        {
            jamtimer += Time.deltaTime;
            if(jamtimer > 1)
            {
                this.unjam = true;
                this.jam = false;
                jamtimer = 0;
            }
        }
        if(ticktimer > 0.5f)
        {
            GetSurrounders();
        } else
        {
            ticktimer += Time.deltaTime;
        }
        if (isRunning)
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
    }
}