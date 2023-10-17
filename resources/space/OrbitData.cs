using Godot;
using System;

public partial class OrbitData : Resource
{
    [Export] public Vector3 Periapsis { get; private set; } = Vector3.Back;
    [Export] public Vector3 VelocityAtPeriapsis { get; private set; } = Vector3.Right;
    [Export] public CelestialBodyData CentralBody { get; private set; } = new CelestialBodyData();

    public float SemiMajorAxis { get; private set; } = 1;
    public float Eccentricity { get; private set; } = 0;
    public float Inclination { get; private set; } = 0;
    public float LongitudeOfAscendingNode { get; private set; } = 0;
    public float ArgumentOfPeriapsis { get; private set; } = 0;
    public float MeanAnomaly { get; private set; } = 0;
    public Vector3 SpecificAngularMomentum => Periapsis.Cross(VelocityAtPeriapsis);

    public OrbitData()
    {
    }

    public Vector3 PositionAtTrueAnomaly(float fi)
    {
        var r = SpecificAngularMomentum.Length() * SpecificAngularMomentum.Length() / CentralBody.StandardGravitationalParameter / (1 + Eccentricity * Mathf.Cos(fi));
        var x = r * (Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Cos(fi + ArgumentOfPeriapsis) - Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        var y = r * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Sin(Inclination);
        var z = r * (Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Cos(fi + ArgumentOfPeriapsis) + Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        return new Vector3(x, y, z);
    }
}
