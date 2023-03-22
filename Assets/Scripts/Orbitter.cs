using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitter : MonoBehaviour
{
    public Orbit Orbit;
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

        float radius = Mathf.Lerp(Orbit.radii[posFloor], Orbit.radii[(posFloor + 1 >= Orbit.resolution ? 0 : posFloor + 1)], pos - posFloor);
        gameObject.transform.position = Vector3.Lerp(Orbit.positions[posFloor], Orbit.positions[(posFloor + 1 >= Orbit.resolution ? 0 : posFloor + 1)], pos - posFloor);

        progress += 0.05f / radius / radius;
        if (progress >= 1)
        {
            progress -= Mathf.Floor(progress);
        }
    }
}
