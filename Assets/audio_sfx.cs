using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_sfx : MonoBehaviour
{
    public List<AudioClip> ourSounds;
    private AudioSource ourAudioSource;
    public float pitch_min = 0.75f;
    public float pitch_max = 1.25f;

    public float WaitBeforePlay = 0;
    public float LenghtOffset = 0.1f;

    private void Awake()
    {
        ourAudioSource = GetComponent(typeof(AudioSource)) as AudioSource;
    }

    public void PlaySound()
    {
        if (WaitBeforePlay > 0) return;

        int soundIndex = Random.Range(0, ourSounds.Count - 1);

        WaitBeforePlay = (ourSounds[soundIndex].length + LenghtOffset);
        ourAudioSource.pitch = Random.Range(pitch_min, pitch_max);
        ourAudioSource.PlayOneShot(ourSounds[soundIndex]);
    }

    private void Update()
    {
        if (WaitBeforePlay > 0) WaitBeforePlay = WaitBeforePlay -= 1 * Time.deltaTime;
    }
}
