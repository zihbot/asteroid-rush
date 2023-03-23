using UnityEngine;

public class OrbitRenderer : MonoBehaviour
{
    public Orbit orbit;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.positionCount = orbit.pointCount;
        lineRenderer.SetPositions(orbit.positions);
    }

    void Update()
    {
    }
}