using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioEventTitled
{
    public string name;
    public AudioEvent sound;
}

public class SoundEventManager : MonoBehaviour
{
    public static SoundEventManager Instance;

    [SerializeField]
    public List<AudioEventTitled> audioEvent = new List<AudioEventTitled>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}