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
                Orbit origin = new Orbit(GameManager.Instance.Center, .2f, .3f);
                Orbit destination = new Orbit(GameManager.Instance.Center, .6f, 1.5f);
                Orbit transfer = OrbitPlanner.TransferOrbit(origin, destination);
                OrbitManager.Instance.Create(origin);
                OrbitManager.Instance.Create(destination);
                OrbitManager.Instance.Create(transfer);
                return;
            default:
                return;
        };
    }
}
