using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Event", menuName = "Audio Event")]
public class AudioEvent : ScriptableObject
{
    public AudioClip audioClip;
    public float volume = 1f;
    public bool isEnabled = true;
    
    public void PlaySound(AudioSource audioSource)
    {
        if (isEnabled)
        {
            audioSource.PlayOneShot(audioClip, volume);
        }
    }

    public void ChangeStatus()
    {
        if (isEnabled) {
            isEnabled = false;
        } else {
            isEnabled = true;
        }
    }
}
