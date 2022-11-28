using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTerrain : MonoBehaviour
{
    public Blocks blockstore;
    public Dictionary<Vector3, Blocks.Block> thechunk = new();
    public List<Vector3> vertices = new();
    public List<int> triangles = new();
    public List<Vector2> uvs = new();
    public Drawer draw;

    public int dimension = 0;
    Vector3 vec = new();

    public GameObject player;

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
    void Start()
    {
        OnInstantiate();
        UpdateSurrounders();
    }
    public bool isRebuilt = false;
    public void UpdateSurrounders()
    {
        var chunkspot = new Vector3((int)((int)this.transform.position.x / (int)16), 0, (int)((int)this.transform.position.z / (int)16));
        if (draw.chunks.ContainsKey(chunkspot + new Vector3(0, 0, -1)))
        {
            if (draw.worldAllChunks[dimension].ContainsKey(new Vector2(chunkspot.x, chunkspot.z - 1)) && draw.chunks[chunkspot + new Vector3(0, 0, -1)].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
            {
                draw.remeshQueue.Add(draw.chunks[chunkspot + new Vector3(0, 0, -1)]);
                draw.chunks[chunkspot + new Vector3(0, 0, -1)].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
            }
        }
        if (draw.chunks.ContainsKey(chunkspot + new Vector3(0, 0, 1)))
        {
            if (draw.worldAllChunks[dimension].ContainsKey(new Vector2(chunkspot.x, chunkspot.z + 1)) && draw.chunks[chunkspot + new Vector3(0, 0, 1)].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
            {
                draw.remeshQueue.Add(draw.chunks[chunkspot + new Vector3(0, 0, 1)]);
                draw.chunks[chunkspot + new Vector3(0, 0, 1)].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
            }
        }
        if (draw.chunks.ContainsKey(chunkspot + new Vector3(-1, 0, 0)))
        {
            if (draw.worldAllChunks[dimension].ContainsKey(new Vector2(chunkspot.x - 1, chunkspot.z)) && draw.chunks[chunkspot + new Vector3(-1, 0, 0)].GetComponent<ChunkTerrain>().isInRemeshQueue == false)
            {
                draw.remeshQueue.Add(draw.chunks[chunkspot + new Vector3(-1, 0, 0)]);
                draw.chunks[chunkspot + new Vector3(-1, 0, 0)].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
            }
        }
        if (draw.chunks.ContainsKey(chunkspot + new Vector3(1, 0, 0)))
        {
            if (draw.worldAllChunks[dimension].ContainsKey(new Vector2(chunkspot.x + 1, chunkspot.z)) && draw.chunks[chunkspot + new Vector3(1, 0, 0)].GetComponent<ChunkTerrain>().isInRemeshQueue  == false)
            {
                draw.remeshQueue.Add(draw.chunks[chunkspot + new Vector3(1, 0, 0)]);
                draw.chunks[chunkspot + new Vector3(1, 0, 0)].GetComponent<ChunkTerrain>().isInRemeshQueue = true;
            }
        }
    }
    public bool isInRemeshQueue = false;
    public void OnInstantiate()
    {
        this.dimension = draw.dimension;
        //set the chunk data to the worldallchunks world data
        if (!draw.worldAllChunks.ContainsKey(dimension))
        {
            draw.worldAllChunks.Add(dimension, new Dictionary<Vector2, Dictionary<Vector3, Blocks.Block>>());
            if (draw.worldAllChunks[dimension].ContainsKey(new Vector2((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16))))
            {
                this.thechunk = draw.worldAllChunks[dimension][new Vector2((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16))];
                //Debug.Log("Adding chunk with " + draw.worldAllChunks[dimension][new Vector2((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16))].Count + " things");

            }
            else
            { //generate new data if needed
                draw.GenerateNewWorldDataChunk((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16), dimension);
                this.thechunk = draw.worldAllChunks[dimension][new Vector2((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16))];
            }
        }
        else
        {
            if (draw.worldAllChunks[dimension].ContainsKey(new Vector2((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16))))
            {
                this.thechunk = draw.worldAllChunks[dimension][new Vector2((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16))];
                //Debug.Log("Adding chunk with " + draw.worldAllChunks[dimension][new Vector2((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16))].Count + " things");

            }
            else
            { //generate new data if needed
                draw.GenerateNewWorldDataChunk((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16), dimension);
                this.thechunk = draw.worldAllChunks[dimension][new Vector2((int)((int)this.transform.position.x / (int)16), (int)((int)this.transform.position.z / (int)16))];
            }
        }
        //RebuildMesh();
        if (!draw.remeshQueue.Contains(this.gameObject) && !this.isInRemeshQueue)
        {
            draw.remeshQueue.Insert(0, this.gameObject);
            this.isInRemeshQueue = true;
        }
        this.isRebuilt = false;
    }
    private float uvwidth = 0.0625f;
    private float onepixel = 0.0034722222222222f;
    public List<CombineEgg> combs = new();
    Vector3 vec2;
    Vector3 vec3;
    public struct CombineEgg
    {
        public Vector3 localPosition;
        public Mesh mesh;
    }

    public void RebuildMesh()
    {
        //Debug.Log("Rebuilding Mesh");
        combs.Clear();
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();
        Mesh mesh = new Mesh();
        
        for (int i = 0; i < 16; i++)
        {
            for(int j= 0; j < 64; j++)
            {
                for (int k = 0; k < 16; k++)
                {
                    vec2.x = i;
                    vec2.y = j;
                    vec2.z = k;
                    vec3.x = i;
                    vec3.y = j;
                    vec3.z = k;
                    var thing = new Vector3((int)this.transform.position.x + i, j, (int)this.transform.position.z + k);
                    if (draw.worldFoliage.ContainsKey(thing))
                    {
                        CombineEgg comby = new();

                        var meshy = draw.foliageDict[draw.worldFoliage[thing]];
                        comby.mesh = meshy;
                        comby.localPosition = vec2;
                        combs.Add(comby);
                    }
                    if (thechunk.ContainsKey(vec2))
                    {
                        if (thechunk[vec2].id != 0 && !blockstore.modelIDs.Contains(thechunk[vec2].id))
                        {
                            vec2.y -= 1;
                            if (thechunk.ContainsKey(vec2))
                            {
                                if (thechunk[vec2].id == 0 || blockstore.modelIDs.Contains(thechunk[vec2].id))
                                {
                                    triangles.Add(vertices.Count + 1);
                                    triangles.Add(vertices.Count + 0);
                                    triangles.Add(vertices.Count + 3);
                                    triangles.Add(vertices.Count + 2);
                                    triangles.Add(vertices.Count + 1);
                                    triangles.Add(vertices.Count + 3);
                                    vertices.Add(new Vector3(i + 0, j + 0, k + 0));
                                    vertices.Add(new Vector3(i + 0, j + 0, k + 1));
                                    vertices.Add(new Vector3(i + 1, j + 0, k + 1));
                                    vertices.Add(new Vector3(i + 1, j + 0, k + 0));// bottom face
                                    uvs.Add(new Vector2(thechunk[vec3].bottomTex.x + 0 + onepixel, thechunk[vec3].bottomTex.y + uvwidth - onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].bottomTex.x + 0 + onepixel, thechunk[vec3].bottomTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].bottomTex.x + uvwidth - onepixel, thechunk[vec3].bottomTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].bottomTex.x + uvwidth - onepixel, thechunk[vec3].bottomTex.y + uvwidth - onepixel));


                                }
                            }
                            vec2.y += 2;
                            if (thechunk.ContainsKey(vec2))
                            {
                                if (thechunk[vec2].id == 0 || blockstore.modelIDs.Contains(thechunk[vec2].id))
                                {
                                    triangles.Add(vertices.Count + 0);
                                    triangles.Add(vertices.Count + 1);
                                    triangles.Add(vertices.Count + 2);
                                    triangles.Add(vertices.Count + 0);
                                    triangles.Add(vertices.Count + 2);
                                    triangles.Add(vertices.Count + 3);
                                    vertices.Add(new Vector3(i + 0, j + 1, k + 0));
                                    vertices.Add(new Vector3(i + 0, j + 1, k + 1));
                                    vertices.Add(new Vector3(i + 1, j + 1, k + 1));
                                    vertices.Add(new Vector3(i + 1, j + 1, k + 0));// top face
                                    uvs.Add(new Vector2(thechunk[vec3].topTex.x + 0 + onepixel, thechunk[vec3].topTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].topTex.x + 0 + onepixel, thechunk[vec3].topTex.y + uvwidth - onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].topTex.x + uvwidth - onepixel, thechunk[vec3].topTex.y + uvwidth - onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].topTex.x + uvwidth - onepixel, thechunk[vec3].topTex.y + 0 + onepixel));

                                }
                            }
                            vec2.y = j;
                            vec2.x += 1;
                            if (thechunk.ContainsKey(vec2))
                            {
                                if (thechunk[vec2].id == 0 || blockstore.modelIDs.Contains(thechunk[vec2].id))
                                {
                                    triangles.Add(vertices.Count + 0);
                                    triangles.Add(vertices.Count + 3);
                                    triangles.Add(vertices.Count + 2);
                                    triangles.Add(vertices.Count + 0);
                                    triangles.Add(vertices.Count + 2);
                                    triangles.Add(vertices.Count + 1);
                                    vertices.Add(new Vector3(i + 1, j + 0, k + 0));
                                    vertices.Add(new Vector3(i + 1, j + 0, k + 1));
                                    vertices.Add(new Vector3(i + 1, j + 1, k + 1));
                                    vertices.Add(new Vector3(i + 1, j + 1, k + 0));// right face
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));


                                }
                            }
                            else
                            {
                                if (draw.chunks.ContainsKey(new Vector3(((int)transform.position.x / 16) + 1, 0, ((int)transform.position.z / 16))))
                                {
                                    if (draw.chunks[new Vector3(((int)transform.position.x / 16) + 1, 0, ((int)transform.position.z / 16))].GetComponent<ChunkTerrain>().thechunk.ContainsKey(new Vector3(0, j, k)))
                                    {
                                        if (draw.chunks[new Vector3(((int)transform.position.x / 16) + 1, 0, ((int)transform.position.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(0, j, k)].id == 0 || blockstore.modelIDs.Contains(draw.chunks[new Vector3(((int)transform.position.x / 16) + 1, 0, ((int)transform.position.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(0, j, k)].id))
                                        {
                                            triangles.Add(vertices.Count + 0);
                                            triangles.Add(vertices.Count + 3);
                                            triangles.Add(vertices.Count + 2);
                                            triangles.Add(vertices.Count + 0);
                                            triangles.Add(vertices.Count + 2);
                                            triangles.Add(vertices.Count + 1);
                                            vertices.Add(new Vector3(i + 1, j + 0, k + 0));
                                            vertices.Add(new Vector3(i + 1, j + 0, k + 1));
                                            vertices.Add(new Vector3(i + 1, j + 1, k + 1));
                                            vertices.Add(new Vector3(i + 1, j + 1, k + 0));// right face
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                        }
                                    }
                                }
                            }
                            vec2.x -= 2;
                            if (thechunk.ContainsKey(vec2))
                            {
                                if (thechunk[vec2].id == 0 || blockstore.modelIDs.Contains(thechunk[vec2].id))
                                {
                                    triangles.Add(vertices.Count + 1);
                                    triangles.Add(vertices.Count + 2);
                                    triangles.Add(vertices.Count + 3);
                                    triangles.Add(vertices.Count + 1);
                                    triangles.Add(vertices.Count + 3);
                                    triangles.Add(vertices.Count + 0);
                                    vertices.Add(new Vector3(i + 0, j + 0, k + 0));
                                    vertices.Add(new Vector3(i + 0, j + 0, k + 1));
                                    vertices.Add(new Vector3(i + 0, j + 1, k + 1));
                                    vertices.Add(new Vector3(i + 0, j + 1, k + 0));// left face
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));


                                }
                            }
                            else
                            {
                                if (draw.chunks.ContainsKey(new Vector3(((int)transform.position.x / 16) - 1, 0, ((int)transform.position.z / 16))))
                                {
                                    if (draw.chunks[new Vector3(((int)transform.position.x / 16) - 1, 0, ((int)transform.position.z / 16))].GetComponent<ChunkTerrain>().thechunk.ContainsKey(new Vector3(15, j, k)))
                                    {
                                        if (draw.chunks[new Vector3(((int)transform.position.x / 16) - 1, 0, ((int)transform.position.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15, j, k)].id == 0 || blockstore.modelIDs.Contains(draw.chunks[new Vector3(((int)transform.position.x / 16) - 1, 0, ((int)transform.position.z / 16))].GetComponent<ChunkTerrain>().thechunk[new Vector3(15, j, k)].id))
                                        {
                                            triangles.Add(vertices.Count + 1);
                                            triangles.Add(vertices.Count + 2);
                                            triangles.Add(vertices.Count + 3);
                                            triangles.Add(vertices.Count + 1);
                                            triangles.Add(vertices.Count + 3);
                                            triangles.Add(vertices.Count + 0);
                                            vertices.Add(new Vector3(i + 0, j + 0, k + 0));
                                            vertices.Add(new Vector3(i + 0, j + 0, k + 1));
                                            vertices.Add(new Vector3(i + 0, j + 1, k + 1));
                                            vertices.Add(new Vector3(i + 0, j + 1, k + 0));// left face
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                        }
                                    }
                                }
                            }
                            vec2.x++;
                            vec2.z += 1;
                            if (thechunk.ContainsKey(vec2))
                            {
                                if (thechunk[vec2].id == 0 || blockstore.modelIDs.Contains(thechunk[vec2].id))
                                {
                                    triangles.Add(vertices.Count + 1);
                                    triangles.Add(vertices.Count + 3);
                                    triangles.Add(vertices.Count + 2);
                                    triangles.Add(vertices.Count + 1);
                                    triangles.Add(vertices.Count + 2);
                                    triangles.Add(vertices.Count + 0);
                                    vertices.Add(new Vector3(i + 0, j + 0, k + 1));
                                    vertices.Add(new Vector3(i + 1, j + 0, k + 1));
                                    vertices.Add(new Vector3(i + 0, j + 1, k + 1));
                                    vertices.Add(new Vector3(i + 1, j + 1, k + 1));//back face
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));


                                }
                            }
                            else
                            {
                                if (draw.chunks.ContainsKey(new Vector3(((int)transform.position.x / 16), 0, ((int)transform.position.z / 16) + 1)))
                                {
                                    if (draw.chunks[new Vector3(((int)transform.position.x / 16), 0, ((int)transform.position.z / 16) + 1)].GetComponent<ChunkTerrain>().thechunk.ContainsKey(new Vector3(i, j, 0)))
                                    {
                                        if (draw.chunks[new Vector3(((int)transform.position.x / 16), 0, ((int)transform.position.z / 16) + 1)].GetComponent<ChunkTerrain>().thechunk[new Vector3(i, j, 0)].id == 0 || blockstore.modelIDs.Contains(draw.chunks[new Vector3(((int)transform.position.x / 16), 0, ((int)transform.position.z / 16) + 1)].GetComponent<ChunkTerrain>().thechunk[new Vector3(i, j, 0)].id))
                                        {
                                            triangles.Add(vertices.Count + 1);
                                            triangles.Add(vertices.Count + 3);
                                            triangles.Add(vertices.Count + 2);
                                            triangles.Add(vertices.Count + 1);
                                            triangles.Add(vertices.Count + 2);
                                            triangles.Add(vertices.Count + 0);
                                            vertices.Add(new Vector3(i + 0, j + 0, k + 1));
                                            vertices.Add(new Vector3(i + 1, j + 0, k + 1));
                                            vertices.Add(new Vector3(i + 0, j + 1, k + 1));
                                            vertices.Add(new Vector3(i + 1, j + 1, k + 1));//back face
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                        }
                                    }
                                }
                            }
                            vec2.z -= 2;
                            if (thechunk.ContainsKey(vec2))
                            {
                                if (thechunk[vec2].id == 0 || blockstore.modelIDs.Contains(thechunk[vec2].id))
                                {
                                    triangles.Add(vertices.Count + 0);
                                    triangles.Add(vertices.Count + 2);
                                    triangles.Add(vertices.Count + 3);
                                    triangles.Add(vertices.Count + 0);
                                    triangles.Add(vertices.Count + 3);
                                    triangles.Add(vertices.Count + 1);
                                    vertices.Add(new Vector3(i + 0, j + 0, k + 0));
                                    vertices.Add(new Vector3(i + 1, j + 0, k + 0));
                                    vertices.Add(new Vector3(i + 0, j + 1, k + 0));
                                    vertices.Add(new Vector3(i + 1, j + 1, k + 0));//front face
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                    uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));


                                }
                            }
                            else
                            {
                                if (draw.chunks.ContainsKey(new Vector3(((int)transform.position.x / 16), 0, ((int)transform.position.z / 16) - 1)))
                                {
                                    if (draw.chunks[new Vector3(((int)transform.position.x / 16), 0, ((int)transform.position.z / 16) - 1)].GetComponent<ChunkTerrain>().thechunk.ContainsKey(new Vector3(i, j, 15)))
                                    {
                                        if (draw.chunks[new Vector3(((int)transform.position.x / 16), 0, ((int)transform.position.z / 16) - 1)].GetComponent<ChunkTerrain>().thechunk[new Vector3(i, j, 15)].id == 0 || blockstore.modelIDs.Contains(draw.chunks[new Vector3(((int)transform.position.x / 16), 0, ((int)transform.position.z / 16) - 1)].GetComponent<ChunkTerrain>().thechunk[new Vector3(i, j, 15)].id))
                                        {
                                            triangles.Add(vertices.Count + 0);
                                            triangles.Add(vertices.Count + 2);
                                            triangles.Add(vertices.Count + 3);
                                            triangles.Add(vertices.Count + 0);
                                            triangles.Add(vertices.Count + 3);
                                            triangles.Add(vertices.Count + 1);
                                            vertices.Add(new Vector3(i + 0, j + 0, k + 0));
                                            vertices.Add(new Vector3(i + 1, j + 0, k + 0));
                                            vertices.Add(new Vector3(i + 0, j + 1, k + 0));
                                            vertices.Add(new Vector3(i + 1, j + 1, k + 0));//front face
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + 0 + onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + 0 + onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                            uvs.Add(new Vector2(thechunk[vec3].sidesTex.x + uvwidth - onepixel, thechunk[vec3].sidesTex.y + uvwidth - onepixel));
                                        }
                                    }
                                }
                            }
                            vec2.z++;
                        }
                }
                }
                
            }
        }
        if (combs.Count > 0)
        {
            Mesh secondmesh = ActuallyCombineMeshes(combs);
            
            this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.transform.GetChild(0).GetComponent<MeshFilter>().mesh = secondmesh;
            this.gameObject.transform.GetChild(0).GetComponent<MeshFilter>().sharedMesh = secondmesh;
            this.gameObject.transform.GetChild(0).GetComponent<MeshCollider>().enabled = true;
            this.gameObject.transform.GetChild(0).GetComponent<MeshCollider>().sharedMesh = secondmesh;
            Debug.Log("Combscount: " + combs.Count + "!!");
           
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.transform.GetChild(0).GetComponent<MeshCollider>().enabled = false;
        }



        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 128);
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.SetUVs(0, uvs);
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.OptimizeReorderVertexBuffer();

        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
        this.gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
        this.isRebuilt = true;
    }
    Mesh ActuallyCombineMeshes(List<CombineEgg> coms)
    {
        Mesh m = new();

        List<Vector3> verts = new();
        List<int> tris = new();
        List<Vector2> uvs = new();
        for (int i = 0; i < coms.Count; i++)
        {
            //tris
            for (int z = 0; z < coms[i].mesh.triangles.Length; z++)
            {
                tris.Add(coms[i].mesh.triangles[z] + verts.Count);

            }
            //verts
            for (int z = 0; z < coms[i].mesh.vertices.Length; z++)
            {
                Vector3 coo = coms[i].mesh.vertices[z]*2;
                Vector3 theTransform = coms[i].localPosition;
                verts.Add(new Vector3(coo.x + theTransform.x - 1, coo.y + theTransform.y, coo.z + theTransform.z + 0.5f)); //offset on z and x to center on block!

            }
            //uvs
            for (int z = 0; z < coms[i].mesh.uv.Length; z++)
            {
                uvs.Add(coms[i].mesh.uv[z]);

            }
        }
        m.vertices = verts.ToArray();
        m.triangles = tris.ToArray();
        m.uv = uvs.ToArray();
        m.bounds = new Bounds(Vector3.zero, Vector3.one * 128);
        m.RecalculateNormals();

        return m;
    }
    // Update is called once per frame
    void Update()
    {
        this.dimension = draw.dimension;
      if(Vector3.Distance(this.transform.position+(transform.up*35), player.transform.position) > 200)
        {
            if(player.GetComponent<PlayerScript>().neededChunks.Count > 2)
            {
                var need = player.GetComponent<PlayerScript>().neededChunks[0];
                    var thispos = new Vector3((int)((int)transform.position.x / (int)16), 0, (int)((int)transform.position.z / (int)16));
                            draw.chunks.Remove(thispos);
                    
                    player.GetComponent<PlayerScript>().neededChunks.RemoveAt(0);
                    this.transform.position = new Vector3(need.x * 16, 0, need.y * 16);
                //this.GetComponent<MeshRenderer>().enabled = true;
                    draw.chunks.Add(new Vector3(need.x, 0, need.y), this.gameObject);
                    this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    this.isRebuilt = false;
                    //this.isInRemeshQueue = false;
                    this.OnInstantiate();
                    this.UpdateSurrounders();
            } else
            {
                //this.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
