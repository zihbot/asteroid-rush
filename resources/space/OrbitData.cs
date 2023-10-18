using Godot;
using System;

public partial class OrbitData : Resource
{
    [Export] public Vector3 Periapsis { get; private set; } = Vector3.Back;
    [Export] public Vector3 VelocityAtPeriapsis { get; private set; } = Vector3.Right;
    [Export] public CelestialBodyData CentralBody { get; private set; } = new CelestialBodyData();

    public float SemiMajorAxis { get; private set; } = 1;

    private float? _eccentricity = null;
    public float Eccentricity => _eccentricity ?? RecalculateEccentricity();
    public float Inclination { get; private set; } = 0;
    public float LongitudeOfAscendingNode { get; private set; } = 0;
    public float ArgumentOfPeriapsis { get; private set; } = 0;
    public float MeanAnomaly { get; private set; } = 0;
    public Vector3 SpecificAngularMomentum => Periapsis.Cross(VelocityAtPeriapsis);

    public OrbitData()
    {
    }

    protected float RecalculateEccentricity() {
        // epsilon = v^2 / 2 - mu / r
        var specificOrbitalEnergy = VelocityAtPeriapsis.LengthSquared() / 2 - CentralBody.StandardGravitationalParameter / Periapsis.Length();
        // Sqrt(1 + 2 * epsilon * h^2 / mu^2)
        _eccentricity = MathF.Sqrt(1 + 2 * specificOrbitalEnergy * SpecificAngularMomentum.LengthSquared() / CentralBody.StandardGravitationalParameter / CentralBody.StandardGravitationalParameter);
        return _eccentricity.Value;
    }

    public Vector3 PositionAtTrueAnomaly(float fi)
    {
        var r = SpecificAngularMomentum.Length() * SpecificAngularMomentum.Length() / CentralBody.StandardGravitationalParameter / (1 + Eccentricity * Mathf.Cos(fi));
        var x = r * (Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Cos(fi + ArgumentOfPeriapsis) - Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        var y = r * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Sin(Inclination);
        var z = r * (Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Cos(fi + ArgumentOfPeriapsis) + Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        return new Vector3(x, y, z);
    }

    public override string ToString()
    {
        return $"OrbitData: Periapsis: {Periapsis}, VelocityAtPeriapsis: {VelocityAtPeriapsis}, CentralBody: {CentralBody}, SemiMajorAxis: {SemiMajorAxis}, Eccentricity: {Eccentricity}, Inclination: {Inclination}, LongitudeOfAscendingNode: {LongitudeOfAscendingNode}, ArgumentOfPeriapsis: {ArgumentOfPeriapsis}, MeanAnomaly: {MeanAnomaly}, SpecificAngularMomentum: {SpecificAngularMomentum}";
    }
}
