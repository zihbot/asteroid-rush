using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    #region Singleton
    public static SurfaceManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        Init();
    }
    #endregion

    void Init()
    {
        createExample();
    }

    private void createExample()
    {
        List<CenterPoint> points = new List<CenterPoint>();
        float distance = 0.5f;
        float R = 2f;
        int imax = Mathf.FloorToInt(Mathf.PI / (distance / R));
        imax += imax % 2 == 1 ? 1 : 0;
        for (int i = 0; i <= imax; i++)
        {
            float theta = Mathf.PI * i / imax;
            float r = (R * Mathf.Sin(theta));
            int jmax = Mathf.FloorToInt(2 * Mathf.PI * r / distance);
            if (jmax <= 0)
            {
                Vector3 pos = new Vector3(0, (R * Mathf.Cos(theta)), 0);
                points.Add(new CenterPoint { Position = pos, Phi = 0, Theta = theta, R = R, I = i, J = 0, Imax = imax, Jmax = 1 });
            }
            for (int j = 0; j < jmax; j++)
            {
                float phi = Mathf.PI * 2 * j / jmax + theta;
                Vector3 pos = new Vector3(r * Mathf.Cos(phi), (R * Mathf.Cos(theta)), r * Mathf.Sin(phi));
                points.Add(new CenterPoint { Position = pos, Phi = phi, Theta = theta, R = R, I = i, J = j, Imax = imax, Jmax = jmax });
            }
        }

        foreach (CenterPoint cp in points)
        {
            GameObject point = new GameObject("Point_" + cp.I + "_" + cp.J);
            point.transform.position = cp.Position;

            if (cp.I <= cp.Imax / 2)
            {
                var (p, next, above) = FindTriangle(cp, points, true);
                if (next != null && above != null)
                {
                    DrawTriangle(p.Position, next.Position, above.Position);
                }
            }
            if (cp.I >= cp.Imax / 2)
            {
                var (p, next, above) = FindTriangle(cp, points, false);
                if (next != null && above != null)
                {
                    DrawTriangle(p.Position, next.Position, above.Position);
                }
            }
        }
    }

    (CenterPoint, CenterPoint, CenterPoint) FindTriangle(CenterPoint cp, List<CenterPoint> points, bool up = true)
    {
        CenterPoint next = points.Find(c => c.I == cp.I && c.J == (cp.J == cp.Jmax - 1 ? 0 : cp.J + 1));
        float nextPhi = next == null ? cp.Phi : (next.Phi < cp.Phi ? next.Phi + 2 * Mathf.PI : next.Phi);
        float phi = cp.J == cp.Jmax ? cp.Phi - 2 * Mathf.PI : cp.Phi;
        CenterPoint above = points.Find(c => c.I == cp.I + (up ? -1 : 1) && ((c.Phi >= cp.Phi && c.Phi < nextPhi) || (c.Phi >= phi && c.Phi < next.Phi)));
        if (above == null)
        {
            CenterPoint closest = null;
            float minDiff = Mathf.Infinity;
            foreach (var c in points.FindAll(c => c.I == cp.I + (up ? -1 : 1)))
            {
                float diff1 = Mathf.Abs(c.Phi - (nextPhi + cp.Phi) / 2);
                float diff2 = Mathf.Abs(c.Phi + 2 * Mathf.PI - (nextPhi + cp.Phi) / 2);
                float diff = Mathf.Min(diff1, diff2);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    closest = c;
                }
            }
            above = closest;
        }
        return (cp, next, above);
    }

    void DrawTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        Debug.DrawLine(a, b, Color.green, 120);
        Debug.DrawLine(b, c, Color.green, 120);
        Debug.DrawLine(c, a, Color.green, 120);
    }

    class CenterPoint
    {
        public Vector3 Position { get; set; }
        public float Phi { get; set; }
        public float Theta { get; set; }
        public float R { get; set; }
        public int I { get; set; }
        public int J { get; set; }
        public int Imax { get; set; }
        public int Jmax { get; set; }
    }
}

