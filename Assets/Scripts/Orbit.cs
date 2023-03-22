using UnityEngine;

public class Orbit : MonoBehaviour
{
    public const int resolution = 720;

    public CelestialBody Center;
    public float Eccentricity;
    public float LdSemiMajorAxis;
    public Vector2 OrbitalPlane;
    public float ArgumentOfPeriapsis;

    public Vector3[] positions;
    public float[] radii;

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
        radii = new float[resolution];
        for (int i = 0; i < resolution; i++)
        {
            float theta = 2 * Mathf.PI * i / resolution;
            float r = LdSemiMajorAxis * (1 - Eccentricity * Eccentricity) / (1 + Eccentricity * Mathf.Cos(theta));
            if (r > 1000)
            {
                lineRenderer.positionCount = i;
                break;
            }
            radii[i] = r;
            positions[i] = new Vector3(Mathf.Sin(theta), 0, Mathf.Cos(theta)) * r;
        }

        lineRenderer.SetPositions(positions);
    }

    void Update()
    {
    }

    public Vector3 getPosition(float progress)
    {
        float pos = progress * resolution;
        int floor = Mathf.FloorToInt(pos);
        return Vector3.Lerp(positions[floor], positions[(floor + 1 >= resolution ? 0 : floor + 1)], pos - floor);
    }
}