using UnityEngine;

public class OrbitPlanner : MonoBehaviour
{
    public Orbit origin;
    public Orbit destination;
    private Orbit transfer;
    private OrbitRenderer transferRenderer;

    void Start()
    {
        transfer = gameObject.AddComponent<Orbit>();
        transfer.center = origin.center;
        transfer.eccentricity = 0.9f;
        float destinationTheta = 0.5f;
        int destinationThetaRes = Mathf.FloorToInt(destinationTheta * Orbit.resolution);

        float radiusPeriapsis = origin.ldRadii[0];
        float radiusApoapsis = destination.ldRadii[destinationThetaRes];
        float majorAxis = radiusPeriapsis + radiusApoapsis;
        transfer.ldSemiMajorAxis = majorAxis / 2f;
        transfer.eccentricity = (radiusApoapsis - radiusPeriapsis) / majorAxis;
        transfer.Setup();

        transferRenderer = gameObject.AddComponent<OrbitRenderer>();
        transferRenderer.orbit = transfer;
    }

    void Update()
    {
    }
}