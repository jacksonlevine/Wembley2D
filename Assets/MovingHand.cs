using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHand : MonoBehaviour
{
    public GameObject player;
    Vector3 initLocalPos;
    // Start is called before the first frame update
    void Start()
    {
        initLocalPos = transform.localPosition;
    }
    float timePeriod = 0;
    float angle, x1, y1;
    float PI = 3.1415926535f;

    float hittimer = 0;
    bool hit = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            hit = true;
        }
        if (hit)
        {
            if (hittimer < .45f)
            {
                if (timePeriod < .45f)
                {
                    hittimer += Time.deltaTime;
                    timePeriod += Time.deltaTime;
                    float r = 1f;
                    angle = timePeriod * 800;
                    x1 = r * Mathf.Cos(angle * PI / 180f);
                    y1 = r * Mathf.Sin(angle * PI / 180f);
                    transform.localPosition = initLocalPos + new Vector3(0 + x1 / 8, 0 + y1 / 12, 0);
                } else
                {
                    timePeriod = 0;
                }
            }
            else
            {
                hit = false;
                hittimer = 0;
            }
        }
        if (player.GetComponent<PlayerScript>().cc.velocity != Vector3.zero && !hit)
        {
            if (timePeriod < .9f)
            {
                timePeriod += Time.deltaTime/2;
                float r = 1f;
                angle = timePeriod * 800;
                x1 = r * Mathf.Cos(angle * PI / 180f);
                y1 = r * Mathf.Sin(angle * 2 * PI / 180f);
                transform.localPosition = initLocalPos + new Vector3(0 + x1/8, 0 + y1 / 12, 0);
            }
            else
            {
                timePeriod = 0;
            }
        }
    }
}
