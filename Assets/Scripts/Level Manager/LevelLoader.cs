using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string levelToLoadOnClick;
    private string continuouslyRunningSceneName = "Environment";
	[SerializeField] bool mainScreen;
	[SerializeField] bool sameLevel;

	public void Update() {

		if (mainScreen)
		{
			 levelToLoadOnClick = "MainScene";
		}
		else if (sameLevel)
		{
			levelToLoadOnClick = GetActiveSceneName();
		}

	}

	public void RestartLevel()
	{
		SceneManager.UnloadSceneAsync(levelToLoadOnClick);
		SceneManager.LoadSceneAsync(levelToLoadOnClick, LoadSceneMode.Additive);
	}

    public void LoadLevel()
    {
        Scene environmentScene = SceneManager.GetSceneByName(continuouslyRunningSceneName);
        Scene currentScene = SceneManager.GetSceneByName(levelToLoadOnClick);

        if (environmentScene.isLoaded)
        {
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene scene = SceneManager.GetSceneAt(i);
				if (scene != environmentScene)
				{
					SceneManager.UnloadSceneAsync(scene);
				}
			}
			if (!currentScene.isLoaded) {
            SceneManager.LoadSceneAsync(levelToLoadOnClick, LoadSceneMode.Additive);
			}
	
        }
        else
        {
            SceneManager.LoadSceneAsync(continuouslyRunningSceneName, LoadSceneMode.Additive);
			if (!currentScene.isLoaded && currentScene.buildIndex != -1)
			{
				SceneManager.LoadSceneAsync(levelToLoadOnClick, LoadSceneMode.Additive);
			}

		}
    }

	string GetActiveSceneName()
	{
    Scene activeScene = SceneManager.GetActiveScene();

			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene scene = SceneManager.GetSceneAt(i);

				if (scene != activeScene && scene.name != continuouslyRunningSceneName)
				{
					return scene.name;
				}
			}
    return null; // Return null if no other scene found
		}
	}
