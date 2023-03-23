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

    public CelestialBody center;
    public float eccentricity;
    public float ldSemiMajorAxis;
    public Vector2 orbitalPlane;
    public float argumentOfPeriapsis;

    public Vector3[] positions;
    public float[] ldRadii;
    public float[] ldDSpeeds;
    public float[] ldDAngularSpeeds;

    void Awake()
    {
        positions = new Vector3[resolution];
        ldRadii = new float[resolution];
        ldDSpeeds = new float[resolution];
        ldDAngularSpeeds = new float[resolution];
        pointCount = resolution;

        float stdGravParam = center.emMass * ldEmDGravitationalConst;
        float reciprocalSemiMajorAxis = 1f / ldSemiMajorAxis;
        for (int i = 0; i < resolution; i++)
        {
            float trueAnomaly = 2 * Mathf.PI * i / resolution;
            float r = ldSemiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Mathf.Cos(trueAnomaly));
            if (r > 1000)
            {
                pointCount = i;
                break;
            }
            ldRadii[i] = r;
            ldDSpeeds[i] = stdGravParam * (2f/r - reciprocalSemiMajorAxis);
            positions[i] = new Vector3(Mathf.Sin(trueAnomaly), 0, Mathf.Cos(trueAnomaly)) * r;
        }
    }
}