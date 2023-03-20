using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitter : MonoBehaviour
{
    public Orbit Orbit;
    public CelestialBody Center;
    public Vector3 LdPeriapsis = Vector3.back;
    public Vector3 KmSVelocity = Vector3.zero;
    public float Eccentricity = 1f;

    private float DPeriod = 300;
    private float DTime = 0; // Time since periapsis

    void Start()
    {

    }

    void Update()
    {
        Vector3 kmMove = Time.deltaTime * KmSVelocity;
        gameObject.transform.position += kmMove;

        DTime += Time.deltaTime;
        if (DTime > DPeriod) {
            DTime -= DPeriod;
        }
        float M = DTime/DPeriod;
    }
}
