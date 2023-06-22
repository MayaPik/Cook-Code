using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelTextManager : MonoBehaviour
{
    public string levelTextFilePath = "Assets/Scripts/levels_info.json";
    private LevelTextData levelTextData;
    public TextMeshProUGUI textMeshPro;
    string currentSceneName;

    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        string jsonString = File.ReadAllText(levelTextFilePath);
        levelTextData = JsonUtility.FromJson<LevelTextData>(jsonString);
        SetLevelText(currentSceneName);
    }

    public void SetLevelText(string levelTag)
    {
        foreach (LevelText levelText in levelTextData.levels)
        {
            if (levelText.level.ToString() == levelTag)
            {
                textMeshPro.text = levelText.text;
                return;
            }
        }

        // If level text is not found, display a default message
        textMeshPro.text = "Level text not found";
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
    public object level; // Use the object type to accommodate both string and integer values
    public string text;
}

