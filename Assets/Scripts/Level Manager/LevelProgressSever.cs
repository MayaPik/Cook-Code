using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressSever : MonoBehaviour
{
    [SerializeField] GameObject nextObject;
    [SerializeField]  int LevelToUnlock;
    [SerializeField]  string WorldName;

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
