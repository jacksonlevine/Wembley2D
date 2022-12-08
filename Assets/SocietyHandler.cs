using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocietyHandler : MonoBehaviour
{

    public Blocks blockstore;
    public List<buil> builds = new();
    public Drawer draw;
    public PlayerScript player;

    public struct buil
    {
        public List<int[]> build;
        public int width;
        public int zwidth;
    }




    public buil mine;

    // Start is called before the first frame update
    void Start()
    {

    }

    buil GenerateRandomHouse()
    {
        int width = Random.Range(16, 32);
        int zwidth = Random.Range(16, 32);

        int[] ints= new int[width*zwidth];

        int x1 = Mathf.Clamp(Random.Range(0, width/10), 1, width);
        int x2 = Mathf.Clamp(x1 + Random.Range(5, 8), 1, width);

        int z1 = Mathf.Clamp(Random.Range(0, zwidth / 6), 1, zwidth);
        int z2 = Mathf.Clamp(z1 + Random.Range(5, 10), 1, zwidth);

        Blocks.Block[] blocks =
        {
            blockstore.wood,
            blockstore.stone,
        };

        var block = blocks[Random.Range(0, blocks.Length)];

        for(int i = 0; i < width; i++)
        {
            for(int k = 0; k < zwidth; k++)
            {
                ints[(k * width) + i] = 0;
                if (i >= x1 && i <= x2 && k >= z1 && k <= z2)
                {
                    ints[(k * width) + i] = block.id;
                }
            }
        }

        int x11 = Mathf.Clamp(x2+ Random.Range(0, width / 2), 1, width);
        int x22 = Mathf.Clamp(x11 + Random.Range(5, 8), 1, width);

        int z11 = Mathf.Clamp(z2+ Random.Range(0, zwidth / 4), 1, zwidth);
        int z22 = Mathf.Clamp(z11 +  Random.Range(5, 10), 1, zwidth);

        var block2 = blocks[Random.Range(0, blocks.Length)];
        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < zwidth; k++)
            {
                ints[(k * width) + i] = 0;
                if (i >= x11 && i <= x22 && k >= z11 && k <= z22)
                {
                    ints[(k * width) + i] = block2.id;
                }

            }
        }
        int[] secondints = new int[width*zwidth];
        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < zwidth; k++)
            {
                secondints[(k * width) + i] = 0;
                if (ints[(k*width)+ i] != 0)
                {
                    if(ints[Mathf.Clamp((k * width) + i - 1, 0, ints.Length-1)] == 0 || ints[Mathf.Clamp((k * width) + i + 1, 0, ints.Length-1)] == 0
                        || ints[Mathf.Clamp((k * width) + i -width, 0, ints.Length - 1)] == 0 || ints[Mathf.Clamp((k * width) + i + width, 0, ints.Length - 1)] == 0)
                    {
                        secondints[(k * width) + i] = ints[(k * width) + i];
                    }
                }

            }
        }
        int[] secondintslights = new int[width * zwidth];

        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < zwidth; k++)
            {

                secondintslights[(k * width) + i] = secondints[(k * width) + i];
            }
        }

        for (int i = 0; i < width; i++)
        {
            secondints[x1 + 1 * width + i] = 0;
            secondints[x11 + 1 * width + i] = 0;
            if(Random.Range(0, 6) == 4)
            {
                secondintslights[x1 + 1 * width + i] = blockstore.lanternItem.id;
                secondintslights[x11 + 1 * width + i] = blockstore.lanternItem.id;
            }
        }
        
        List<int[]> buil = new();

        buil.Add(ints);
        buil.Add(secondints);
        buil.Add(secondints);
        buil.Add(secondints);
        buil.Add(secondints);
        buil.Add(secondintslights);
        buil.Add(ints);

        buil test = new();
        test.width = width;
        test.zwidth = zwidth;
        test.build = buil;

        return test;
    }



    public void BuildBuild(int i, int k, Dictionary<Vector3, Blocks.Block> thechunk, int dimension, int chunkx, int chunkz)
    {
        int g = blockstore.block.id;
        int m = blockstore.shaft.id;
        int t = blockstore.lanternItem.id;
        int st = blockstore.stone.id;
        int w = blockstore.stone.id;


        buil mine = new buil();
        //builds.Add(GenerateRandomHouse());

        int[] layer0 =
        {
            g, g, g, g,   g, g, g, g,
            g, m ,m, g,   g, g, g, g,
            g, st,st,g,   g, g, g, g,
            g, st,st,g,   g, g, m, g,
            g, g, st,g,   g, g, g, g,
            g, g, g, g,   g, g, g, g,
        };
        mine.build = new List<int[]>();
        mine.build.Add(layer0);

        int[] layer1 =
        {
            st, st, st, st, 0, 0, 0, 0,
            st, 0,  0,  st, 0, 0, 0, 0,
            st, 0, 0,   st, 0, st,st,st,
            st, 0, 0,   st, 0, st, 0, st,
            st, st,0 , st,  0, st, 0, st,
            0, 0, 0, 0,     0, st, 0, st,
        };
        int[] layer1lights =
        {
            st, st, st, st, 0, 0, 0, 0,
            st, t,  0,  st, 0, 0, t, 0,
            st, 0, 0,   st, 0, st,st,st,
            st, 0, 0,   st, 0, st, 0, st,
            st, st,0 , st,  0, st, 0, st,
            t, 0, 0,   t,    0, st, t, st,
        };
        mine.build.Add(layer1);
        mine.build.Add(layer1);
        mine.build.Add(layer1);
        mine.build.Add(layer1);
        mine.build.Add(layer1);
        mine.build.Add(layer1);
        mine.build.Add(layer1);
        mine.build.Add(layer1);
        mine.build.Add(layer1lights);

        int[] layer2 =
        {
            st, st, st, st, 0, 0, 0, 0,
            st, st,  st,st, 0, 0, 0, 0,
            st, st, st, st, 0, st,st,st,
            st, st, st, st, 0, st, st, st,
            st, st,st , st,  0, st, st, st,
            0, 0, 0, 0,      0, st, st, st, 
        };
        mine.build.Add(layer2);
        mine.width = 8;
        mine.zwidth = 6;
        builds.Add(mine);

        buil farm = new buil();
        farm.build = new List<int[]>();
        int c = blockstore.trunk.id;
        int[] farmlay1 =
        {
            w, w, w, w,  w, w, w, w,
            w, st,st,st, st,st,st,w,
            w,st, st,st, st,st,st,w,
            w,st,st,st,  st,st,st,w,
            w,st,st,st,  st,st,st,w,
            w,st,st,st, st,st,st,w,
            w,st,st,st, st,st,st,w,
            w,w, w, w,  w, w, w, w,
            g, g, g, g, g, g, g, g,
        };
        int[] farmlay2a =
        {
            w, w, w, w,  w, w, w, w,
            w, 0,c,c,     0,c,c,w,
            w,0, 0,0,    0,0,0,w,
            w, 0,0,0,     0,0,0,w,
            w,w, 0, w,  w, w, w, w,
            0, w, 0, w, 0, 0, 0, 0,
            0, w, 0, w, 0, 0, 0, 0,
            0, w, 0, w, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] farmlay2 =
        {
            w, w, w, w,  w, w, w, w,
            w, 0,0,0,     0,0,0,w,
            w,0, 0,0,    0,0,0,w,
            w, 0,0,0,     0,0,0,w,
            w,w, 0, w,  w, w, w, w,
            0, w, 0, w, 0, 0, 0, 0,
            0, w, 0, w, 0, 0, 0, 0,
            0, w, 0, w, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] farmlay3 =
        {
            w, w, w, w,  w, w, w, w,
            w, 0,0,0,     0,0,0,w,
            w,0, 0,0,    0,0,0,w,
            w, 0,0,0,     0,0,0,w,
            w,w, 0, w,  w, w, w, w,
            0, w, 0, w, 0, 0, t, 0,
            0, w, 0, w, 0, 0, 0, 0,
            0, w, w, w, 0, 0, 0, 0,
            0, t, 0, t, 0, 0, 0, 0,
        };
        int[] farmlay4 =
        {
            0, 0, 0, 0,  0, 0, 0, 0,
            0, w,w,w,     w,w,w,0,
            0,w, w,w,    w,w,w,0,
            0, w,w,w,     w,w,w,0,
            0,0, w, 0,  0, 0, 0, 0,
            0, 0, w, 0, 0, 0, 0, 0,
            0, 0, w, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] farmlay5 =
        {
            0, 0, 0, 0,  0, 0, 0, 0,
            0, 0,0,0,     0,0, 0,0,
            0,0, w,w,    w,w,0,0,
            0, 0,0,0,     0,0,0,0,
            0,0, 0, 0,  0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
        };
        farm.build.Add(farmlay1);
        farm.build.Add(farmlay2a);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay2);
        farm.build.Add(farmlay3);
        farm.build.Add(farmlay4);
        farm.build.Add(farmlay5);
        farm.width = 8;
        farm.zwidth = 9;
        builds.Add(farm);
        Vector3 position = new Vector3(i, (Mathf.PerlinNoise((float)((chunkx * 16) + i) / 30f, (float)((chunkz * 16) + k) / 30f) * 16), k);


        int or = Random.Range(0, 2);

        var build = builds[Random.Range(0, builds.Count)];

        for (int z = (int)position.y; z < (int)position.y + build.build.Count; z++)
            {
                for (int j = 0; j < build.width; j++)
                {
                    for (int s = 0; s < build.zwidth; s++)
                    {
                    
                    var id = build.build[z - (int)position.y][(s * build.width) + j];
                    if(or == 1)
                    {
                        id = build.build[z - (int)position.y][(build.width * build.zwidth) - 1 - ((s * build.width) + j)];
                    }

                    if (blockstore.modelIDs.Contains(id))
                    {
                        Vector3 position2 = new Vector3((chunkx * 16) + i + j, z, (chunkz * 16) + k + s);
                        GameObject ga = Instantiate(blockstore.IDtoModel[id], position2, Quaternion.identity);
                        Drawer.MachineNode thismach = new Drawer.MachineNode();
                        thismach.id = id;
                        thismach.linkedObject = ga;
                        if (draw.worldMachines.ContainsKey(dimension))
                        {
                            if (!draw.worldMachines[dimension].ContainsKey(position2))
                            {
                                draw.worldMachines[dimension].Add(position2, thismach);
                            }
                        } else
                        {
                            draw.worldMachines.Add(dimension, new Dictionary<Vector3, Drawer.MachineNode>());
                            draw.worldMachines[dimension].Add(position2, thismach);
                        }
                    }
                    else
                    {
                        if (thechunk.ContainsKey(new Vector3((int)position.x + j, z, (int)position.z + s)))
                        {
                            thechunk[new Vector3((int)position.x + j, z, (int)position.z + s)] = blockstore.blocks[id];
                        }
                        else
                        {
                            thechunk.Add(new Vector3((int)position.x + j, z, (int)position.z + s), blockstore.blocks[id]);
                        }
                    }
                        //player.SetBlock((int)position.x + j, (int)position.y + z, (int)position.z + s, build.build[z - (int)position.y][s * build.width + j]);
                    }
                }
            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
