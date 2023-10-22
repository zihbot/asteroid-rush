using Godot;
using System;

public partial class PlanetContextMenu : Control
{
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
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton && mouseButton.IsPressed() && mouseButton.ButtonIndex == MouseButton.Left)
        {
            if (!GetViewportRect().HasPoint(mouseButton.Position))
            {
                Hide();
            }
        }
    }
}
