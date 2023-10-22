using Godot;
using System;

public partial class CameraMovement : Node3D
{
    [Export] public Camera3D Camera = null;
    [Export] public Vector3 Center = Vector3.Zero;
    [Export] public float Sensitivity = 0.003f;

    public override void _Ready()
    {
        Camera ??= GetViewport().GetCamera3D();
        Camera.LookAt(Center, Vector3.Up);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        // If left mouse button is pressed while dragging the mouse
        if (Camera != null && @event is InputEventMouseMotion mouseMotion && (int)mouseMotion.ButtonMask == (int)MouseButton.Left)
        {
            var camPostion = Camera.Position - Center;
            // Rotate the camera around the center horizontally
            camPostion = camPostion.Rotated(Vector3.Up, -mouseMotion.Relative.X * Sensitivity);
            // Rotate the camera around the center vertically
            var crossProduct = camPostion.Cross(Vector3.Up);
            var newCamPosition = camPostion.Rotated(crossProduct.Normalized(), mouseMotion.Relative.Y * Sensitivity);
            // Prevent the camera from going upside down
            if (newCamPosition.Cross(Vector3.Up).Dot(crossProduct) > 0)
                camPostion = newCamPosition;
            Camera.LookAtFromPosition(Center + camPostion, Center);
        }
        else if (Camera != null && @event is InputEventMouseButton mouseWheel)
        {
            var camPostion = Camera.Position - Center;
            // Zoom in and out
            if (mouseWheel.ButtonIndex == MouseButton.WheelUp)
            {
                camPostion *= 0.95f;
            }
            else if (mouseWheel.ButtonIndex == MouseButton.WheelDown)
            {
                camPostion *= 1.05f;
            }
            else
            {
                return;
            }
            Camera.LookAtFromPosition(Center + camPostion, Center);
        }
    }
}
