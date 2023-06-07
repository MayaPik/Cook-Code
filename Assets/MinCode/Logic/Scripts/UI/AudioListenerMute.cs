using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerMute : MonoBehaviour
{
    public void SetSound(bool newValue)
    {
        AudioListener.volume = newValue ? 1 : 0;
    }
}
