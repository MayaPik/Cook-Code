using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressSever : MonoBehaviour
{
    public GameObject nextObject;
    public int LevelToUnlock;
    public string WorldName;

    private void Update() {
        if (nextObject.activeSelf) {
            UnlockLevel();
        }
    }

    private void UnlockLevel() {
        if (PlayerPrefs.GetInt("LevelAvailable" + WorldName) > LevelToUnlock) {
            // Level is already unlocked
        } else {
            PlayerPrefs.SetInt("LevelAvailable" + WorldName, LevelToUnlock);
        }
    }
}
