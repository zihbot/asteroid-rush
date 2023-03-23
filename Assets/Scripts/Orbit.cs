using UnityEngine;

public class Orbit : MonoBehaviour
{
    // 1 ld = 3.844 * 10^8 m
    // 1 em = 5.972 * 10^24 kg
    // 1 d  = 8.640 * 10^4 s

    //   6.674 * 10^−11 m * m * m / kg / s / s =
    // = 6.674 / 3.844 / 3.844 / 3.844 * 5.972 * 8.64 * 8.64 * 10^(-11-8-8-8+24+4+4) =
    // = 0.05238215355 ld * ld * ld / em / d / d
    public const float ldEmDGravitationalConst = 0.05238f;

    public const int resolution = 720;
    public int pointCount = 0;

    public CelestialBody Center;
    public float Eccentricity;
    public float LdSemiMajorAxis;
    public Vector2 OrbitalPlane;
    public float ArgumentOfPeriapsis;

    public Vector3[] positions;
    public float[] radii;

    void Start()
    {
        positions = new Vector3[resolution];
        radii = new float[resolution];
        pointCount = resolution;
        for (int i = 0; i < resolution; i++)
        {
            float trueAnomaly = 2 * Mathf.PI * i / resolution;
            float r = LdSemiMajorAxis * (1 - Eccentricity * Eccentricity) / (1 + Eccentricity * Mathf.Cos(trueAnomaly));
            if (r > 1000)
            {
                pointCount = i;
                break;
            }
            radii[i] = r;
            positions[i] = new Vector3(Mathf.Sin(trueAnomaly), 0, Mathf.Cos(trueAnomaly)) * r;
        }
    }

}