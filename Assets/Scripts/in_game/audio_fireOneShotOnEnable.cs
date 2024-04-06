using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_fireOneShotOnEnable : MonoBehaviour
{
    public AudioManager.sfxtype ourType;


    private void OnEnable()
    {
        if (AudioManager.Instance) AudioManager.Instance.play_sfx(ourType);
    }
}
