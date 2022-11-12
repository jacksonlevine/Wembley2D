using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mounts : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position + (new Vector3(0, .25f, 1) * 100);
    }
}
