using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitManager : MonoBehaviour
{
    public static OrbitManager Instance { get; private set; }
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

    private List<Orbit> orbits = new List<Orbit>();
    private List<GameObject> orbitRenderers = new List<GameObject>();

    void Init()
    {

    }

    public Orbit Create(Orbit orbit)
    {
        orbits.Add(orbit);

        GameObject orbitRenderer = new GameObject("OrbitRenderer");
        orbitRenderers.Add(orbitRenderer);
        OrbitRenderer orComponent = orbitRenderer.AddComponent<OrbitRenderer>();
        orComponent.orbit = orbit;
        return orbit;
    }
}
