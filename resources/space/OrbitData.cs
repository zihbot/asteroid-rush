using Godot;
using System;

public partial class OrbitData : Resource
{
    [Export] public Vector3 ReferencePoint { get; private set; } = Vector3.Back;
    private Vector3 _velocityAtReferencePoint = Vector3.Right;
    [Export] public Vector3 VelocityAtReferencePoint { get => _velocityAtReferencePoint; set { _velocityAtReferencePoint = value; ResetValues(); } }
    [Export] public CelestialBodyData CentralBody { get; private set; } = new CelestialBodyData();

    public float SemiMajorAxis { get; private set; } = 1;

    private float? _eccentricity = null;
    public float Eccentricity => _eccentricity ?? RecalculateEccentricity();
    private Vector3? _rotations = null;
    public Vector3 Rotations => _rotations ?? RecalculateRotations();
    public float LongitudeOfAscendingNode => Rotations.X;
    public float Inclination => Rotations.Y;
    public float ArgumentOfPeriapsis => Rotations.Z;
    public Vector3 SpecificAngularMomentum => ReferencePoint.Cross(VelocityAtReferencePoint);

    public OrbitData()
    {
    }

    private void ResetValues()
    {
        _eccentricity = null;
        _rotations = null;
        EmitChanged();
    }

    protected float RecalculateEccentricity()
    {
        // epsilon = v^2 / 2 - mu / r
        var specificOrbitalEnergy = VelocityAtReferencePoint.LengthSquared() / 2 - CentralBody.StandardGravitationalParameter / ReferencePoint.Length();
        // Sqrt(1 + 2 * epsilon * h^2 / mu^2)
        _eccentricity = Mathf.Sqrt(1 + 2 * specificOrbitalEnergy * SpecificAngularMomentum.LengthSquared() / CentralBody.StandardGravitationalParameter / CentralBody.StandardGravitationalParameter);
        return _eccentricity.Value;
    }

    protected Vector3 RecalculateRotations()
    {
        var h = SpecificAngularMomentum;
        var n = Vector3.Up.Cross(h);

        var omega = n.Length() < Mathf.Epsilon ? 0 : Mathf.Acos(n.X / n.Length());
        if (n.Z < 0) omega = 2 * Mathf.Pi - omega;

        var i = Mathf.Acos(h.Y / h.Length());

        var argper = n.Length() < Mathf.Epsilon ? 0 : n.Dot(ReferencePoint) / n.Length() / ReferencePoint.Length();

        _rotations = new(omega, i, argper);
        return _rotations.Value;
    }

    public virtual Vector3 PositionAtTrueAnomaly(float fi)
    {
        var r = SpecificAngularMomentum.Length() * SpecificAngularMomentum.Length() / CentralBody.StandardGravitationalParameter / (1 + Eccentricity * Mathf.Cos(fi));
        var x = r * (Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Cos(fi + ArgumentOfPeriapsis) - Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        var y = r * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Sin(Inclination);
        var z = r * (Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Cos(fi + ArgumentOfPeriapsis) + Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        return new Vector3(x, y, z);
    }

    public override string ToString()
    {
        return $"OrbitData: Periapsis: {ReferencePoint}, VelocityAtPeriapsis: {VelocityAtReferencePoint}, CentralBody: {CentralBody}, SemiMajorAxis: {SemiMajorAxis}, Eccentricity: {Eccentricity}, Inclination: {Inclination}, LongitudeOfAscendingNode: {LongitudeOfAscendingNode}, ArgumentOfPeriapsis: {ArgumentOfPeriapsis}, SpecificAngularMomentum: {SpecificAngularMomentum}";
    }
}
