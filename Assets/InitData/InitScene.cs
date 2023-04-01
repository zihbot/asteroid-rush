using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InitScene : MonoBehaviour
{
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitScenes()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "PlanetScene":
                Orbit origin = new Orbit(GameManager.Instance.Center, .5f, .5f);
                Orbit destination = new Orbit(GameManager.Instance.Center, .8f, 1f);
                Orbit transfer = OrbitPlanner.TransferOrbit(origin, destination);
                new GameObject("Init");
                OrbitManager.Instance.Create(origin);
                OrbitManager.Instance.Create(destination);
                OrbitManager.Instance.Create(transfer);
                return;
            default:
                new GameObject("Default");
                return;
        };
    }
}
