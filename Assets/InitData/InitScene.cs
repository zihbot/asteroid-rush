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
                Orbit origin1 = new Orbit(GameManager.Instance.Center, .3f, .4f, new Vector3(0, 1, 1), new Vector3(-1, 0, 0));
                Orbit origin = new Orbit(GameManager.Instance.Center, .3f, .3f, new Vector3(1, 1, 0), new Vector3(0, 0, 1));
                Orbit destination = new Orbit(GameManager.Instance.Center, .6f, 1.5f, new Vector3(3f, 2f, 1f), new Vector3(-1, 0, 3));
                Debug.Log("origin1" + origin1);
                Debug.Log("origin" + origin);
                Debug.Log("destination" + destination);
                //Orbit transfer = OrbitPlanner.TransferOrbit(origin, destination);
                OrbitManager.Instance.Create(origin1);
                OrbitManager.Instance.Create(origin);
                OrbitManager.Instance.Create(destination);
                //OrbitManager.Instance.Create(transfer);
                return;
            default:
                return;
        };
    }
}
