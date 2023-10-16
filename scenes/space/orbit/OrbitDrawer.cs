using Godot;
using System;
using System.Collections.Generic;

public partial class OrbitDrawer : MeshInstance3D
{
	[Export] public OrbitData OrbitData { get; set; } = new OrbitData();

	public override void _Ready()
	{
		OrbitData.Changed += RecalculateMesh;
		RecalculateMesh();
	}

	private void RecalculateMesh()
	{
		var surfaceArray = new Godot.Collections.Array();
		surfaceArray.Resize((int)Mesh.ArrayType.Max);

		var verts = new List<Vector3>(){new(0, 0, 0), new(1, 0, 0), new(0, 0, 1)};
		var uvs = new List<Vector2>(){new(0, 0), new(0, 0), new(0, 0)};
		var normals = new List<Vector3>(){new(0, 1, 0), new(0, 1, 0), new(0, 1, 0)};
		var indices = new List<int>(){0, 1, 2};

		surfaceArray[(int)Mesh.ArrayType.Vertex] = verts.ToArray();
		surfaceArray[(int)Mesh.ArrayType.TexUV] = uvs.ToArray();
		surfaceArray[(int)Mesh.ArrayType.Normal] = normals.ToArray();
		surfaceArray[(int)Mesh.ArrayType.Index] = indices.ToArray();

		var arrMesh = new ArrayMesh();
		arrMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.LineStrip, surfaceArray);
		Mesh = arrMesh;
	}

}
