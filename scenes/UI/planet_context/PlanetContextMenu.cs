using Godot;
using System;

public partial class PlanetContextMenu : Control
{
    [Signal] public delegate void NewOrbitEventHandler();
    [Signal] public delegate void StartMissionEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        VisibilityChanged += () =>
        {
            if (Visible)
            {
                Position = GetViewport().GetMousePosition();
            }
        };
        if (Visible)
        {
            Position = GetViewport().GetMousePosition();
        }
        GetNode<Button>("Box/NewOrbitButton").Pressed += () => { EmitSignal(nameof(NewOrbit)); Hide(); };
        GetNode<Button>("Box/StartMissionButton").Pressed += () => { EmitSignal(nameof(StartMission)); Hide(); };
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton && mouseButton.IsPressed() && mouseButton.ButtonIndex == MouseButton.Left)
        {
            if (!GetGlobalRect().HasPoint(mouseButton.Position))
            {
                Hide();
            }
        }
    }
}
