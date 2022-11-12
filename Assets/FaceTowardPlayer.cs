using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player, this.transform.up);
    }
}
