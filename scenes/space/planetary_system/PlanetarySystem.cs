using Godot;
using System;

public partial class PlanetarySystem : Node3D
{
	public override void _Ready()
	{
		var planet = GetChild<Planet>(0);
		planet.PlanetClicked += () =>
		{
			var planetContext = GetNode<Control>("PlanetContext");
			planetContext.Show();
		};
	}
}
