using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunscript : MonoBehaviour
{
    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.rotation.x >= 360)
        {
            this.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        } else
        {
            this.transform.Rotate(new Vector3(speed, 0, 0));
        }
    }
}
