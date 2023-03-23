using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitter : MonoBehaviour
{
    public Orbit orbit;
    private float dPeriod = 20;
    private float dTime = 0; // Time since periapsis
    private float progress = 0; // main anomaly / 2PI

    void Start()
    {

    }

    void Update()
    {

        dTime += Time.deltaTime;
        if (dTime > dPeriod)
        {
            dTime -= dPeriod;
        }

        float pos = progress * Orbit.resolution;
        int posFloor = Mathf.FloorToInt(pos);
        int posCeil = posFloor + 1 >= orbit.pointCount ? 0 : posFloor + 1;

        Vector3 position = Vector3.Lerp(orbit.positions[posFloor], orbit.positions[posCeil], pos - posFloor);
        float radius = Mathf.Lerp(orbit.ldRadii[posFloor], orbit.ldRadii[posCeil], pos - posFloor);
        float speed = Mathf.Lerp(orbit.ldDSpeeds[posFloor], orbit.ldDSpeeds[posCeil], pos - posFloor);
        float angularSpeed = Mathf.Lerp(orbit.ldDAngularSpeeds[posFloor], orbit.ldDAngularSpeeds[posCeil], pos - posFloor);

        gameObject.transform.position = position;

        progress += 0.05f / radius / radius;
        if (progress >= 1)
        {
            progress -= Mathf.Floor(progress);
        }
    }
}
