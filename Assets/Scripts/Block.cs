using System.Collections.Generic;
public class Block
{
	public enum Id : byte
	{
		Air,
		Grass,
		Dirt,


		Bedrock = 255,
	}

	public static Dictionary<Id, byte[]> Faces = new()
	{
		//       					 rgt,  up, fnt, lft, dwn, bck
		{ Id.Grass,		new byte[] {   1,   0,   1,   1,   3,   1 } },
		{ Id.Dirt,		new byte[] {   2,   2,   2,   2,   2,   2 } },


		{ Id.Bedrock,   new byte[] {   3,   3,   3,   3,   3,   3 } },
	};

	public static ushort[] UVMap = {
		0x0000, 0x2C00, 0x3000, 0x3200, 0x3400, 0x3500, 0x3600, 0x3700,
		0x3800, 0x3880, 0x3980, 0x3A00, 0x3A80, 0x3B00, 0x3B80, 0x3C00 };

	public static float[] VertexMap = new float[]
	{
		1, 0, 0, 0, 0, 0, 0 , 1, 0, 1, 0, 0, 0, 0 , 1, 1, 0, 0, 0, 0, 0 , 1, 1, 1, 0, 0, 0, 0, // right face
		0, 1, 0, 0, 0, 0, 0 , 1, 1, 0, 0, 0, 0, 0 , 0, 1, 1, 0, 0, 0, 0 , 1, 1, 1, 0, 0, 0, 0, // top face
		1, 0, 1, 0, 0, 0, 0 , 0, 0, 1, 0, 0, 0, 0 , 1, 1, 1, 0, 0, 0, 0 , 0, 1, 1, 0, 0, 0, 0, // front face
	};

	public static float[] Normals = { 1, 0, 0, 0, 1, 0, 0, 0, 1, -1, 0, 0, 0, -1, 0, 0, 0, -1, };

	public enum Face { Right, Top, Front, Left, Bottom, Back }

	public static long[] Quad = { 0x0002000000010003, 0x0001000000020003, }; 
	public static long QuadOffset = 0x0004000400040004;
	public static long[] QuadZero = { 0 };

	// fix and rename all these wtf
	public static readonly int FaceMapSize = 84;
	public static readonly int IntsPerBlock = 112;
	public static readonly int IntsPerVertex = 7;
	public static readonly int IntsPerNormal = 3;
	public static readonly int IntsPerFace = 28;
	public static readonly int FacesPerBlock = 3;

	public static readonly int VectorDataLen = 6;
	public static readonly int UvByteLen = 4;
	public static readonly int VectorByteLen = 24;
	public static readonly int VertsPerFace = 4;


}