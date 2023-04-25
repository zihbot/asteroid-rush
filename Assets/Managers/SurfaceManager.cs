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
            float theta = i * distance / R;
            float r = (R * Mathf.Sin(theta));
            int jmax = Mathf.FloorToInt(2 * Mathf.PI * r / distance);
            for (int j = 0; j < jmax; j++)
            {
                float phi = j * distance / r;
                GameObject point = new GameObject("Point");
                point.transform.position = new Vector3(r * Mathf.Cos(phi), (R * Mathf.Cos(theta)), r * Mathf.Sin(phi));
            }
        }
    }
}

