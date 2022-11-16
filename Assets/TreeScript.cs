using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool fall = false;
    float falltimer = 0;
    public void FallOver()
    {
        this.fall = true;
    }

    // Update is called once per frame
    void Update()
    {
      if(fall) { 
            if(falltimer < 4.5)
            {
                falltimer += Time.deltaTime;
                this.transform.Rotate(new Vector3(0, 0, 1));
            }
            else
            {
                Destroy(this.gameObject);
            }
        }  
    }
}
