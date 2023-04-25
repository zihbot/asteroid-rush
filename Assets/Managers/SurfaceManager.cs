using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
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

    void Init()
    {

    }

}
