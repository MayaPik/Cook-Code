using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvRunning : MonoBehaviour
{
    private const string continuouslyRunningSceneName = "Environment";
    private static bool sceneLoaded = false;

    private void Start()
    {
        if (!sceneLoaded) {
        gameObject.GetComponent<LevelLoader>().LoadLevel();
        sceneLoaded = true;
        }
    }
}
