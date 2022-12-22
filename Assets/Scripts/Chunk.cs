using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Chunk : MonoBehaviour
{
	public MeshRenderer meshRenderer;
	public MeshFilter meshFilter;
	public MeshCollider meshCollider;

	private short maxFaces;
	Block.Id[,,] blockIds;

	int maxVertexIndex;
	int minVertexIndex ;

	private byte chunkWidth;
	private Vector3 perlinScale;

	public void BuildChunk(int chunkWidth, Vector3 perlinScale)
	{
		if (chunkWidth < 1) return;

		this.transform.localScale = Vector3.one / chunkWidth;

		maxVertexIndex = int.MinValue;
		minVertexIndex = int.MaxValue;

		this.chunkWidth = (byte)chunkWidth;
		this.perlinScale = perlinScale;

        maxFaces = (short)(this.chunkWidth * this.chunkWidth * this.chunkWidth * Block.FacesPerBlock);
        blockIds = new Block.Id[this.chunkWidth, this.chunkWidth, this.chunkWidth];
		byte[,] perlinMap = new byte[this.chunkWidth + 1, this.chunkWidth + 1]; // +1 is temporary
        Perlin.Preload(perlinMap, transform.position, this.chunkWidth, perlinScale);// use highest perlin to set inital index buffer size
		CreateMesh();
		PreloadVectors(); // dynamic size also
		GenerateMeshData(perlinMap);
		DrawMesh();
	}

	public void RebuildChunk(int chunkWidth, Vector3 perlinScale)
	{
		long[] zeros = new long[maxFaces];
		Array.Clear(zeros, 0, maxFaces);
		meshFilter.mesh.SetIndexBufferData(zeros, 0, 0, maxFaces, MeshUpdateFlags.DontValidateIndices);
		DrawMesh();
		BuildChunk(chunkWidth, perlinScale);
	}


	// todo, can buffers be changed dynamically?
	// store +- 1y, if block added grow, if blocks removed, shrink. first test if growing and shrinking affects data

	// todo  get perlin function, and get Id function need to query neighbor chunks

	// todo generate backwards, otherwise looking forward wont work, and get proper block ids
	private void GenerateMeshData(byte[,] perlinMap)
	{
		for (int x = 0; x < chunkWidth; x++)
		{
			for (int z = 0; z < chunkWidth; z++)
			{
				byte p  = perlinMap[x, z];
				byte px = perlinMap[x + 1, z];
				byte pz = perlinMap[x, z + 1];

				for (int y = 0; y < p; y++)
				{
					blockIds[x, y, z] = Block.Id.Dirt;
					if (y > px) AddFace(x, y, z, Block.Face.Right, Block.Id.Dirt);
					if (y > pz) AddFace(x, y, z, Block.Face.Front, Block.Id.Dirt);
				}
				Debug.Log(String.Format("{0} {1} {2}", x, p, z));
				blockIds[x, p, z] = Block.Id.Grass;
				AddFace(x, p, z, Block.Face.Top, Block.Id.Grass);


				if (px < p)
				{
					for (int y = px + 1; y < p; y++) AddFace(x, y, z, Block.Face.Right, Block.Id.Dirt);
					AddFace(x, p, z, Block.Face.Right, Block.Id.Grass);
				}
				else if (px > p)
				{
					for (int y = p + 1; y < px; y++) AddFace(x + 1, y, z, Block.Face.Left, Block.Id.Dirt);
					AddFace(x + 1, px, z, Block.Face.Left, Block.Id.Grass);
				}

				if (pz < p)
				{
					for (int y = pz + 1; y < p; y++) AddFace(x, y, z, Block.Face.Front, Block.Id.Dirt);
					AddFace(x, p, z, Block.Face.Front, Block.Id.Grass);
				}
				else if (pz > p)
				{
					for (int y = p + 1; y < pz; y++) AddFace(x, y, z + 1, Block.Face.Back, Block.Id.Dirt);
					AddFace(x, pz, z + 1, Block.Face.Back, Block.Id.Grass);
				}
			}
		}
	}


	void AddFace(int x, int y, int z, Block.Face _face, Block.Id id)
	{
		int face = (int)_face;
		int spriteIndex = Block.Faces[id][face];

		// write quads (triangles) to buffer
		int quadIndex = 0;
		if ( face > 2 )
		{
			if (face == 3) { x--; }
			else if (face == 4) { y--; }
			else if (face == 5) { z--; }
			face -= 3;
			quadIndex = 1;
		}
		int faceIndex = (((y * chunkWidth + x) * chunkWidth) + z)  * Block.FacesPerBlock + face;
		long[] quad = { Block.Quad[quadIndex] + Block.QuadOffset * faceIndex };
		meshFilter.mesh.SetIndexBufferData(quad, 0, faceIndex, 1); //, MeshUpdateFlags.DontValidateIndices);

		// get sprite coords
		int s_x = spriteIndex / 16;
		int s_y = spriteIndex % 16;
		int[] UVs = new int[] {
				Block.UVMap[s_x] << 16 | Block.UVMap[s_y],
				Block.UVMap[s_x] << 16 | Block.UVMap[s_y + 1],
			Block.UVMap[s_x + 1] << 16 | Block.UVMap[s_y],
			Block.UVMap[s_x + 1] << 16 | Block.UVMap[s_y + 1],
		};

		// write normals and uv to buffer
		int uvIndex = 0;
		for (int i = 3; i < Block.IntsPerFace; i += Block.IntsPerVertex )
		{
			int faceIntIndex = faceIndex * Block.IntsPerFace + i;
			meshFilter.mesh.SetVertexBufferData(Block.Normals, face * Block.IntsPerNormal, faceIntIndex, Block.IntsPerNormal);
			meshFilter.mesh.SetVertexBufferData(UVs, uvIndex, faceIntIndex + Block.IntsPerNormal, 1);
			uvIndex += 1;
		}

		if (faceIndex > maxVertexIndex) maxVertexIndex = faceIndex;
		if (faceIndex < minVertexIndex) minVertexIndex = faceIndex;
	}

	void RemoveFace(int x, int y, int z, Block.Face face) //todo test this
	{
		int faceindex = (((y * chunkWidth + x) * chunkWidth) + z) * Block.FacesPerBlock + (int)face;
		meshFilter.mesh.SetIndexBufferData(Block.QuadZero, 0, faceindex, 1); //, MeshUpdateFlags.DontValidateIndices);
	}

	//on delete, occasionally move min and max?

	private void PreloadVectors()
	{
		float[] faceVectors = new float[Block.FaceMapSize];
		Array.Copy(Block.VertexMap, faceVectors, Block.FaceMapSize);
		int index = 0;
		for (int y = 0; y < chunkWidth; y++)
		{
			for (int i = 1; i < Block.FaceMapSize; i += Block.IntsPerVertex) faceVectors[i] = y + Block.VertexMap[i];
			for (int x = 0; x < chunkWidth; x ++)
			{
				for (int i = 0; i < Block.FaceMapSize; i += Block.IntsPerVertex) faceVectors[i] = x + Block.VertexMap[i];
				for (int z = 0; z < chunkWidth; z++)
				{
					for (int i = 2; i < Block.FaceMapSize; i += Block.IntsPerVertex) faceVectors[i] = z + Block.VertexMap[i];
					meshFilter.mesh.SetVertexBufferData(faceVectors, 0, index * Block.FaceMapSize, Block.FaceMapSize);
					index++;
				}
            }
        }
	}

	private void CreateMesh()
	{
		meshFilter = this.gameObject.GetComponent<MeshFilter>();
		meshFilter.mesh = new();
		this.gameObject.GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;

		meshFilter.mesh.SetVertexBufferParams(maxFaces * Block.VertsPerFace, vertexAttributeDescriptor);
		meshFilter.mesh.SetIndexBufferParams(maxFaces * Block.VertsPerFace, IndexFormat.UInt16);

		long[] zeros = new long[maxFaces];
		Array.Clear(zeros, 0, maxFaces);
		meshFilter.mesh.SetIndexBufferData(zeros, 0, 0, maxFaces, MeshUpdateFlags.DontValidateIndices);
	}

	private void DrawMesh()
	{
		meshFilter.mesh.SetSubMesh(0, GetSubMeshDescriptor());//, MeshUpdateFlags.DontRecalculateBounds);
	}

	private static readonly VertexAttributeDescriptor[] vertexAttributeDescriptor = new VertexAttributeDescriptor[]
	{
		new VertexAttributeDescriptor(VertexAttribute.Position,  VertexAttributeFormat.Float32, 3),
		new VertexAttributeDescriptor(VertexAttribute.Normal,    VertexAttributeFormat.Float32, 3),
		new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float16, 2),
	};

	private SubMeshDescriptor GetSubMeshDescriptor()
	{
		int indicesCount = (1 + maxVertexIndex - minVertexIndex ) * Block.VertsPerFace;
		return new(minVertexIndex * Block.VertsPerFace, indicesCount, MeshTopology.Quads) // todo fix bounds
		{
			bounds = new Bounds(new(chunkWidth / 2, chunkWidth / 2, chunkWidth / 2), new(chunkWidth, chunkWidth, chunkWidth)),
			firstVertex = minVertexIndex * Block.VertsPerFace,
			vertexCount = indicesCount
		};
	}

	/*
	 * private Block.Id GetBlockId(int x, int y, int z) // change to get block?
	{

		if (y < 0)
		{
			return Block.Id.Bedrock;
		}
		if (y >= ChunkHeight) // add vertical chunks?
		{
			return Block.Id.Air;
		}

		// check neighboring chunk
		if (x < 0|| z < 0 || x >= ChunkWidth  || z >= ChunkWidth)
		{
			//Debug.LogError("Cannot get blocks from outside chunk, yet.");
			return Block.Id.Bedrock; 
		}

		return blocks[x, y, z] == null ? Block.Id.Air : blocks[x, y, z].id;
	}

	private void SetBlockId(int x, int y, int z, Block.Id id)
    {
		Block block = blocks[x, y, z];
		if (block == null)
        {
			blocks[x, y, z] = new(id);
        } 
		else
        {
			block.id = id;
        }
	}
	*/

	public class Perlin // move somewhere else
	{
        public static void Preload(byte[,] map, Vector3 pos, int chunkWidth, Vector3 perlinScale)
		{
			pos *= chunkWidth; // rescale
			for (int x = 0; x <= chunkWidth; x++) //replace with query
			{
				for (int z = 0; z <= chunkWidth; z++) //replace with query
				{

					map[x,z] = Noise(x + pos.x, z + pos.z, perlinScale, chunkWidth);
				}
			}
		}
		public static byte Noise(float x, float z, Vector3 perlinScale, int chunkWidth)
		{
			float perlin = Mathf.PerlinNoise(x / perlinScale.x, z / perlinScale.x);
			float modifiedP = Mathf.Floor(perlin * perlinScale.y);
			byte p = (byte)Mathf.Clamp(modifiedP, 1, chunkWidth - 1);
			return p;
		}
    }

	void Update()
	{
	}
}
