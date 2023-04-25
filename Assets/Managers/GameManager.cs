using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        init();
    }

    public CelestialBody Center {
        get {
            return GameObject.FindObjectOfType<CelestialBody>();
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMain()
    {
        GameObject main = new GameObject("GameManager");
        GameObject.DontDestroyOnLoad(main);
        main.AddComponent<GameManager>();
    }

    void init()
    {
        gameObject.AddComponent<OrbitManager>();
        gameObject.AddComponent<PositionManager>();
        gameObject.AddComponent<SurfaceManager>();
    }

    private void Start() {
        InitScene.InitScenes();
    }
}
