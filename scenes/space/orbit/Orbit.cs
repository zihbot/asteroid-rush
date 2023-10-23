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

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Left && mouseButton.IsPressed())
        {
            /*var mesh = _orbitDrawer.Vertices.ToArray();
            var camera = GetViewport().GetCamera3D();
            for (int i = 0; i < mesh.Length - 1; i++)
            {
                var rect = new Rect2(camera.UnprojectPosition(mesh[i]), 0, 0);
            }*/
            GD.Print("Clicked");
        }
    }
}
