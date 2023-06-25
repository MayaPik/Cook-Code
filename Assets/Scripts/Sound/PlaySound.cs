using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlaySound : MonoBehaviour
{
    public SoundEventManager soundEventManager;
    public enum SoundOptions
    {
        Yummy,
        Oops,
        Drag,
        Drop
    }

    public SoundOptions selectedSoundOption; // Selected sound option from the enum
    public AudioSource audioSource; // Reference to the AudioSource component
    [SerializeField] bool isUIAwake;

    void Start()
    {
        soundEventManager = FindObjectOfType<SoundEventManager>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component from the GameObject
    }

    void OnEnable() {
        if (isUIAwake)
            {
            soundEventManager = FindObjectOfType<SoundEventManager>();
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component from the GameObject
            TriggerSoundEvent();
            }
        }
    
    public void TriggerSoundEvent()
    {
        if (soundEventManager != null && audioSource != null)
        {
            string eventName = selectedSoundOption.ToString(); // Convert the selected enum value to a string
            AudioEventTitled selectedAudioEvent = soundEventManager.audioEvent.Find(ae => ae.name == eventName);
            if (selectedAudioEvent != null)
            {
                selectedAudioEvent.sound.PlaySound(audioSource); // Pass the AudioSource component to the PlaySound method
            }
            else
            {
                Debug.LogWarning("Selected sound event does not exist!");
            }
        }
        else
        {
            Debug.LogWarning("SoundEventManager or AudioSource not found!");
        }
    }
}