using Godot;
using System;

public partial class CelestialBody : Node3D
{
    [Export] public CelestialBodyData Data { get; set; } = new CelestialBodyData();
    [Signal] public delegate void ClickedEventHandler();

    private SystemManager _systemManager;
    private StaticBody3D _staticBody;

    public override void _Ready()
    {
        _systemManager = GetNode<SystemManager>(SystemManager.TreeName);
        _systemManager.SpaceScaleChanged += (scale) => {
            float bodyScale = scale * Data.Radius;
            _staticBody.Scale = new Vector3(bodyScale, bodyScale, bodyScale);
        };

        _staticBody = GetNode<StaticBody3D>("StaticBody3D");
        _staticBody.InputEvent += (Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shape_idx) =>
        {
            if (@event is InputEventMouseButton mouseButton && mouseButton.IsPressed() && mouseButton.ButtonIndex == MouseButton.Left)
            {
                EmitSignal(SignalName.Clicked);
                GetViewport().SetInputAsHandled();
            }
        };

    }

    public override void _Process(double delta)
    {
    }
}
