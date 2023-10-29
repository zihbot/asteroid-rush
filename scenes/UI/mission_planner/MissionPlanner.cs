using Godot;
using System;

public partial class MissionPlanner : Control
{
    [Export] public Node Origin { get; set; } = null;
    [Export] public Node Target { get; set; } = null;

	public override void _Ready()
	{
	}
}
