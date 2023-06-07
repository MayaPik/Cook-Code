using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads the different scenes that makes the game to play
/// </summary>
public class ScenesLoader : MonoBehaviour
{
    public GameEvent eventWhenSceneLoaded;
    public GameEvent eventWhenAllScenesLoaded;
    public List<string> sceneNamesToLoad;
    public string levelSceneName;

    void Start()
    {
        if (!sceneNamesToLoad.Contains(levelSceneName))
        {
            sceneNamesToLoad.Add(levelSceneName);
        }

        LoadScenes();
    }

    public void SetLevelsSceneMain()
    {
        try
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelSceneName));
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Can't set {levelSceneName} main. Exeption.Message: {e.Message}");
        }
    }

    public void LoadScenes()
    {
        UnloadAllScenes();
        foreach (var sceneName in sceneNamesToLoad)
        {
            LoadSceneSafe(sceneName);
            eventWhenSceneLoaded.RaiseIfNotNull();
        }

        eventWhenAllScenesLoaded.RaiseIfNotNull();
    }

    private void UnloadAllScenes()
    {
        foreach (var sceneName in sceneNamesToLoad)
        {
            UnloadSceneSafe(sceneName);
        }
    }

    private void LoadSceneSafe(string sceneName)
    {
        try
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
        catch
        {
            Debug.LogError($"Scene with name: {sceneName} does not exist");
        }
    }

    private void UnloadSceneSafe(string sceneName)
    {
        try
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        catch
        {
        }
    }
}
