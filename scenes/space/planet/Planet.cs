using Godot;
using System;

public partial class Planet : Node3D
{
    [Signal] public delegate void PlanetClickedEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetChild<StaticBody3D>(0).InputEvent += (Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shape_idx) =>
        {
            if (@event is InputEventMouseButton mouseButton && mouseButton.IsPressed() && mouseButton.ButtonIndex == MouseButton.Left)
            {
                EmitSignal(SignalName.PlanetClicked);
                GetViewport().SetInputAsHandled();
            }
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
