using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool isOriginal = true;
    public PlayerScript player;
    public Drawer draw;

    public float timer = 5;
    const float PI = 3.1415926535f;
    float angle, x1, y1;
    public AudioSource aud;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        aud = this.GetComponent<AudioSource>();
        //aud.volume = Mathf.Clamp(1 - (Vector3.Distance(this.transform.position, player.transform.position)/30), 0.1f, 0.8f);
        aud.clip = clip;
        if (Time.time > 5)
        {
            aud.Play();
        }
    }

    public void Explode()
    {
        List<Vector3> spots = new();
        if (!isOriginal)
        {
            for(int i = -2; i < 2; i++)
            {
                for(int j = -2; j < 2; j++)
                {
                    for(int k = -2; k < 2; k++)
                    {
                        var spot = new Vector3Int(Mathf.RoundToInt(this.transform.position.x + i), Mathf.RoundToInt(this.transform.position.y + j), Mathf.RoundToInt(this.transform.position.z + k));
                        spots.Add(spot);
                        if (draw.worldMachines[player.dimension].ContainsKey(spot))
                        {
                            Destroy(draw.worldMachines[player.dimension][spot].linkedObject);
                            draw.worldMachines[player.dimension].Remove(spot);
                        }
                    }
                }
            }
            player.RemoveBlocks(spots);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!isOriginal)
        {
            
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            } else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
