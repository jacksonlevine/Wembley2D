using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public GameObject chunk;
    public Dictionary<Vector3, GameObject> chunks = new();
    public Blocks blockstore;

    public Transform player;


    public Material grassMaterial1;


    public int dimension = 0;

    public Dictionary<int, ComputeBuffer> bufferArgs = new();
    public Dictionary<int, ComputeBuffer> meshPropsBuffers = new();

    public Mesh grassmesh;

    public ComputeShader compute;
    public Transform pusher;

    private struct MeshProperties
    {
        public Matrix4x4 mat;
        public Vector4 color;
        public Vector3 uv;

        public static int Size()
        {
            return
                sizeof(float) * 4 * 4 + // matrix;
                sizeof(float) * 4 +     // color;
                sizeof(float) * 3;   //uv disabled

        }
    }

    public List<GameObject> remeshQueue = new();
    private float remeshTimer = 0;
    public Dictionary<int, Dictionary<Vector2, Dictionary<Vector3, Blocks.Block>>> worldAllChunks = new();





    public Dictionary<int, Dictionary<Vector3, MachineNode>> worldMachines = new();



    public Dictionary<Vector3, int> worldFoliage = new();
    public Dictionary<int, Mesh> foliageDict = new();
    public Dictionary<int, Material> foliageMats = new();
    
    public struct MachineNode
    {
        public int id;
        public GameObject linkedObject;
        public bool active;
        public float level;
    }
    public GameObject tree;
    // Start is called before the first frame update
    int LoadRadius = 70;
    Dictionary<int, Dictionary<Vector3, int>> foliagePoses = new();

    Dictionary<int, List<Matrix4x4>> mats = new();

    int index = 0;
    public void InitializeFoliageBuffers()
    {
        int kernel = compute.FindKernel("CSMain");
        index = 0;
        //mats.Clear();
        bufferArgs.Clear();
        meshPropsBuffers.Clear();
        foliagePoses.Clear();

        Vector3 vec;
        for(float i = player.position.x - LoadRadius; i < player.position.x + LoadRadius; i++)
        {
            for(float j = player.position.z - LoadRadius; j < player.position.z + LoadRadius; j++) {
                for (float k = player.position.y - 5; k < player.position.y + 5; k++)
                {
                    vec = Vector3Int.FloorToInt(new Vector3(i, k, j));
                    if (worldFoliage.ContainsKey(vec))
                    {
                        for (int x = 0; x < 6; x++)
                        {
                            if (!foliagePoses.ContainsKey(x))
                            {
                                foliagePoses.Add(x, new Dictionary<Vector3, int>());
                            }
                            if (worldFoliage[vec] == x)
                            {
                                foliagePoses[x].Add(vec, x);
                                
                            }
                        }
                    }
                }
            }
        }

        for(int i = 0; i < foliagePoses.Count; i++)
        {
            if (foliagePoses[i].Count > 0)
            {
                //BUFFER ARGS

                uint[] onearg = new uint[5] { 0, 0, 0, 0, 0 };
                onearg[0] = (uint)foliageDict[i].GetIndexCount(0);
                onearg[1] = (uint)foliagePoses[i].Count;
                ComputeBuffer oneBuffer = new ComputeBuffer(foliagePoses[i].Count, onearg.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
                System.GC.SuppressFinalize(oneBuffer);
                oneBuffer.SetData(onearg);
                bufferArgs.Add(i, oneBuffer);


                //MESH PROPERTIES
                index = 0;
                    MeshProperties[] properties = new MeshProperties[foliagePoses[i].Count];
                    foreach(KeyValuePair<Vector3, int> keyv in foliagePoses[i])
                    {
                        MeshProperties props = new();
                        props.uv[0] = 0;
                        props.color = Color.white;
                        props.mat.SetTRS(keyv.Key, Quaternion.identity, Vector3.one);
                        properties[index] = props;
                        index++;
                    }
                    ComputeBuffer meshPropertieBuffer = new ComputeBuffer(foliagePoses[i].Count, MeshProperties.Size());
                    System.GC.SuppressFinalize(meshPropertieBuffer);
                    meshPropertieBuffer.SetData(properties);
                    compute.SetBuffer(kernel, "_Properties", meshPropertieBuffer);
                    meshPropsBuffers.Add(i, meshPropertieBuffer);
                    foliageMats[i].SetBuffer("_Properties", meshPropertieBuffer);
            }
        }
    }



    public static float Perlin3D(float x, float y, float z, float scale)
    {
        float XY = Mathf.PerlinNoise(x, y);
        float YZ = Mathf.PerlinNoise(y, z);
        float ZX = Mathf.PerlinNoise(z, x);

        float YX = Mathf.PerlinNoise(y, z);
        float ZY = Mathf.PerlinNoise(z, y);
        float XZ = Mathf.PerlinNoise(x, z);

        float val = (XY + YZ + ZX + YX + ZY + XZ) / 6f;
        return val * scale;
    }
    Bounds bounds;
    public Vector3 thisPosition = new();
    void Start()
    {
        bounds = new Bounds(Vector3.zero, Vector3.one*int.MaxValue);

        grassmesh = new Mesh();
        Vector3[] verts = {
            new(1, 0, 0),
            new(0, 0, 0),
            new(0, 1, 0),
            new(1, 1, 0),
            new(0.5f, 0, -0.5f),
            new(0.5f, 1, -0.5f),
            new(0.5f, 1, 0.5f),
            new(0.5f, 0, 0.5f),
        };
        Vector2[] uvs =
        {
            new(1, 0),
            new(0, 0),
            new(0, 1),
            new(1, 1),
            new(1, 0),
            new(0, 0),
            new(0, 1),
            new(1, 1),
        };
        grassmesh.bounds = bounds;
        grassmesh.SetVertices(verts);
        int[] tris =
        {
            1, 2, 3, 1, 3, 0,
            0, 3, 2, 0, 2, 1,
            4, 5, 6, 4, 6, 7,
            7, 6, 5, 7, 5, 4,
        };
        grassmesh.SetTriangles(tris, 0);
        grassmesh.RecalculateNormals();
        
        grassmesh.RecalculateTangents();
        grassmesh.OptimizeReorderVertexBuffer();
        grassmesh.uv = uvs;
        foliageDict.Add(0, grassmesh);
        foliageMats.Add(0, grassMaterial1);

        thisPosition.x = player.position.x;
        thisPosition.y = player.position.y;
        thisPosition.z = player.position.z;

        InitializeFoliageBuffers();

        for (int i = 0; i < 17; i++)
        {
            for(int j = 0; j < 17; j++)
            {
                //GenerateNewWorldDataChunk(i, j);
                GameObject c = Instantiate(chunk, Vector3.zero + new Vector3(i * 16, 0, j * 16), Quaternion.identity);
                chunks.Add(new Vector3(i, 0, j), c);
            }
        }

       for(int i = 0; i < 40; i++)
        {
            for (int j = 0; j < 40; j++)
            {
                if(Random.Range(0, 20) < 2)
                {
                    GameObject t = Instantiate(tree, new Vector3(i*20, Random.Range(-20, -10), j*20), Quaternion.identity);
                    t.SetActive(true);
                }
            }
        }
        

    }

    private float rtime = 0.015f;
    void remesh()
     {

        if(remeshQueue.Count > 2)
        {
            if(remeshTimer > rtime)
            {
                /*if (Vector3.Distance(player.position, remeshQueue[0].transform.position) < 128)
                {*/
                    remeshQueue[0].GetComponent<ChunkTerrain>().RebuildMesh();
                    remeshQueue[0].GetComponent<ChunkTerrain>().isInRemeshQueue = false;
                    remeshQueue.RemoveAt(0);
                    remeshTimer = 0;
                /*} else
                {
                    var t = remeshQueue[0];
                    remeshQueue.Add(t);
                    remeshQueue.RemoveAt(0); //move it to the end 
                    remeshTimer = 0;
                }*/
                
            } else
            {
                remeshTimer += Time.deltaTime;
            }
        }
    }

    Vector2 cp = new();
    Vector3 vec = new();
    public void GenerateNewWorldDataChunk(int x, int z, int dimen)
    {
        cp.x = x;
        cp.y = z;
        
        var thechunk = new Dictionary<Vector3, Blocks.Block>();

        /*for (int i = 0; i < 16; i++)
        {
            for (int k = 0; k < 16; k++)
            {
                vec.x = i;
                vec.z = k;
                for (int y = 0; y < 30; y++)
                {
                    vec.y = y;
                    thechunk.Add(vec, blockstore.stone);
                }
                var perl = Mathf.PerlinNoise(((x * 16) + vec.x) / 40f, ((z * 16) + vec.z) / 40f);
                    
                for (int h = 0; h < 10 * perl; h++)
                {
                    vec.y = 30 + h;
                    thechunk.Add(vec, blockstore.block);
                    vec.y = 29 + h;
                    thechunk[vec] = blockstore.stone;
                }

            }
        }*/
        if (dimen == 0)
        {
            for (int i = 0; i < 16; i++)
            {

                for (int k = 0; k < 16; k++)
                {
                    for (int j = 0; j < 65; j++)
                    {
                        vec.x = i;
                        vec.y = j;
                        vec.z = k;
                        if (j < (Mathf.PerlinNoise((float)((x * 16) + vec.x) / 30f, (float)((z * 16) + vec.z) / 30f) * 2))
                        {
                            if (thechunk.ContainsKey(vec))
                            {
                                thechunk[vec] = blockstore.block;
                            }
                            else
                            {
                                thechunk.Add(vec, blockstore.block);
                            }
                            vec.y = j - 1;
                            if (thechunk.ContainsKey(vec))
                            {
                                thechunk[vec] = blockstore.dirt;
                            }
                            else
                            {
                                thechunk.Add(vec, blockstore.dirt);
                            }
                            vec.y += 2;
                            if (Random.Range(0, 20) == 2) 
                            {
                                Vector3 thing = new Vector3((x * 16) + vec.x, vec.y, (z * 16) + vec.z);
                                if (!worldFoliage.ContainsKey(thing))
                                {
                                    worldFoliage.Add(thing, 0);
                                }
                            };
                        }
                        else
                        {
                            thechunk.Add(vec, blockstore.air);
                        }

                    }
                }
            }
        } else
        {
            for (int i = 0; i < 16; i++)
            {

                for (int k = 0; k < 16; k++)
                {
                    for (int j = 0; j < 65; j++)
                    {
                        vec.x = i;
                        vec.y = j;
                        vec.z = k;
                        if (j < (Mathf.PerlinNoise((dimension*100)+(float)((x * 16) + vec.x) / 3f, (dimension * 100) + (float)((z * 16) + vec.z) / 3f) * 4))
                        {
                            if (thechunk.ContainsKey(vec))
                            {
                                thechunk[vec] = blockstore.stone;
                            }
                            else
                            {
                                thechunk.Add(vec, blockstore.stone);
                            }
                            vec.y = j - 1;
                            if (thechunk.ContainsKey(vec))
                            {
                                thechunk[vec] = blockstore.dirt;
                            }
                            else
                            {
                                thechunk.Add(vec, blockstore.dirt);
                            }
                        }
                        else
                        {
                            thechunk.Add(vec, blockstore.air);
                        }

                    }
                }
            }
        }
        if (this.worldAllChunks.ContainsKey(dimen))
        {
            this.worldAllChunks[dimen].Add(cp, thechunk);
        } else
        {
            this.worldAllChunks.Add(dimen, new Dictionary<Vector2, Dictionary<Vector3, Blocks.Block>>());
            this.worldAllChunks[dimen].Add(cp, thechunk);
        }
    }
    bool thisdone = false;

    public Camera mainCam;
    // Update is called once per frame
    void Update()
    {
        int kernel = compute.FindKernel("CSMain");

        compute.SetVector("_PusherPosition", pusher.position);
// We used to just be able to use `population` here, but it looks like a Unity update imposed a thread limit (65535) on my device.
// This is probably for the best, but we have to do some more calculation.  Divide population by numthreads.x (declared in compute shader).
        
        foreach(KeyValuePair<int, ComputeBuffer> buff in bufferArgs)
        {
            if(buff.Value.count > 0)
            {
                compute.Dispatch(kernel, bufferArgs[buff.Key].count, 1, 1);
                Graphics.DrawMeshInstancedIndirect(foliageDict[buff.Key], 0, foliageMats[buff.Key], bounds, buff.Value, 0, null, UnityEngine.Rendering.ShadowCastingMode.Off, false, 0, Camera.main, UnityEngine.Rendering.LightProbeUsage.BlendProbes);
            }
        }


        if(Vector3.Distance(thisPosition, player.position) > 20)
        {
            thisPosition.x = player.position.x;
            thisPosition.y = player.position.y;
            thisPosition.z = player.position.z;
            InitializeFoliageBuffers();
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            InitializeFoliageBuffers();
        }

        remesh();


        /*if(Time.time > 0.5f && !thisdone)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    chunks[new Vector3(i, 0, j)].GetComponent<ChunkTerrain>().RebuildMesh();
                }
            }
            thisdone = true;
        }*/
    }
}
