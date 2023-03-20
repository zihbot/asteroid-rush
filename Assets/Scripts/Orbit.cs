using UnityEngine;

public class Orbit : MonoBehaviour
{
    private const int resolution = 720;

    public CelestialBody Center;
    public float Eccentricity;
    public float LdSemiMajorAxis;
    public Vector2 OrbitalPlane;
    public float ArgumentOfPeriapsis;

    Vector3[] positions;

    private bool draw = true;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.positionCount = resolution;

        positions = new Vector3[resolution];
        for (int i = 0; i < resolution; i++) {
            float theta = 2 * Mathf.PI * i / resolution;
            float r = LdSemiMajorAxis * (1 - Eccentricity * Eccentricity) / (1 + Eccentricity * Mathf.Cos(theta));
            if (r > 1000) {
                lineRenderer.positionCount = i;
                break;
            }
            positions[i] = new Vector3(Mathf.Sin(theta), 0, Mathf.Cos(theta)) * r;
        }

        lineRenderer.SetPositions(positions);
    }

    void Update()
    {
    }
}