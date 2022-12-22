using System;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Chunk chunkPrefab;

    private Dictionary<Vector3, Chunk> allChunks = new();
    public int bigPerlinX = 1;
    public int bigPerlinY = 1;
    public int smallPerlinX = 1;
    public int smallPerlinY = 1;
    public bool update = false;
    public int chunksize = 2;

    Chunk[,] chunks = new Chunk[16, 16];

    // Start is called before the first frame update
    void Start()
    {

        // size is one, we gotta grow them bitches out


        long d = DateTime.Now.Ticks;
        for (int x = 0; x < 1; x++)
        {
            for (int z = 0; z < 1; z++)
            {
                Vector3 perlinScale = new(bigPerlinX, bigPerlinY);
                byte p = Chunk.Perlin.Noise(x, z, perlinScale, 32);
                Chunk _chunk = Instantiate(chunkPrefab, new Vector3(x, p, z), Quaternion.identity, this.transform);
                _chunk.BuildChunk(chunksize, new(smallPerlinX, smallPerlinY));
                chunks[x, z] = _chunk;
                //allChunks.Add(new Vector3(x, p, z), _chunk);
            }
        }

        Debug.Log(String.Format("Loaded in {0:F} miliseconds ", (DateTime.Now.Ticks - d)/ 1000000));
    }

    void U()
    {
        long d = DateTime.Now.Ticks;
        for (int x = 0; x < 1; x++)
        {
            for (int z = 0; z < 1; z++)
            {
                Vector3 perlinScale = new(bigPerlinX, bigPerlinY);
                byte p = Chunk.Perlin.Noise(x, z, perlinScale, 32);
                chunks[x, z].RebuildChunk(chunksize, new(smallPerlinX, smallPerlinY));
                chunks[x, z].transform.Translate(0,p,0);
                //allChunks.Add(new Vector3(x, p, z), _chunk);
            }
        }

        Debug.Log(String.Format("Loaded in {0:F} miliseconds ", (DateTime.Now.Ticks - d) / 1000000));
    }

    // Update is called once per frame
    void Update()
    {
        if (update)
        {
            update = false;
            U();
        }
        
    }
}
