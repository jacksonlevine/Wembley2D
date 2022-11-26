using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public List<ItemSlot> myInv = new();
    public Sprite[] sprites;
    public Texture2D texture;
    public Texture2D texture2;

    public Vector3 velocity = Vector3.zero;
    public Ray ray = new Ray();
    private RaycastHit hit;
    public Drawer draw;
    public Blocks blockstore;

    public Color[] lampColors;


    private float walktime = 0;

    public int dimension = 0;

    public Ray walkray = new Ray();
    public Ray walkrayleft = new Ray();
    public Ray walkrayright = new Ray();
    public Ray walkrayback = new Ray();
    public Ray finalray = new Ray();
    private RaycastHit walkhit;
    private RaycastHit walkhitleft;
    private RaycastHit walkhitright;
    private RaycastHit walkhitback;
    private RaycastHit finalhit;
    public CharacterController cc;

    public Texture2D[] skyBoxTextures = new Texture2D[2];


    private Ray r;
    private RaycastHit h;
    public GameObject selectedCube;

    public List<ItemSlot> currentChestInv;

    public bool isLoading = false;

    public bool isInventoryOpen = false;
    void DoSelected()
    {
        r.origin = (transform.position + transform.up) + (Camera.main.transform.forward * 0.5f);
        r.direction = Camera.main.transform.forward;
        Physics.Raycast(r, out h, 30f);
        if(h.collider != null)
        {
            if (h.collider.gameObject.tag != draw.tree.tag)
            {
                var thing = h.point + (r.direction * .1f);
                selectedCube.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                selectedCube.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                selectedCube.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                selectedCube.transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                selectedCube.transform.GetChild(0).transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                selectedCube.transform.GetChild(0).transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                selectedCube.transform.position = new Vector3(Mathf.FloorToInt(thing.x), Mathf.FloorToInt(thing.y), Mathf.FloorToInt(thing.z));
            } else
            {
                selectedCube.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                selectedCube.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                selectedCube.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                selectedCube.transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                selectedCube.transform.GetChild(0).transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                selectedCube.transform.GetChild(0).transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        } else
        {
            //selectedCube.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(3).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            selectedCube.transform.GetChild(0).transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    void Start()
    {
        Camera.main.gameObject.GetComponent<Skybox>().material.SetTexture("_MainTex", skyBoxTextures[0]);
        cc = this.gameObject.GetComponent<CharacterController>();
        for(int i = 0; i < 35; i++)
        {
            ItemSlot slot = new();
            slot.id = 0;
            slot.amt = 0;
            myInv.Add(slot);
        }
        texture = new Texture2D(16, 16);
        texture2 = new Texture2D(16, 16);

        texture.filterMode = FilterMode.Point;
        
        texture2.filterMode = FilterMode.Point;
        Cursor.lockState = CursorLockMode.Locked;
        myInv[0].id = 2;
        myInv[0].amt = 128;
        myInv[1].id = 4;
        myInv[1].amt = 99;
        myInv[2].id = 5;
        myInv[2].amt = 1;
        myInv[3].id = 7;
        myInv[3].amt = 20;
        myInv[4].id = 8;
        myInv[4].amt = 99;
        myInv[5].id = 9;
        myInv[5].amt = 5;
        myInv[10].id = 10;
        myInv[10].amt = 5;
        myInv[11].id = 11;
        myInv[11].amt = 5;
        myInv[12].id = 12;
        myInv[12].amt = 1;
        myInv[13].id = 13;
        myInv[13].amt = 199;
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    Rect position = new(); 
    float invTileWidth = Screen.width / 32;
    public void DrawItemSlot(ItemSlot slot, Vector2 pos)
    {
            position.x = pos.x;
            position.y = pos.y;
            position.width = invTileWidth;
            position.height = invTileWidth;
            var pixels1 = sprites[0].texture.GetPixels((int)sprites[0].textureRect.x,
                                                    (int)sprites[0].textureRect.y,
                                                    (int)sprites[0].textureRect.width,
                                                    (int)sprites[0].textureRect.height);
            texture.SetPixels(pixels1);
            texture.Apply(); 

        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
        if (slot.id != 0)
        {
            position.x = pos.x+7;
            position.y = pos.y+7;
            position.width = Screen.width / 40;
            position.height = Screen.width / 40;
            var pixels = sprites[slot.id].texture.GetPixels((int)sprites[slot.id].textureRect.x,
                                                    (int)sprites[slot.id].textureRect.y,
                                                    (int)sprites[slot.id].textureRect.width,
                                                    (int)sprites[slot.id].textureRect.height);
            texture2.SetPixels(pixels);
            texture2.Apply();
            GUI.skin.box.normal.background = texture2;
            GUI.Box(position, GUIContent.none);
            position.width = 100;
            position.y += Screen.width / 40;
            GUI.Label(position, slot.amt.ToString());
        }
        
    }
    public Sprite selectedSp;
    public void DrawSelectedSquare(Vector2 pos)
    {
        position.x = pos.x;
        position.y = pos.y;
        position.width = Screen.width / 32;
        position.height = Screen.width / 32;
        var pixels1 = selectedSp.texture.GetPixels((int)selectedSp.textureRect.x,
                                                (int)selectedSp.textureRect.y,
                                                (int)selectedSp.textureRect.width,
                                                (int)selectedSp.textureRect.height);
        texture.SetPixels(pixels1);
        texture.Apply();

        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);

    }
    public Vector2 mousePosInInvTerms;
    public GUISkin skin;
    public ItemSlot mousedOverSlot;

    public int mousedSlots = 0;
    public bool isMouseCurrentlyOnSlot = false;

    public void SwitchDimension()
    {
        cc.Move(transform.up * 20);

        //unload all these
        if (draw.worldMachines.ContainsKey(draw.dimension))
        {
            foreach (KeyValuePair<Vector3, Drawer.MachineNode> mach in draw.worldMachines[draw.dimension])
            {
                mach.Value.linkedObject.gameObject.SetActive(false);
            }
        }


        if (Input.GetKey(KeyCode.LeftShift) && draw.dimension > 0)
        {
            draw.dimension--;
        }
        else
        {
            draw.dimension++;
        }
        if (draw.dimension == 0)
        {
            Camera.main.gameObject.GetComponent<Skybox>().material.SetTexture("_MainTex", skyBoxTextures[0]);
        }
        else
        {
            Camera.main.gameObject.GetComponent<Skybox>().material.SetTexture("_MainTex", skyBoxTextures[1]);
        }
        draw.remeshQueue.Clear();


        foreach (KeyValuePair<Vector3, GameObject> chuk in draw.chunks)
        {
            chuk.Value.GetComponent<ChunkTerrain>().isInRemeshQueue = false;
            chuk.Value.GetComponent<ChunkTerrain>().OnInstantiate();
        }

        //load all these
        if (draw.worldMachines.ContainsKey(draw.dimension))
        {
            foreach (KeyValuePair<Vector3, Drawer.MachineNode> mach in draw.worldMachines[draw.dimension])
            {
                mach.Value.linkedObject.gameObject.SetActive(true);
            }
        }
    }
    private void OnGUI()
    {
        GUI.skin = skin;
        mousePosInInvTerms = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        mousedSlots = 0;

        if(isLoading)
        {
            GUI.Label(new Rect(200, 10, 300, 30), "Loading world...");
        }


        if(mouseSlot.id != 0)
        {
            DrawItemSlot(mouseSlot, mousePosInInvTerms);
        }
        for (int i = 0; i < 7; i++)
        {
            var spot = new Vector2(i * (Screen.width / 24) + 30, Screen.height - (Screen.height / 6));
            
            DrawItemSlot(myInv[i], spot);
            var centerofspot = new Vector2(spot.x + (invTileWidth/2), spot.y + (invTileWidth / 2));
            if(selected == i || Vector2.Distance(centerofspot, mousePosInInvTerms) < invTileWidth/2) 
            {
                DrawSelectedSquare(spot);
            }
            if(Vector2.Distance(centerofspot, mousePosInInvTerms) < invTileWidth / 2)
            {
                this.mousedOverSlot = myInv[i];
                mousedSlots++;
            }
        }

        if(isInventoryOpen)
        {
            for(int i = 7; i < myInv.Count; i++)
            {
                var spot = new Vector2(((i % 7) * (Screen.width / 24)) + 30, Screen.height - ((Screen.height / 2)+invTileWidth) + ((5-(i / 7)) * invTileWidth*1.2f));
                var centerofspot = new Vector2(spot.x + (invTileWidth / 2), spot.y + (invTileWidth / 2));
                DrawItemSlot(myInv[i], spot);
                if (selected == i || Vector2.Distance(centerofspot, mousePosInInvTerms) < invTileWidth / 2)
                {
                    DrawSelectedSquare(spot);
                }
                if (Vector2.Distance(centerofspot, mousePosInInvTerms) < invTileWidth / 2)
                {
                    this.mousedOverSlot = myInv[i];
                    mousedSlots++;
                }
            }
        }

        if (isOtherInventoryOpen)
        {
            GUI.Label(new Rect(Screen.width / 3, (Screen.height / 2) + (invTileWidth*2), invTileWidth * 10, invTileWidth), "Trunk:");
            for (int i = 0; i < currentChestInv.Count; i++)
            {
                var spot = new Vector2(((i % 7) * (Screen.width / 24)) + Screen.width/3, Screen.height - ((Screen.height / 2) + invTileWidth) + ((5 - (i / 7)) * invTileWidth * 1.2f));
                var centerofspot = new Vector2(spot.x + (invTileWidth / 2), spot.y + (invTileWidth / 2));
                DrawItemSlot(currentChestInv[i], spot);
                if (Vector2.Distance(centerofspot, mousePosInInvTerms) < invTileWidth / 2)
                {
                    DrawSelectedSquare(spot);
                }
                if (Vector2.Distance(centerofspot, mousePosInInvTerms) < invTileWidth / 2)
                {
                    this.mousedOverSlot = currentChestInv[i];
                    mousedSlots++;
                }
            }
        }


        if (mousedSlots != 0)
        {
            isMouseCurrentlyOnSlot = true;
        } else
        {
            isMouseCurrentlyOnSlot = false;
        }
    }

    Ray placeray = new();
    RaycastHit placehit = new();
    float protectiveTimer = 0;
    bool machine = false;
    bool placed = false;
    Vector3 myblockpos;

    public bool isOtherInventoryOpen = false;
    void DoBlockPlaceStuff()
    {
        myblockpos.x = (int)transform.position.x;
        myblockpos.y = (int)transform.position.y;
        myblockpos.z = (int)transform.position.z;
        if(!draw.worldMachines.ContainsKey(dimension))
        {
            draw.worldMachines.Add(dimension, new Dictionary<Vector3, Drawer.MachineNode>());
        }
        if (!isInventoryOpen)
        {
            if (Input.GetMouseButtonDown(1)) //right click
            {
                if (myInv[selected].id == blockstore.gunItem.id)
                {
                    GameObject d = Instantiate(blockstore.bullet, this.transform.position, Quaternion.identity);
                    d.GetComponent<BulletScript>().direction = Camera.main.transform.forward;
                    d.GetComponent<BulletScript>().isOriginal = false;
                }
                placed = false;
                placeray.origin = (transform.position + transform.up) + (Camera.main.transform.forward * 0.5f);
                placeray.direction = Camera.main.transform.forward;
                Physics.Raycast(placeray, out placehit, 4f);
                while (placehit.collider == null)
                {
                    if (protectiveTimer < 2f)
                    {
                        protectiveTimer += Time.deltaTime;
                        placeray.origin += placeray.direction / 4;
                        Physics.Raycast(placeray, out placehit, 4f);
                    }
                    else
                    {
                        protectiveTimer = 0;
                        break;
                    }
                }
                machine = false;
                if (placehit.collider != null)
                {
                    protectiveTimer = 0;
                    Vector3 thing1 = placehit.point + (placeray.direction * .1f);
                    Vector3 machcheckspot = new Vector3(Mathf.FloorToInt(thing1.x), Mathf.FloorToInt(thing1.y), Mathf.FloorToInt(thing1.z));
                    bool placing = true;
                    if (draw.worldMachines[dimension].ContainsKey(machcheckspot))
                    {
                        if (draw.worldMachines[dimension][machcheckspot].id == blockstore.trunk.id)
                        {
                            placing = false;
                            isInventoryOpen = true;
                            isOtherInventoryOpen = true;
                            Cursor.lockState = CursorLockMode.None;
                            currentChestInv = draw.worldMachines[dimension][machcheckspot].linkedObject.GetComponent<TrunkScript>().myInv;
                        } else
                        if (draw.worldMachines[dimension][machcheckspot].id == blockstore.shaft.id)
                        {
                            SwitchDimension();
                            
                        }else
                        {
                            placing = true;
                        }
                    }
                    if (myInv[selected].id == blockstore.shaft.id)
                    {
                        placing = false;
                        RemoveBlock(Mathf.FloorToInt(thing1.x), Mathf.FloorToInt(thing1.y), Mathf.FloorToInt(thing1.z));
                        SetBlock(Mathf.FloorToInt(thing1.x), Mathf.FloorToInt(thing1.y), Mathf.FloorToInt(thing1.z), blockstore.shaft.id);
                        if (myInv[selected].amt > 1)
                            {
                                myInv[selected].amt--;

                            }
                            else
                            {
                                myInv[selected].amt = 0;
                                myInv[selected].id = 0;
                            }
                        
                    }
                    if(placing)
                    {
                        Vector3 thing = placehit.point - (placeray.direction * .1f);
                        if (blockstore.itemIDs.Contains(myInv[selected].id) || Input.GetKey(KeyCode.LeftShift) || myInv[selected].id == 0)
                        {
                            thing += (placeray.direction * .2f); //go into the block if its an item
                        }
                        Vector3 machspot = new Vector3(Mathf.FloorToInt(thing.x), Mathf.FloorToInt(thing.y), Mathf.FloorToInt(thing.z));

                        if (SetBlock((int)machspot.x, (int)machspot.y, (int)machspot.z, myInv[selected].id))
                        {
                            RecheckSurrounders((int)machspot.x, (int)machspot.y, (int)machspot.z);
                            if (myInv[selected].amt > 1)
                            {
                                myInv[selected].amt--;

                            }
                            else
                            {
                                myInv[selected].amt = 0;
                                myInv[selected].id = 0;
                            }
                        }
                        //Debug.Log(thing.x + " " + thing.y + " " + thing.z);
                    }
                }

            }

            if (Input.GetMouseButtonDown(0)) //left click
            {
                placeray.origin = (transform.position + transform.up) + (Camera.main.transform.forward * 0.5f);
                placeray.direction = Camera.main.transform.forward;
                Physics.Raycast(placeray, out placehit, 4f);
                while (placehit.collider == null)
                {
                    if (protectiveTimer < 2f)
                    {
                        protectiveTimer += Time.deltaTime;
                        placeray.origin += placeray.direction / 4;
                        Physics.Raycast(placeray, out placehit, 4f);
                    }
                    else
                    {
                        protectiveTimer = 0;
                        break;
                    }
                }
                if (placehit.collider != null)
                {
                    protectiveTimer = 0;
                    Vector3 thing = placehit.point + (placeray.direction * .1f);
                    if (placehit.collider.gameObject.tag == draw.tree.tag)
                    {
                        placehit.collider.gameObject.GetComponent<TreeScript>().FallOver();
                        //Destroy(placehit.collider.gameObject);
                    }
                    else
                    {
                        //Debug.Log("Is this");
                        BreakBlock(Mathf.FloorToInt(thing.x), Mathf.FloorToInt(thing.y), Mathf.FloorToInt(thing.z));
                        RecheckSurrounders((int)thing.x, (int)thing.y, (int)thing.z);
                    }
                }
            }
        } else
        {
            if(Input.GetMouseButtonDown(0)) //left click in inventory
            {
                if(isMouseCurrentlyOnSlot)
                {
                        int currentMouseThing = mouseSlot.id;
                        int currentMouseAmt = mouseSlot.amt;
                        mouseSlot.id = mousedOverSlot.id;
                        mouseSlot.amt = mousedOverSlot.amt;
                        mousedOverSlot.id = currentMouseThing;
                        mousedOverSlot.amt = currentMouseAmt;
                } else
                {
                    if(mouseSlot.id != 0)
                    {
                        PutDroppedItem(this.transform.position + (transform.forward * 3), blockstore.blocks[mouseSlot.id], mouseSlot.amt);
                        mouseSlot.id = 0;
                        mouseSlot.amt = 0;
                    }
                }
            }
            if (Input.GetMouseButtonDown(1)) //right click in inventory
            {

            }
        }
    }

    public ItemSlot mouseSlot = new();

    void RecheckSurrounders(int x, int y, int z)
    {
        Vector3 thing = new(x, y, z);
        Vector3[] vecs =
        {
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
        };
        foreach(Vector3 vec in vecs)
        {
            if(draw.worldMachines[dimension].ContainsKey(thing + vec))
            {
                if (draw.worldMachines[dimension][thing + vec].id == 8)
                {
                    draw.worldMachines[dimension][thing + vec].linkedObject.GetComponent<PipeMesher>().Remesh();
                }
            }
        }

    }

    void PutDroppedItem(Vector3 thing, Blocks.Block block, int amount)
    {
        if (block.id != 0)
        {
            GameObject di = Instantiate(droppedItem, thing + transform.up, Quaternion.identity);
            var drop = di.GetComponent<DroppedItem>();
            drop.sprite = sprites[block.id];
            drop.id = block.id;
            drop.SetTexture();
            drop.amount = amount;
        }
    }

    public GameObject droppedItem;

    void BreakBlock(int x, int y, int z)
    {
        bool ismachine = false;
        Vector3 thing = new(x, y, z);
        Blocks.Block block;
        if(draw.worldMachines[dimension].ContainsKey(thing))
        {
            ismachine = true;
            if (draw.worldMachines[dimension][thing].id == 6)
            {
                PutDroppedItem(thing, blockstore.gearblock, 2);
            }
            else if (draw.worldMachines[dimension][thing].id == 10)
            {
                var thisinv = draw.worldMachines[dimension][thing].linkedObject.GetComponent<TrunkScript>().myInv;
                PutDroppedItem(thing, blockstore.trunk, 1);
                for (int i = 0; i < thisinv.Count; i++)
                {
                    PutDroppedItem(thing + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), blockstore.blocks[thisinv[i].id], thisinv[i].amt);
                }
            }else
            {
                PutDroppedItem(thing, blockstore.blocks[draw.worldMachines[dimension][thing].id], 1);
            }
            Destroy(draw.worldMachines[dimension][thing].linkedObject);
            draw.worldMachines[dimension].Remove(thing);
        } else
        if (thing.y > 0)
        {
            if (thing.x < 0 && thing.z > 0)
            {
                block = draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(16 + (int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))];
                PutDroppedItem(thing, block, 1);
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(16 + (int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.air;
            }
            if (thing.x < 0 && thing.z < 0)
            {
                block = draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(16 + (int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))];
                PutDroppedItem(thing, block, 1);
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(16 + (int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))] = blockstore.air;
            }
            if (thing.x > 0 && thing.z < 0)
            {
                block = draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))];
                PutDroppedItem(thing, block, 1);
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))] = blockstore.air;
            }
            if (thing.x > 0 && thing.z > 0)
            {
                block = draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))];
                PutDroppedItem(thing, block, 1);
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.air;
            }
        }

        //block = blockstore.air;
        if (!ismachine)
        {
            draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
            if (thing.x > 0 && thing.z > 0)
            {
                if ((int)(thing.x % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.x % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.z % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.z % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
            }
            if (thing.x < 0 && thing.z < 0)
            {
                if (15 + (int)(thing.x % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.x % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.z % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.z % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
            }
            if (thing.x > 0 && thing.z < 0)
            {
                if ((int)(thing.x % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.x % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.z % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.z % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
            }
            if (thing.x < 0 && thing.z > 0)
            {
                if (15 + (int)(thing.x % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if (15 + (int)(thing.x % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.z % 16) == 15)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
                if ((int)(thing.z % 16) == 0)
                {
                    if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                    }
                }
            }
        }
        //Debug.Log(thing.x + " " + thing.y + " " + thing.z);
    }

    public bool AddToInventory(List<ItemSlot> inv, int type, int amount)
    {
        int slottoadd = -1;

        for(int i = 0; i < inv.Count; i++)
        {
            if (inv[i].id == 0 || inv[i].id == type)
            {
                slottoadd = i;
                break;
            } else
            {
                continue;
            }
        }
        if(slottoadd == -1)
        {
            return false;
        } else
        {
            
            inv[slottoadd].id = type;
            inv[slottoadd].amt += amount;
            return true;
        }

    }

    public bool AddToInventory(int type, int amount)
    {
        int slottoadd = -1;

        for (int i = 0; i < this.myInv.Count; i++)
        {
            if (this.myInv[i].id == 0 || this.myInv[i].id == type)
            {
                slottoadd = i;
                break;
            }
            else
            {
                continue;
            }
        }
        if (slottoadd == -1)
        {
            return false;
        }
        else
        {

            this.myInv[slottoadd].id = type;
            this.myInv[slottoadd].amt += amount;
            return true;
        }

    }

    bool letMeDown = false;
    void MovementStuff()
    {
        selected += (int)Input.mouseScrollDelta.y;
        selected = Mathf.Abs(selected) % 7;
        DoBlockPlaceStuff();

        
        if(Time.time < 20)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                letMeDown = true;
            }
        }


        if (Time.time > 20 || letMeDown)
        {
            if (!cc.isGrounded && this.transform.position.y >= 1)
            {
                timefalling += Time.deltaTime * 230;
                velocity.y -= Time.deltaTime * (15 + timefalling);
                cc.Move(velocity / 100);
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (myInv[selected].id != 0)
            {
                if(myInv[selected].amt > 1)
                {
                    myInv[selected].amt--;
                } else
                {
                    myInv[selected].amt = 0;
                    myInv[selected].id = 0;
                }
                PutDroppedItem(this.transform.position + (Camera.main.transform.forward*3), blockstore.blocks[myInv[selected].id], 1);
            }
        }

        if (velocity != Vector3.zero)
        {

            velocity -= velocity / 15;
            var thing = transform.position + transform.up;
            Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));

        }

        if (cc.isGrounded || this.transform.position.y <= 1.1)
        {
            timefalling = 0;
            velocity.y = Mathf.Clamp(velocity.y, 0, 10);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y += 20f;
            }
        }

        if (!isInventoryOpen)
        {
            rotation.x = -Input.GetAxis("Mouse Y");
            rotation.y = Input.GetAxis("Mouse X");
            rotation.z = 0;
            var rot = transform.rotation;
            var rot2 = Camera.main.transform.rotation;
            this.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y + rotation.y, rot.eulerAngles.z + 0);
            Camera.main.transform.rotation = Quaternion.Euler(rot2.eulerAngles.x + rotation.x, rot2.eulerAngles.y + rotation.y, rot2.eulerAngles.z + 0);



            if (Input.GetKey(KeyCode.W))
            {

                walkray.origin = this.transform.position;
                walkray.direction = this.transform.forward;
                Physics.Raycast(walkray, out walkhit, 1f);
                if (walkhit.collider == null)
                {
                    movement += (this.transform.forward * Input.GetAxis("Vertical")) / 4;
                }
                if (velocity == Vector3.zero)
                {
                    var thing = transform.position + transform.up;
                    walktime += Time.deltaTime;
                    Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));
                }
            }
            if (Input.GetKey(KeyCode.S))
            {

                walkrayback.origin = this.transform.position;
                walkrayback.direction = -1 * this.transform.forward;
                Physics.Raycast(walkrayback, out walkhitback, 1f);
                if (walkhitback.collider == null)
                {
                    movement += (this.transform.forward * Input.GetAxis("Vertical")) / 4;
                }
                if (velocity == Vector3.zero)
                {
                    var thing = transform.position + transform.up;
                    walktime += Time.deltaTime;
                    Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));
                }
            }
            if (Input.GetKey(KeyCode.A))
            {

                walkrayleft.origin = this.transform.position;
                walkrayleft.direction = -1 * this.transform.right;
                Physics.Raycast(walkrayleft, out walkhitleft, 1f);
                if (walkhitleft.collider == null)
                {
                    movement += (this.transform.right * Input.GetAxis("Horizontal")) / 4;
                }
                if (velocity == Vector3.zero)
                {
                    var thing = transform.position + transform.up;
                    walktime += Time.deltaTime;
                    Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));
                }
            }
            if (Input.GetKey(KeyCode.D))
            {

                walkrayright.origin = this.transform.position;
                walkrayright.direction = this.transform.right;
                Physics.Raycast(walkrayright, out walkhitright, 1f);
                if (walkhitright.collider == null)
                {
                    movement += (this.transform.right * Input.GetAxis("Horizontal")) / 4;

                }
                if (velocity == Vector3.zero)
                {
                    var thing = transform.position + transform.up;
                    walktime += Time.deltaTime;
                    Camera.main.transform.position = thing + this.transform.right * (Mathf.Sin(walktime * 8) / 8) + (this.transform.up * (Mathf.Sin(walktime * 4) / 8));
                }
            }
        }
        finalray.origin = transform.position;
        finalray.direction = Vector3.Normalize(movement);
        Physics.Raycast(finalray, out finalhit, 1f);
        if (finalhit.collider == null)
        {
            cc.Move((movement * Time.deltaTime) * 20);
        }
        else
        {
            movement = Vector3.zero;
        }
        movement /= 4;
    }



    private int selected = 0;
    float timefalling = 0;
    Vector3 rotation = new();
    Vector3 movement = new();
    public List<Vector2> neededChunks = new();
    void Update()
    {
        this.dimension = draw.dimension;
        if (draw.remeshQueue.Count > 50)
        {
            isLoading = true;

        } else
        {
            isLoading = false;
        }
        DoSelected();
        if (myInv[selected].id != 0)
        {
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[myInv[selected].id];
        } else
        {
            Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
        if (!isLoading)
        {
            MovementStuff();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (!this.isInventoryOpen)
            {
                this.isInventoryOpen = true;
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                this.isInventoryOpen = false;
                isOtherInventoryOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if ((int)transform.position.x > 0 && (int)transform.position.z > 0)
        {
            for (int i = ((int)transform.position.x - ((int)transform.position.x%16)) - (6 * 16); i < ((int)transform.position.x - ((int)transform.position.x % 16)) + (6 * 16); i++)
            {
                for (int j = ((int)transform.position.z - ((int)transform.position.z % 16)) - (6 * 16); j < ((int)transform.position.z - ((int)transform.position.z % 16)) + (6 * 16); j++)
                {
                    if (!draw.chunks.ContainsKey(new Vector3((int)(i / 16), 0, (int)(j / 16))))
                    {
                        if (!neededChunks.Contains(new Vector2((int)(i / 16), (int)(j / 16))))
                        {
                            neededChunks.Add(new Vector2((int)(i / 16), (int)(j / 16)));
                        }
                    }
                    /*else
                    {
                        if (draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isRebuilt == false && draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
                        {
                            draw.remeshQueue.Insert(0, draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))]);
                            draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
                        }
                    }*/

                }
            }
        }
        else
            if ((int)transform.position.x < 0 && (int)transform.position.z > 0)
        {

            for (int i = ((int)transform.position.x - (15 + (int)transform.position.x % 16)) - (6 * 16); i < ((int)transform.position.x - (15 + (int)transform.position.x % 16)) + (6 * 16); i++)
            {
                for (int j = ((int)transform.position.z - ((int)transform.position.z % 16)) - (6 * 16); j < ((int)transform.position.z - ((int)transform.position.z % 16)) + (6 * 16); j++)
                {
                    if (!draw.chunks.ContainsKey(new Vector3((int)(i / 16), 0, (int)(j / 16))))
                    {
                        if (!neededChunks.Contains(new Vector2((int)(i / 16), (int)(j / 16))))
                        {
                            neededChunks.Add(new Vector2((int)(i / 16), (int)(j / 16)));
                        }
                    }
                    /*else
                    {
                        if (draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isRebuilt == false && draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
                        {
                            draw.remeshQueue.Insert(0, draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))]);
                            draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
                        }
                    }*/

                }
            }
        }
        else
            if ((int)transform.position.x < 0 && (int)transform.position.z < 0)
        {
            for (int i = ((int)transform.position.x - (15 + (int)transform.position.x % 16)) - (6 * 16); i < ((int)transform.position.x - (15 + (int)transform.position.x % 16)) + (6 * 16); i++)
            {
                for (int j = ((int)transform.position.z - (15 + (int)transform.position.z % 16)) - (6 * 16); j < ((int)transform.position.z - (15 + (int)transform.position.z % 16)) + (6 * 16); j++)
                {
                    if (!draw.chunks.ContainsKey(new Vector3((int)(i / 16), 0, (int)(j / 16))))
                    {
                        if (!neededChunks.Contains(new Vector2((int)(i / 16), (int)(j / 16))))
                        {
                            neededChunks.Add(new Vector2((int)(i / 16), (int)(j / 16)));
                        }
                    }
                    /*else
                    {
                        if (draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isRebuilt == false && draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
                        {
                            draw.remeshQueue.Insert(0, draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))]);
                            draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
                        }
                    }*/

                }
            }

        }
        else if ((int)transform.position.x > 0 && (int)transform.position.z < 0)
        {
            for (int i = ((int)transform.position.x - ((int)transform.position.x % 16)) - (6 * 16); i < ((int)transform.position.x - ((int)transform.position.x % 16)) + (6 * 16); i++)
            {
                for (int j = ((int)transform.position.z - (15 + (int)transform.position.z % 16)) - (6 * 16); j < ((int)transform.position.z - (15 + (int)transform.position.z % 16)) + (6 * 16); j++)
                {
                    if (!draw.chunks.ContainsKey(new Vector3((int)(i / 16), 0, (int)(j / 16))))
                    {
                        if (!neededChunks.Contains(new Vector2((int)(i / 16), (int)(j / 16))))
                        {
                            neededChunks.Add(new Vector2((int)(i / 16), (int)(j / 16)));
                        }
                    }
                    /*else
                    {
                        if (draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isRebuilt == false && draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
                        {
                            draw.remeshQueue.Insert(0, draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))]);
                            draw.chunks[new Vector3((int)(i / 16), 0, (int)(j / 16))].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
                        }
                    }*/

                }
            }

        }
    }

    int colorIndex = 1;
    bool SetBlock(int x, int y, int z, int id)
    {
        Vector3 thing = new Vector3(x, y, z);
        Vector3 machspot = thing;
        bool machThatNeedsRemesh = false;
        if (!blockstore.modelIDs.Contains(id) && !blockstore.itemIDs.Contains(id) && myblockpos != machspot && id != 0)
        {
            machine = false;
            if (thing.x < 0 && thing.z > 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.blocks[id];
            }
            if (thing.x < 0 && thing.z < 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))] = blockstore.blocks[id];
            }
            if (thing.x > 0 && thing.z < 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))] = blockstore.blocks[id];
            }
            if (thing.x > 0 && thing.z > 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.blocks[id];
            }
        }
        else if (!blockstore.itemIDs.Contains(id) && blockstore.modelIDs.Contains(id))
        {
            machine = true;
            if (!draw.worldMachines[dimension].ContainsKey(machspot))
            {
                GameObject g = Instantiate(blockstore.IDtoModel[id], machspot, Quaternion.identity);
                Drawer.MachineNode thismach = new Drawer.MachineNode();
                thismach.id = id;
                thismach.linkedObject = g;
                draw.worldMachines[dimension].Add(machspot, thismach);
                if(id == 11)
                {
                    machThatNeedsRemesh = true;

                    if (thing.x < 0 && thing.z > 0)
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.blocks[id];
                    }
                    if (thing.x < 0 && thing.z < 0)
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))] = blockstore.blocks[id];
                    }
                    if (thing.x > 0 && thing.z < 0)
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))] = blockstore.blocks[id];
                    }
                    if (thing.x > 0 && thing.z > 0)
                    {
                        draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.blocks[id];
                    }
                }

            } else 
            {
                if (draw.worldMachines[dimension][machspot].id == blockstore.gearblock.id)
                {
                    if (id == 4)
                    {
                        Destroy(draw.worldMachines[dimension][machspot].linkedObject);
                        draw.worldMachines[dimension].Remove(machspot);
                        GameObject g = Instantiate(blockstore.IDtoModel[6], machspot, Quaternion.identity);
                        Drawer.MachineNode thismach = new Drawer.MachineNode();
                        thismach.id = 6;
                        thismach.linkedObject = g;
                        draw.worldMachines[dimension].Add(machspot, thismach);
                    }
                }
            }
            
            return true;
        }
        else if (blockstore.itemIDs.Contains(id))
        {
            if (id == blockstore.wrenchItem.id)
            {
                if (draw.worldMachines[dimension].ContainsKey(thing))
                {
                    if (draw.worldMachines[dimension][thing].id == 4)
                    {
                        var t = draw.worldMachines[dimension][thing].linkedObject.GetComponent<GearController>().rotationIndex;
                        draw.worldMachines[dimension][thing].linkedObject.GetComponent<GearController>().UpdateRotIndex((t + 1) % 6);
                    }
                    if (draw.worldMachines[dimension][thing].id == 6)
                    {
                        var t = draw.worldMachines[dimension][thing].linkedObject.GetComponent<DoubleGearController>().rotationIndex;
                        var t2 = draw.worldMachines[dimension][thing].linkedObject.GetComponent<DoubleGearController>().rotationIndex2;
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            draw.worldMachines[dimension][thing].linkedObject.GetComponent<DoubleGearController>().UpdateRot2Index((t2 + 1) % 6);
                        }
                        else
                        {
                            draw.worldMachines[dimension][thing].linkedObject.GetComponent<DoubleGearController>().UpdateRot1Index((t + 1) % 6);
                        }
                    }
                    if (draw.worldMachines[dimension][thing].id == 13)
                    {
                        var t = colorIndex;
                        draw.worldMachines[dimension][thing].linkedObject.transform.GetChild(0).GetChild(0).GetComponent<Light>().color = lampColors[((t + 1) % lampColors.Length)];
                        colorIndex = ((t + 1) % lampColors.Length);
                    }
                }
                
            }
            
            return false;
        }
        else if(id == 0)
        {
            if (draw.worldMachines[dimension].ContainsKey(machspot))
            {
                if (draw.worldMachines[dimension][machspot].id == blockstore.leverItem.id)
                {
                    draw.worldMachines[dimension][machspot].linkedObject.GetComponent<LeverWorker>().Toggle();
                }
            }
            return false;
        }

        if (!machine || machThatNeedsRemesh == true)
        {
            draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
            if ((int)(thing.x % 16) == 15)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            if ((int)(thing.x % 16) == 0)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            if ((int)(thing.z % 16) == 15)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            if ((int)(thing.z % 16) == 0)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            return true;
        }
        return false;
       
    }

    public void RemoveBlock(int x, int y, int z)
    {
        Vector3 thing = new Vector3(x, y, z);
        Vector3 machspot = thing;

            if (thing.x < 0 && thing.z > 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.air;
            }
            if (thing.x < 0 && thing.z < 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15 + (int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))] = blockstore.air;
            }
            if (thing.x > 0 && thing.z < 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, 16 + (int)(thing.z % 16))] = blockstore.air;
            }
            if (thing.x > 0 && thing.z > 0)
            {
                draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3((int)(thing.x % 16), (int)thing.y, (int)(thing.z % 16))] = blockstore.air;
            }


            draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
            if ((int)(thing.x % 16) == 15)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) + 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            if ((int)(thing.x % 16) == 0)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16) - 1, 0, Mathf.FloorToInt(thing.z / 16))].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            if ((int)(thing.z % 16) == 15)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) + 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            if ((int)(thing.z % 16) == 0)
            {
                if (draw.chunks.ContainsKey(new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)))
                {
                    draw.chunks[new Vector3(Mathf.FloorToInt(thing.x / 16), 0, Mathf.FloorToInt(thing.z / 16) - 1)].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
        }


}
