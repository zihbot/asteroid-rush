using Godot;
using System;

public partial class ClickableInputHandler : Node3D
{
    private Vector2? _lastClick = null;

    public override void _PhysicsProcess(double delta)
    {
        if (_lastClick == null)
        {
            return;
        }
        var camera = GetViewport().GetCamera3D();
        var space = GetWorld3D().DirectSpaceState;
        var start = camera.ProjectRayOrigin(_lastClick ?? Vector2.Zero);
        var end = start + camera.ProjectRayNormal(_lastClick ?? Vector2.Zero) * camera.Far;
        var query = PhysicsRayQueryParameters3D.Create(start, end, Constans.PHYSICS_LAYER_MASK_CLICKABLE);
        _lastClick = null;
        var collision = space.IntersectRay(query);
        if (collision.Count == 0)
        {
            return;
        }
        //collision[0].As<CollisionShape3D>() Collider.GetParent().EmitSignal(nameof(Planet.PlanetClickedEventHandler));
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton && mouseButton.IsPressed() && mouseButton.ButtonIndex == MouseButton.Left)
        {
            _lastClick = mouseButton.Position;
        }
    }
}
