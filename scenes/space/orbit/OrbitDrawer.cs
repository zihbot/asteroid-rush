using Godot;
using System;
using System.Collections.Generic;

public partial class OrbitDrawer : MeshInstance3D
{
    private OrbitData _data = null;
    [Export] public OrbitData OrbitData { get => _data; set => SetData(value); }

    public List<Vector3> Vertices { get; private set; } = new List<Vector3>();

    public override void _Ready()
    {
    }

    private void SetData(OrbitData data)
    {
        if (_data != null) _data.Changed -= RecalculateMesh;
        _data = data;
        _data.Changed += RecalculateMesh;
        RecalculateMesh();
    }

    private void RecalculateMesh()
    {
        var surfaceArray = new Godot.Collections.Array();
        surfaceArray.Resize((int)Mesh.ArrayType.Max);

        var Vertices = new List<Vector3>();
        var verts = new List<Vector3>();
        var uvs = new List<Vector2>();
        var normals = new List<Vector3>();
        var indices = new List<int>();

        for (float fi = -Mathf.Pi; fi < Mathf.Pi; fi += 2 * Mathf.Pi / 360)
        {
            var pos = OrbitData.PositionAtTrueAnomaly(fi);
            Vertices.Add(pos);
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
