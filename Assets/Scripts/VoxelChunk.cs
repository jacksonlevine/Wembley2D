using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VoxelChunk : MonoBehaviour
{
    Block.Id[,,] blockIds;

    public GameObject ball;

    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;

    int indexOffset = 0;
    readonly byte chunkWidth = 3;



    void Start()
    {

        blockIds = new Block.Id[this.chunkWidth, this.chunkWidth, this.chunkWidth];

        blockIds[0, 0, 0] = Block.Id.Dirt;
        blockIds[0, 1, 0] = Block.Id.Dirt;
        blockIds[0, 2, 0] = Block.Id.Dirt;
        blockIds[0, 2, 1] = Block.Id.Dirt;
        blockIds[0, 2, 2] = Block.Id.Dirt;
        blockIds[0, 1, 2] = Block.Id.Dirt;
        blockIds[0, 1, 1] = Block.Id.Dirt;
        
        /*blockIds[0, 0, 2] = Block.Id.Dirt;
        blockIds[0, 0, 1] = Block.Id.Dirt;
        
        */


        CreateMesh();

        byte corners = 0;

        for ( int z = 0; z < chunkWidth - 1; z++)
        {
            for (int y = 0; y < chunkWidth - 1; y++)
            {
                if (blockIds[0, y + 0, z + 0] != Block.Id.Air) corners += 1;
                if (blockIds[0, y + 0, z + 1] != Block.Id.Air) corners += 2;
                if (blockIds[0, y + 1, z + 0] != Block.Id.Air) corners += 4;
                if (blockIds[0, y + 1, z + 1] != Block.Id.Air) corners += 8;
                for (int x = 1; x < chunkWidth; x++)
                {
                    if (blockIds[x, y + 0, z + 0] != Block.Id.Air) corners += 16;
                    if (blockIds[x, y + 0, z + 1] != Block.Id.Air) corners += 32;
                    if (blockIds[x, y + 1, z + 0] != Block.Id.Air) corners += 64;
                    if (blockIds[x, y + 1, z + 1] != Block.Id.Air) corners += 128;
                    if (corners != 0 && corners != 255) AddFaceToMesh(corners, x, y, z);
                    corners >>= 4;
                }
            }
        }

        DrawMesh();
    }

    // todo dont pass 0


    void AddFaceToMesh(int face, int x, int y, int z)
    {

        uint[] vertices = Voxel.Vertices[face];
        if( vertices.Length < 7)
        {
            foreach(int vert in vertices)
            {
                AddFaceToMesh(vert, x, y, z);
            }
            return;
        }

        int indexLength = (vertices.Length / 7) * 3 - 6;
        int vertexOffset = indexOffset * 7;
        ushort[] indices = new ushort[indexLength];

        for (int i = 0; i < vertices.Length; i += 7)
        {
            vertices[i + 0] = Add(vertices[i + 0], x);
            vertices[i + 1] = Add(vertices[i + 1], y);
            vertices[i + 2] = Add(vertices[i + 2], z);
        }

        for (int i = 0; i < indexLength; i++) indices[i] = (ushort)(Voxel.Triangles[i] + indexOffset);

        meshFilter.mesh.SetVertexBufferData(vertices, 0, vertexOffset, vertices.Length);
        meshFilter.mesh.SetIndexBufferData(indices, 0, indexOffset, indexLength); //, MeshUpdateFlags.DontValidateIndices);
        indexOffset += indexLength;
    }

    uint Add (uint vert, int offset)
    {
        float v = BitConverter.Int32BitsToSingle((int)vert);
        float o = offset * 2;
        int r = BitConverter.SingleToInt32Bits(v + o);
        return (uint)r;
    }

    private void CreateMesh()
    {
        indexOffset = 0;
        int vertexBufferSize = chunkWidth * chunkWidth * chunkWidth * 15;
        int indexBufferSize = vertexBufferSize;

        meshFilter = this.gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = new();
        this.gameObject.GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
        meshFilter.mesh.SetVertexBufferParams(vertexBufferSize, vertexAttributeDescriptor);
        meshFilter.mesh.SetIndexBufferParams(indexBufferSize, IndexFormat.UInt16);

        ushort[] zeros = new ushort[indexBufferSize];
        Array.Clear(zeros, 0, indexBufferSize);
        meshFilter.mesh.SetIndexBufferData(zeros, 0, 0, indexBufferSize, MeshUpdateFlags.DontValidateIndices);
    }

    private void DrawMesh()
    {
        meshFilter.mesh.SetSubMesh(0, GetSubMeshDescriptor(), MeshUpdateFlags.DontRecalculateBounds);
    }

    private static readonly VertexAttributeDescriptor[] vertexAttributeDescriptor = new VertexAttributeDescriptor[]
    {
        new VertexAttributeDescriptor(VertexAttribute.Position,  VertexAttributeFormat.Float32, 3),
        new VertexAttributeDescriptor(VertexAttribute.Normal,    VertexAttributeFormat.Float32, 3),
        new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float16, 2),
    };

    private SubMeshDescriptor GetSubMeshDescriptor()
    {
        return new(0, indexOffset, MeshTopology.Triangles) // todo fix bounds
        {
            bounds = new Bounds(new(chunkWidth / 2, chunkWidth / 2, chunkWidth / 2), new(chunkWidth, chunkWidth, chunkWidth)),
            firstVertex = 0,
            vertexCount = indexOffset
        };
    }
}
