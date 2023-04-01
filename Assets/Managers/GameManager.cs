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
        initManagers();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadMain()
    {
        GameObject main = new GameObject("GameManager");
        GameObject.DontDestroyOnLoad(main);
        main.AddComponent<GameManager>();
    }

    void initManagers()
    {
        gameObject.AddComponent<OrbitManager>();
    }
}
