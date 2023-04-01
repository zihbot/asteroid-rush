using UnityEngine;

public static class OrbitPlanner
{

    public static Orbit TransferOrbit(Orbit origin, Orbit destination)
    {
        CelestialBody center = GameManager.Instance.Center;
        float destinationTheta = 0.5f;
        int destinationThetaRes = Mathf.FloorToInt(destinationTheta * Orbit.resolution);

        float radiusPeriapsis = origin.ldRadii[0];
        float radiusApoapsis = destination.ldRadii[destinationThetaRes];
        float majorAxis = radiusPeriapsis + radiusApoapsis;
        float ldSemiMajorAxis = majorAxis / 2f;
        float eccentricity = (radiusApoapsis - radiusPeriapsis) / majorAxis;
        Orbit transfer = new Orbit(center, eccentricity, ldSemiMajorAxis);

        return transfer;
    }
}