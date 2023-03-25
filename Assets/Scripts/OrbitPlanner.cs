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
        transfer.ldSemiMajorAxis = 1;
        transfer.Setup();

        transferRenderer = gameObject.AddComponent<OrbitRenderer>();
        transferRenderer.orbit = transfer;
    }

    void Update()
    {
    }
}