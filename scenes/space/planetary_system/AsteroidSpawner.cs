using Godot;
using System;
using System.Linq;

public partial class AsteroidSpawner : Node3D
{
    [Export] public PackedScene PlanetContextScene { get; set; } = null;
    [Signal] public delegate void StartMissionEventHandler(CelestialBody target);

    CelestialBody[] asteroids = Array.Empty<CelestialBody>();
    public override void _Ready()
    {
        asteroids = GetChildren().Cast<CelestialBody>().ToArray();
        if (PlanetContextScene == null) return;
        foreach (var asteroid in asteroids)
        {
            asteroid.Clicked += () => {
                var planetContext = PlanetContextScene.Instantiate<PlanetContextMenu>();
                planetContext.StartMission += () => EmitSignal(nameof(StartMission), asteroid);
                AddChild(planetContext);
            };
        }
    }

}
