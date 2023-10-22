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
        GD.Print("RecalculateMesh");
        var surfaceArray = new Godot.Collections.Array();
        surfaceArray.Resize((int)Mesh.ArrayType.Max);

        var verts = new List<Vector3>();
        var uvs = new List<Vector2>();
        var normals = new List<Vector3>();
        var indices = new List<int>();

        for (float fi = -Mathf.Pi; fi < Mathf.Pi; fi += 2 * Mathf.Pi / 360)
        {
            var pos = OrbitData.PositionAtTrueAnomaly(fi);
            verts.Add(pos);
            uvs.Add(new Vector2(0, 0));
            normals.Add(new Vector3(0, 1, 0));
            indices.Add(verts.Count - 1);
        }

        surfaceArray[(int)Mesh.ArrayType.Vertex] = verts.ToArray();
        surfaceArray[(int)Mesh.ArrayType.TexUV] = uvs.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Normal] = normals.ToArray();
        surfaceArray[(int)Mesh.ArrayType.Index] = indices.ToArray();

        var arrMesh = new ArrayMesh();
        arrMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.LineStrip, surfaceArray);
        Mesh = arrMesh;
    }

}
