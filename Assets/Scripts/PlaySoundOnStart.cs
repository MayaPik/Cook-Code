using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] AudioClip _clip;
    void OnEnable()
    {
        if (_clip){
        SoundManager.Instance.PlaySound(_clip);
        }
    }
}
