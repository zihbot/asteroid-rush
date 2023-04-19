using UnityEngine;

public class Orbit
{
    // 1 ld = 3.844 * 10^8 m
    // 1 em = 5.972 * 10^24 kg
    // 1 d  = 8.640 * 10^4 s

    //   6.674 * 10^−11 m * m * m / kg / s / s =
    // = 6.674 / 3.844 / 3.844 / 3.844 * 5.972 * 8.64 * 8.64 * 10^(-11-8-8-8+24+4+4) =
    // = 0.05238215355 ld * ld * ld / em / d / d
    public const float ldEmDGravitationalConst = 0.05238f;
    public static Vector3 referenceDirection = Vector3.forward;

    public const int resolution = 720;
    public int pointCount = 0;

    public CelestialBody center;
    public float eccentricity;
    public float ldSemiMajorAxis;
    public float inclination;
    public float longitudeOfAscendingNode;
    public float argumentOfPeriapsis;

    public Vector3 specificAngularMomentum;
    public float stdGravParam;
    public Vector3[] positions;
    public float[] ldRadii;
    public float[] ldDSpeeds;
    public float[] frequencies;

    public Orbit() : this(null, 0f, 0f)
    {
    }

    public Orbit(CelestialBody center, float eccentricity, float ldSemiMajorAxis)
        : this(center, eccentricity, ldSemiMajorAxis, Vector3.forward, Vector3.left)
    { }

    public Orbit(CelestialBody center, float eccentricity, float ldSemiMajorAxis, Vector3 periapsisDirection, Vector3 periapsisVelocityDirection)
    {
        this.center = center;
        this.eccentricity = eccentricity;
        this.ldSemiMajorAxis = ldSemiMajorAxis;

        periapsisDirection = periapsisDirection.normalized;
        periapsisVelocityDirection = periapsisVelocityDirection.normalized;

        this.stdGravParam = center.emMass * ldEmDGravitationalConst;
        Vector3 h = Vector3.Cross(periapsisVelocityDirection, periapsisDirection).normalized * _radius(0f) * _speed(_radius(0f));
        this.specificAngularMomentum = h;

        inclination = Mathf.Acos(h.y / h.magnitude);

        Vector3 n = Vector3.Cross(h, Vector3.up);
        Debug.Log("h: " + h);
        Debug.Log("n: " + n);
        Debug.Log("periapsisDirection: " + periapsisDirection);
        Debug.Log("periapsisVelocityDirection: " + periapsisVelocityDirection);
        if (inclination == 0 || n.magnitude == 0)
        {
            n = referenceDirection;
            longitudeOfAscendingNode = 0;
        }
        else if (n.x >= 0)
        {
            longitudeOfAscendingNode = 2 * Mathf.PI - Mathf.Acos(n.z / n.magnitude);
        }
        else
        {
            longitudeOfAscendingNode = Mathf.Acos(n.z / n.magnitude);
        }

        argumentOfPeriapsis = Vector3.Angle(n, periapsisDirection) * Mathf.PI / 180f;

        Setup();
    }

    public void Setup()
    {
        positions = new Vector3[resolution];
        ldRadii = new float[resolution];
        ldDSpeeds = new float[resolution];
        frequencies = new float[resolution];
        pointCount = resolution;

        float reciprocalSemiMajorAxis = 1f / ldSemiMajorAxis;
        Quaternion rotation = _localRotation();
        for (int i = 0; i < resolution; i++)
        {
            float trueAnomaly = 2 * Mathf.PI * i / resolution;
            float r = _radius(trueAnomaly);
            if (r > 1000)
            {
                pointCount = i;
                break;
            }
            ldRadii[i] = r;
            ldDSpeeds[i] = _speed(r, reciprocalSemiMajorAxis);
            frequencies[i] = ldDSpeeds[i] / r / 2 / Mathf.PI;
            positions[i] = rotation * _localPosition(trueAnomaly, r);
        }
        Debug.Log("periapsis: " + positions[0]);
    }

    private float _radius(float trueAnomaly)
    {
        return ldSemiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Mathf.Cos(trueAnomaly));
    }
    private float _speed(float r, float reciprocalSemiMajorAxis)
    {
        return Mathf.Sqrt(stdGravParam * (2f / r - reciprocalSemiMajorAxis));
    }
    private float _speed(float r)
    {
        return _speed(r, 1f / ldSemiMajorAxis);
    }
    private Vector3 _localPosition(float trueAnomaly, float r)
    {
        return new Vector3(Mathf.Sin(trueAnomaly), 0, Mathf.Cos(trueAnomaly)) * r;
    }
    private Quaternion _localRotation()
    {
        Quaternion result = Quaternion.Euler(0, argumentOfPeriapsis * -180f / Mathf.PI, 0);
        result = Quaternion.Euler(0, longitudeOfAscendingNode * -180f / Mathf.PI, 0) * result;
        result = Quaternion.Euler(inclination * -180f / Mathf.PI, 0, 0) * result;
        /*Quaternion result = Quaternion.Euler(0, longitudeOfAscendingNode * -180f / Mathf.PI, 0);//
        result = Quaternion.Euler(inclination * -180f / Mathf.PI, 0, 0) * result;
        result = Quaternion.Euler(0, argumentOfPeriapsis * -180f / Mathf.PI, 0) * result;*/
        return result;
    }

    public override string ToString()
    {
        return string.Format("Orbit[a={0}; e={1}; asc={2}; inc={3}; per={4}]", ldSemiMajorAxis, eccentricity, longitudeOfAscendingNode, inclination, argumentOfPeriapsis);
    }
}