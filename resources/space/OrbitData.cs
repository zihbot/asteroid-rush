using Godot;
using System;

public partial class OrbitData : Resource
{
    public float SemiMajorAxis { get; private set; } = 1;
    public float Eccentricity { get; private set; } = 0;
    public float Inclination { get; private set; } = 0;
    public float LongitudeOfAscendingNode { get; private set; } = 0;
    public float ArgumentOfPeriapsis { get; private set; } = 0;
    public float MeanAnomaly { get; private set; } = 0;

    [Export] public CelestialBodyData CentralBody { get; private set; } = null;

    public OrbitData()
    {
    }


}
