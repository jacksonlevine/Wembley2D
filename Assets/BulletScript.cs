using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class BulletScript : MonoBehaviour
{
    public Vector3 direction = new();
    public bool isOriginal = true;
    public Drawer draw;
    public GameObject explosion;
    public AudioSource aud;
    public AudioClip clip;
    void Start()
    {
        aud = this.GetComponent<AudioSource>();
        aud.clip = clip;
        if (Time.time > 5)
        {
            aud.Play();
        }
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
        
        ray.origin = this.transform.position;
            ray.direction = direction;
        Physics.Raycast(ray, out hit, 3f);
        if(hit.collider != null)
        {
                GameObject c = Instantiate(explosion, this.transform.position, Quaternion.identity);
                c.GetComponent<SpriteRenderer>().enabled = true;
                c.GetComponent<Light>().enabled = true;
                c.GetComponent<Explosion>().isOriginal = false;
                c.GetComponent<Explosion>().Explode();
                Destroy(this.gameObject);
        }
        }
    }
}
