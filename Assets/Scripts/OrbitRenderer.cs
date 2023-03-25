using UnityEngine;

public class OrbitRenderer : MonoBehaviour
{
    public Orbit orbit;
    private LineRenderer lineRenderer;

    private const int lineWidthFrames = 8;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.startWidth = .1f;
        //lineRenderer.endWidth = .1f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.positionCount = orbit.pointCount;
        lineRenderer.SetPositions(orbit.positions);

        Keyframe[] keyframes = new Keyframe[lineWidthFrames];
        for (int i = 0; i < lineWidthFrames; i++)
        {
            keyframes[i] = new Keyframe(1.0f * i / lineWidthFrames, 0.01f);
        }
        lineRenderer.widthCurve = new AnimationCurve(keyframes);
    }

    void Update()
    {
        Keyframe[] keyframes = new Keyframe[lineWidthFrames];
        for (int i = 0; i < lineWidthFrames; i++)
        {
            Vector3 pos = orbit.positions[(Orbit.resolution * i) / lineWidthFrames];
            //lineRenderer.widthCurve.keys[i] = new Keyframe(1.0f * i / lineWidthFrames, Vector3.Distance(Camera.main.transform.position, pos) / 100f);

            keyframes[i] = new Keyframe(1.0f * i / lineWidthFrames, Vector3.Distance(Camera.main.transform.position, pos) / 100f);
        }
        lineRenderer.widthCurve = new AnimationCurve(keyframes);
    }
}