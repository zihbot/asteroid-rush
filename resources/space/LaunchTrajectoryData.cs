using Godot;
using System;

public partial class LaunchTrajectoryData : OrbitData
{

    public override Vector3 PositionAtTrueAnomaly(float fi)
    {
        var aMax = 2;

        var rMax = ReferencePoint.Length();
        var rMin = CentralBody.Radius / 2; // Nem kell /2
        var rDelta = rMax - rMin;

        var r = rMin + rDelta * (fi/Mathf.Pi + 1f) / 2f;

        var aRad = CentralBody.StandardGravitationalParameter / r / r;
        var aTan = aMax - aRad;

        var speedStart = CentralBody.Radius * Mathf.Abs(ReferencePoint.Normalized().Dot(Vector3.Forward)) / CentralBody.RotationPeriod;
        var vStart = ReferencePoint.Normalized().Cross(Vector3.Down).Normalized() * speedStart / 1000;
        var vTarget = VelocityAtReferencePoint;
        var vDeltaTan = vTarget - vStart;

        // aTan is quadratic, so vDeltaTan is the quadratic integral of aTan, aMax^3/3
        // vDeltaTan = aMax^3/3 * t
        var t = vDeltaTan.Length() * 3 / aMax / aMax / aMax;

        GD.Print($"t: {t}");

        var x = r * (Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Cos(fi + ArgumentOfPeriapsis) - Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        var y = r * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Sin(Inclination);
        var z = r * (Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Cos(fi + ArgumentOfPeriapsis) + Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Sin(fi + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        return new Vector3(x, y, z);
    }
}