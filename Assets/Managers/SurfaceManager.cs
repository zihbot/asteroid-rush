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
        float distance = 0.5f;
        float R = 2f;
        int imax = Mathf.FloorToInt(Mathf.PI / (distance / R));
        for (int i = 0; i <= imax; i++)
        {
            float theta = Mathf.PI * i / imax;
            float r = (R * Mathf.Sin(theta));
            int jmax = Mathf.FloorToInt(2 * Mathf.PI * r / distance);
            if (jmax <= 0) {
                GameObject point = new GameObject("Point_"+i);
                point.transform.position = new Vector3(0, (R * Mathf.Cos(theta)), 0);
            }
            for (int j = 0; j < jmax; j++)
            {
                float phi = Mathf.PI * 2 * j / jmax;
                GameObject point = new GameObject("Point_"+i+"_"+j);
                point.transform.position = new Vector3(r * Mathf.Cos(phi), (R * Mathf.Cos(theta)), r * Mathf.Sin(phi));
            }
        }
    }
}

