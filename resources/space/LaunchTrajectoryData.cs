using Godot;
using System;

public partial class LaunchTrajectoryData : OrbitData
{

    public override Vector3 PositionAtTrueAnomaly(float fi)
    {
        var aMax = 2;

        var speedStart = CentralBody.Radius * Mathf.Abs(ReferencePoint.Normalized().Dot(Vector3.Forward)) / CentralBody.RotationPeriod;
        var vStart = ReferencePoint.Normalized().Cross(Vector3.Down).Normalized() * speedStart / 1000;
        var vTarget = VelocityAtReferencePoint;
        var vDeltaTan = vTarget - vStart;

        // t_m = sqrt(2 * v_tang / a_max)
        var tM = Mathf.Sqrt(2 * vDeltaTan.Length() / aMax);


        var rMax = ReferencePoint.Length();
        var rMin = CentralBody.Radius / 2; // Nem kell /2
        var rDelta = rMax - rMin;

        var t = tM * (fi / Mathf.Pi + 1f) / 2f;

        var r = rMin + rDelta * t / tM;

        var aRad = CentralBody.StandardGravitationalParameter / r / r;
        var aTan = aMax - aRad;

        // aTan is quadratic, so vDeltaTan is the quadratic integral of aTan, aMax^3/3
        // vDeltaTan = (aMax * tM^2 - aMax * t0) / 2 - mu * tM / h^2 - mu * tM^2 / (h^2 * t0)
        // var t = vDeltaTan.Length() * 3 / aMax / aMax / aMax;

        var rY = r;
        var rX = t * t * aTan / 2;

        var angle = rX / rMax;

        var x = rY * (Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Cos(angle + ArgumentOfPeriapsis) - Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Sin(angle + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        var y = rY * Mathf.Sin(angle + ArgumentOfPeriapsis) * Mathf.Sin(Inclination);
        var z = rY * (Mathf.Sin(LongitudeOfAscendingNode) * Mathf.Cos(angle + ArgumentOfPeriapsis) + Mathf.Cos(LongitudeOfAscendingNode) * Mathf.Sin(angle + ArgumentOfPeriapsis) * Mathf.Cos(Inclination));
        return new Vector3(x, y, z);
    }
}