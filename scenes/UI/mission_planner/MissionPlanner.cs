using Godot;
using System;

public partial class MissionPlanner : Control
{
    [Export] public PackedScene OrbitScene { get; set; } = null;
    [Export] public Node Origin { get; set; } = null;
    [Export] public Node Target { get; set; } = null;

	public override void _Ready()
	{
        if (Origin == null || Target == null || OrbitScene == null)
        {
            QueueFree();
            return;
        }

        // Add LaunchTrajectory
        Vector3 launchPad = new(0, 1, 2);
        Vector3 targetSpeed = new(1, 0, 0);

        LaunchTrajectoryData launch = new()
        {
            VelocityAtReferencePoint = targetSpeed
        };

        Orbit launchOrbit = OrbitScene.Instantiate<Orbit>();

        Origin.AddChild(launchOrbit);
    }
}
