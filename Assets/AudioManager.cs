using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null) Debug.Log("NO AUDIOMANAGER INSTANCE FOUND");
            return _instance;
        }
        set { _instance = value; }
    }


    private void Awake()
    {
        if (_instance == null)
        _instance = this;

        foreach(Transform foundChild in Jukebox)
        {
            MusicTracks.Add(foundChild.GetComponent<AudioSource>());
        }
    }

    public AudioSource PlayOneshots;
    public AudioClip[] SFX_ZombieYells;
    private int ZombieYellsCount = 0;

    public List<AudioSource> MusicTracks;
    public Transform Jukebox;
    public float Song_LeftToPlay = 0;
    private float TimeBetweenSongs = 60;
    private float CurrenTimeBetweenSongs = 60;


    private void Update()
    {
        if (Song_LeftToPlay == 0)
        {
            if (CurrenTimeBetweenSongs != 0)
            {
                CurrenTimeBetweenSongs = Mathf.Clamp(CurrenTimeBetweenSongs -= 1 * Time.deltaTime, 0, 100);
            }
            else
            {
                AudioSource toPlay = MusicTracks[Random.Range(0, MusicTracks.Count - 1)];
                Song_LeftToPlay = toPlay.clip.length;
                toPlay.Play();
                CurrenTimeBetweenSongs = TimeBetweenSongs;
            }
        }
        else
        {
            Song_LeftToPlay = Mathf.Clamp(Song_LeftToPlay -= 1 * Time.deltaTime, 0, 99999999);
        }
    }



    public void SFX_ZombieYell()
    {
        if (ZombieYellsCount == 2) return;
        else StartCoroutine(PlayZombieYell());
    }
    IEnumerator PlayZombieYell()
    {
        ZombieYellsCount = Mathf.Clamp(ZombieYellsCount += 1, 0, 2);
        PlayOneshots.PlayOneShot(SFX_ZombieYells[Random.Range(0, SFX_ZombieYells.Length)]);
        yield return new WaitForSeconds(4);
        ZombieYellsCount = Mathf.Clamp(ZombieYellsCount -= 1, 0, 2);
    }
}
