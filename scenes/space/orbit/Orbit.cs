using Godot;
using System;

public partial class Orbit : Node3D
{
    private OrbitData _data = new();
    [Export] public OrbitData OrbitData { get => _data; set { _data = value; if (_orbitDrawer != null) _orbitDrawer.OrbitData = _data; } }
    private OrbitDrawer _orbitDrawer = null;
    public override void _Ready()
    {
        _orbitDrawer = GetNode<OrbitDrawer>("Drawer");
        _orbitDrawer.OrbitData = _data;
    }
}
