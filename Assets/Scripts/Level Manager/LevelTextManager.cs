using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelTextManager : MonoBehaviour
{
    public string levelTextFilePath = "/Assets/Scripts/levels_info.json";
    private LevelTextData levelTextData;
    private string continuouslyRunningSceneName = "Environment";

    public TextMeshProUGUI textMeshPro;
    string currentSceneName;

    void Start()
    {
        currentSceneName = GetActiveSceneName();
        string jsonString = File.ReadAllText(levelTextFilePath);
        levelTextData = JsonUtility.FromJson<LevelTextData>(jsonString);
        SetLevelText(currentSceneName);
    }

    public void SetLevelText(string levelTag)
    {
        foreach (LevelText levelText in levelTextData.levels)
        {
            if (levelText.level?.ToString() == levelTag)
            {
                textMeshPro.text = levelText.text;
                return;
            }
        }

        // If level text is not found, display a default message
        textMeshPro.text = "Level text not found";
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
    return activeScene.name;
		}
	
}

[System.Serializable]
public class LevelTextData
{
    public LevelText[] levels;
}

[System.Serializable]
public class LevelText
{
    public string level; // Use the object type to accommodate both string and integer values
    public string text;
}

