using Godot;
using System;

public partial class PlanetarySystem : Node3D
{
    [Export] public PackedScene OrbitCreatorScene;
    [Export] public PackedScene MissionPlannerScene;

    private SystemManager _systemManager;
    private CelestialBody _mainPlanet;
    private AsteroidSpawner _asteroidSpawner;
    private PlanetContextMenu _planetContext;

    public override void _Ready()
    {
        _mainPlanet = GetNode<CelestialBody>("MainPlanet");
        _mainPlanet.Clicked += () =>
        {
            _planetContext.Show();
        };

        _systemManager = GetNode<SystemManager>(SystemManager.TreeName);
        _systemManager.SpaceScale = 1 / _mainPlanet.Data.Radius;

        _planetContext = GetNode<PlanetContextMenu>("PlanetContext");
        _planetContext.NewOrbit += NewOrbit;

        _asteroidSpawner = GetNode<AsteroidSpawner>("AsteroidSpawner");
        _asteroidSpawner.StartMission += StartMission;
    }

    private void StartMission(CelestialBody target)
    {
        if (MissionPlannerScene == null) return;

        var missionPlanner = MissionPlannerScene.Instantiate<MissionPlanner>();
        missionPlanner.Origin = _mainPlanet;
        missionPlanner.Target = target;
        AddChild(missionPlanner);
    }


    private void NewOrbit()
    {
        if (OrbitCreatorScene == null)
            return;

        var orbitCreator = OrbitCreatorScene.Instantiate<OrbitCreator>();
        orbitCreator.Target = _mainPlanet;
        AddChild(orbitCreator);
    }

}
