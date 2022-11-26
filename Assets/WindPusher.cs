using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPusher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float timePeriod = 0;
    const float PI = 3.1415926535f;
    float angle, x1;
    // Update is called once per frame
    void Update()
    {
        if (timePeriod < 36f)
        {
            timePeriod += Time.deltaTime;
            float r = 10f;
            angle = timePeriod * 10;
            x1 = r * Mathf.Cos(angle * PI / 180f);
            transform.position = new Vector3(0 + x1, 0, 0);

        }
        else
        {
            timePeriod = 0;
        }



    }
}
