using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelTextManager : MonoBehaviour
{
    public TextAsset levelTextFile; // Reference to the JSON file in the Inspector
    private LevelTextData levelTextData;
    public TextMeshProUGUI textMeshPro;
    public GameObject popup;
    private string currentSceneName;
    public bool isPopupClosed = true;


    private void Start()
    {
        popup.SetActive(false);

        if (levelTextFile != null)
        {
            string jsonString = levelTextFile.text;
            levelTextData = JsonUtility.FromJson<LevelTextData>(jsonString);
            SetLevelText("MainScene"); 
        }
    }

    private void Update()
    {
        if (popup != null)
        {
            isPopupClosed = !popup.activeSelf;
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
        if (!HasVisitedScene(scene.name) && scene.name != "MainScene")
        {
            popup.SetActive(true);
            MarkSceneVisited(scene.name);
        }
    }

    private bool HasVisitedScene(string sceneName)
    {
        return PlayerPrefs.HasKey(sceneName + "_Visited");
    }

    private void MarkSceneVisited(string sceneName)
    {
        PlayerPrefs.SetInt(sceneName + "_Visited", 1);
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
