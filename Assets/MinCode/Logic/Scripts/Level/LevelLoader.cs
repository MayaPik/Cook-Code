using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// A script used to load the player's level prefab
/// </summary>
public class LevelLoader : BehaviorBase
{
    private const string CurrentLevelDbArgument = "CurrentLevel";
    private const string ResourcesFolderName = "Levels";
    private const string LevelPrefabPrefix = "Level";
    public Transform levelParent;
    public Vector3Reference levelSpawnPosition;
    public BooleanVariable lastLevelCompletedVariable;
    public IntegerVariable currentLevelVaribale;
    public GameEvent levelReadyEvent;
    public GameEvent levelRestartedEvent;
    [Header("Set as a number greater than 0 to load a specific level number. Set to 0 to load player's level")]
    public int levelToLoad;
    [Header("true - load level, false - don't load anything (use level in scene)")]
    public bool loadLevel;
    private bool isFirstLevelOnSession = true;
    private bool wasLevelChanged = false;
    private GameObject lastLevelLoaded;
    private int maxLevel;

    private void Awake()
    {
        SetMaxLevel();

        var isInDebug = false;
#if DEBUG
        isInDebug = true;
#endif
        if (!isInDebug)
        {
            levelToLoad = 0;
            loadLevel = true;
        }
    }

    private void SetMaxLevel()
    {
        var allResources = Resources.LoadAll(ResourcesFolderName);

        maxLevel = 0;
        foreach (var resource in allResources)
        {
            if (resource.name.Contains(LevelPrefabPrefix))
            {
                var splitLevelName = resource.name.Split(new string[] { LevelPrefabPrefix }, StringSplitOptions.RemoveEmptyEntries);
                if (splitLevelName.Length == 1
                    && int.TryParse(splitLevelName[0], out var levelNumber)
                    && levelNumber > maxLevel)
                {
                    maxLevel = levelNumber;
                }
            }
        }
    }

    private void OnEnable()
    {
        if (loadLevel)
        {
            if (isFirstLevelOnSession)
            {
                CloseAllOpenedLevels();
                LoadPlayerLevel();
            }
        }
        else
        {
            levelReadyEvent.RaiseIfNotNull();
        }
    }

    public void Restart()
    {
        LoadLevelPrefab(currentLevelVaribale);
        levelRestartedEvent.RaiseIfNotNull();
    }

    public void LoadPlayerLevel()
    {
        if (levelToLoad > 0 && isFirstLevelOnSession)
        {
            LoadLevelPrefab(levelToLoad);
        }
        else if (loadLevel)
        {
            if (isFirstLevelOnSession || wasLevelChanged)
            {
                var currentLevel = PlayerPrefs.GetInt(CurrentLevelDbArgument, 0);

                if (currentLevel < 0)
                {
                    currentLevel = 0;
                }

                SetPlayerLevelDataTo(currentLevel);
                LoadLevelPrefab(currentLevel);
            }
        }
        else
        {
            levelReadyEvent.RaiseIfNotNull();
        }
    }

    public void OnLoadLevelRequest()
    {
        LoadPlayerLevel();

        isFirstLevelOnSession = false;
    }

    public void IncreaseLevel()
    {
        wasLevelChanged = true;
        isFirstLevelOnSession = false;
        var currentLevel = PlayerPrefs.GetInt(CurrentLevelDbArgument);
        currentLevel++;

        SetPlayerLevelDataTo(currentLevel);
    }

    public void CloseAllOpenedLevels()
    {
        if (levelParent != null)
        {
            for (int i = 0; i < levelParent.childCount; i++)
            {
                Destroy(levelParent.GetChild(i).gameObject);
            }
        }
        else
        {
            var sceneObjects =
                FindObjectsOfType<GameObject>()
                .Where(obj => obj != null
                    && obj.activeInHierarchy
                    && obj.name.Contains(LevelPrefabPrefix));

            foreach (var obj in sceneObjects)
            {
                var splitByLevel = obj.name.Split(new string[] { LevelPrefabPrefix }, System.StringSplitOptions.RemoveEmptyEntries);

                if (splitByLevel.Length == 1)
                {
                    if (int.TryParse(splitByLevel[0], out _))
                    {
                        Destroy(obj);
                    }
                }
            }
        }
    }

    private void DestroyPreviousLevel()
    {
        if (lastLevelLoaded != null)
        {
            Destroy(lastLevelLoaded);
        }
    }

    private void LoadLevelPrefab(int levelNumber)
    {
        DestroyPreviousLevel();

        var prefab = LoadLevelPrefabFromResources(levelNumber);

        if (prefab != null)
        {
            lastLevelLoaded = Instantiate(prefab) as GameObject;
            lastLevelLoaded.name = prefab.name;
            lastLevelLoaded.transform.position = levelSpawnPosition;
            if (levelParent != null)
            {
                lastLevelLoaded.transform.SetParent(levelParent);
            }
        }
        else
        {
            if (levelNumber == 1)
            {
                throw new Exception("FATAL: There is no prefab for level 1!");
            }

            SetPlayerLevelDataTo(1);
            LoadLevelPrefab(1);
        }

        levelReadyEvent.RaiseIfNotNull();
    }

    private Object LoadLevelPrefabFromResources(int levelNumber)
    {
        var levelNumberToLoad = levelNumber;

        if (levelNumber > maxLevel)
        {
            levelNumberToLoad = levelNumber % maxLevel;
        }

        if (levelNumberToLoad == 0 && levelNumber != 0)
        {
            levelNumberToLoad = maxLevel;
        }

        var levelName = $"{ResourcesFolderName}/{LevelPrefabPrefix}{levelNumberToLoad}";

        return Resources.Load(levelName);
    }

    private void SetPlayerLevelDataTo(int levelNumber)
    {
        PlayerPrefs.SetInt(CurrentLevelDbArgument, levelNumber);
        currentLevelVaribale.SetValue(levelNumber);
    }
}
