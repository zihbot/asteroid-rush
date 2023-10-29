using Godot;
using System;

public partial class Orbitter : Node3D
{
    [Export] public OrbitData OrbitData;

    private SystemManager _systemManager;
    private Node3D _parent;
    private float trueAnomaly = 0;
    public override void _Ready()
    {
        _systemManager = GetNode<SystemManager>(SystemManager.TreeName);
        _parent = GetParent<Node3D>();
    }

    public override void _Process(double delta)
    {
        var deltaF = Mathf.PosMod((float)delta, Mathf.Pi * 2);
        trueAnomaly = Mathf.Wrap(trueAnomaly + deltaF, -Mathf.Pi, Mathf.Pi);
        if (_parent == null || !_parent.Visible || OrbitData == null) return;

        var position = OrbitData.PositionAtTrueAnomaly(trueAnomaly);
        _parent.Position = position * _systemManager.SpaceScale;
    }
}
