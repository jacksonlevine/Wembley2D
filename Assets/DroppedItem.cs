using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Color color;
    public Sprite sprite;
    public int id;
    public int amount;
    private bool pickedUp = false;
    public GameObject player;
    public Texture2D texture;
    float timePeriod = 0;
    void Start()
    {
        cc = this.GetComponent<CharacterController>();
        cc.detectCollisions = false;
        SetTexture();
    }

    public void SetTexture()
    {
        texture = new Texture2D(16, 16);
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                    (int)sprite.textureRect.y,
                                                    (int)sprite.textureRect.width,
                                                    (int)sprite.textureRect.height);
        texture.SetPixels(pixels);
        texture.Apply();
        this.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
    }
    const float PI = 3.1415926535f;
    float angle, x1, y1;
    float range = 1.5f;
    Vector3 velocity = new();
    CharacterController cc;
    // Update is called once per frame
    void Update()
    {
        if(!cc.isGrounded && this.transform.position.y > 1)
        {
            this.velocity -= transform.up * Time.deltaTime;
        } else
        {
            this.velocity = Vector3.zero;
        }
        cc.Move(velocity);
        transform.Rotate(0, 1, 0);
        if (timePeriod < 3.6f)
        {
            timePeriod += Time.deltaTime;
            float r = 1f;
            angle = timePeriod * 100;
            x1 = r * Mathf.Cos(angle * PI / 180f);
            y1 = r * Mathf.Sin(angle * PI / 180f);
            transform.GetChild(0).localPosition = new Vector3(0, 0 + y1 / 4, 0);
        }
        else
        {
            timePeriod = 0;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < range && pickedUp == false)
        {
            if (player.GetComponent<PlayerScript>().AddToInventory(id, amount))
            {
                pickedUp = true;
                //this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                DestroyImmediate(this.transform.GetChild(0).gameObject);
                DestroyImmediate(this.gameObject);
            }
        }
    }
}
