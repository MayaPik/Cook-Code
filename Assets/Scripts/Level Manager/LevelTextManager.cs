using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelTextManager : MonoBehaviour
{
    public TextAsset levelTextFile; // Reference to the JSON file in the Inspector
    private LevelTextData levelTextData;
    public TextMeshProUGUI textMeshPro;
    private string currentSceneName;

    private void Start()
    {
        if (levelTextFile != null)
        {
            string jsonString = levelTextFile.text;
            levelTextData = JsonUtility.FromJson<LevelTextData>(jsonString);
            SetLevelText("MainScene"); 
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetLevelText(scene.name);
    }

    public void SetLevelText(string levelTag)
    {
        if (levelTextData != null)
        {
            foreach (LevelText levelText in levelTextData.levels)
            {
                if (levelText.level == levelTag)
                {
                    textMeshPro.text = levelText.text;
                    return;
                }
            }
            textMeshPro.text = "Level text not found";
        }
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
    public string level;
    public string text;
}
