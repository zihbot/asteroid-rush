using Godot;
using System;

public partial class PlanetarySystem : Node3D
{
    [Export] public PackedScene OrbitCreatorScene;

    public override void _Ready()
    {
        var planet = GetChild<Planet>(0);
        planet.PlanetClicked += () =>
        {
            var planetContext = GetNode<PlanetContextMenu>("PlanetContext");
            planetContext.Show();
            planetContext.NewOrbit += NewOrbit;
        };
    }

    private void NewOrbit()
    {
        if (OrbitCreatorScene == null)
            return;

        var orbitCreator = OrbitCreatorScene.Instantiate<OrbitCreator>();
        orbitCreator.Target = GetNode<Planet>("Planet");
        AddChild(orbitCreator);
    }

}
