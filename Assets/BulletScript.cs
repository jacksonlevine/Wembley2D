using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 direction = new();
    public bool isOriginal = true;
    public Drawer draw;
    void Start()
    {

    }
    float lifetime = 3f;
    Ray ray = new Ray();
    RaycastHit hit = new();
    // Update is called once per frame
    void Update()
    {
        if (!isOriginal)
        {
            if (lifetime <= 0f)
            {
                Destroy(this.gameObject);

            }
            else; {
                lifetime -= Time.deltaTime;
            }
            this.transform.position += direction*2;
        }
        ray.origin = this.transform.position;
        Physics.Raycast(ray, out hit, 100f);
        if(hit.collider != null)
        {

        }
    }
}
