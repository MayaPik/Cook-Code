using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string levelToLoadOnClick;
    private string continuouslyRunningSceneName = "Environment";
	[SerializeField] bool mainScreen;

	public void Update() {
		if (mainScreen)
		{
			 levelToLoadOnClick = "MainScene";
		}
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
			if (!currentScene.isLoaded) 
			{
				SceneManager.LoadSceneAsync(levelToLoadOnClick, LoadSceneMode.Additive);
			}   
		}
    }
}
