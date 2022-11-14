using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkScript : MonoBehaviour
{

    public List<ItemSlot> myInv = new();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 21; i++)
        {
            ItemSlot slot = new ItemSlot();
            slot.id = 0;
            slot.amt = 0;
            myInv.Add(slot);
        }
        Vector3 relative = transform.InverseTransformPoint(Camera.main.transform.position);
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        this.transform.GetChild(0).transform.GetChild(0).Rotate(0, Mathf.RoundToInt(angle/90)*90, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
