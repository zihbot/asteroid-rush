using UnityEngine;

public class Orbit : MonoBehaviour
{
    // 1 ld = 3.844 * 10^8 m
    // 1 em = 5.972 * 10^24 kg
    // 1 d  = 8.640 * 10^4 s

    //   6.674 * 10^−11 m * m * m / kg / s / s =
    // = 6.674 / 3.844 / 3.844 / 3.844 * 5.972 * 8.64 * 8.64 * 10^(-11-8-8-8+24+4+4) =
    // = 0.05238215355 ld * ld * ld / em / d / d
    public const float ldEmDgravitationalConst = 0.05238f;

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
            float trueAnomaly = 2 * Mathf.PI * i / resolution;
            float r = LdSemiMajorAxis * (1 - Eccentricity * Eccentricity) / (1 + Eccentricity * Mathf.Cos(trueAnomaly));
            if (r > 1000)
            {
                lineRenderer.positionCount = i;
                break;
            }
            radii[i] = r;
            positions[i] = new Vector3(Mathf.Sin(trueAnomaly), 0, Mathf.Cos(trueAnomaly)) * r;
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