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

    public audio_sfx gore;
    public audio_sfx step;
    public audio_sfx stepfast;
    public audio_sfx player_hit;
    public audio_sfx player_cry;
    public audio_sfx attack_shoot;
    public audio_sfx attack_slash;
    public audio_sfx attack_swing;
    public audio_sfx heal;
    public audio_sfx opendoor;
    public audio_sfx lockeddoor;
    public audio_sfx openstash;
    public audio_sfx pickupitem;

    public enum sfxtype { gore,step,stepfast,player_hit,player_cry,attack_shoot,attack_slash,attack_swing,heal,opendoor,lockeddoor,openstash,pickupitem};

    public void play_sfx(sfxtype toplay)
    {
        switch (toplay)
        {
            case sfxtype.gore:
                gore.PlaySound();
                break;
            case sfxtype.step:
                step.PlaySound();
                break;
            case sfxtype.stepfast:
                stepfast.PlaySound();
                break;
            case sfxtype.player_hit:
                player_hit.PlaySound();
                break;
            case sfxtype.player_cry:
                player_cry.PlaySound();
                break;
            case sfxtype.attack_shoot:
                attack_shoot.PlaySound();
                break;
            case sfxtype.attack_slash:
                attack_slash.PlaySound();
                break;
            case sfxtype.attack_swing:
                attack_swing.PlaySound();
                break;
            case sfxtype.heal:
                heal.PlaySound();
                break;
            case sfxtype.opendoor:
                opendoor.PlaySound();
                break;
            case sfxtype.lockeddoor:
                lockeddoor.PlaySound();
                break;
            case sfxtype.openstash:
                openstash.PlaySound();
                break;
            case sfxtype.pickupitem:
                pickupitem.PlaySound();
                break;
        }
    }

    public void parseInteractSoundFrom(ItemDefiner usedItem)
    {
        switch (usedItem.Name)
        {
            case "Knife":
                AudioManager.Instance.play_sfx(sfxtype.attack_slash);
                break;
            case "Crowbar":
                AudioManager.Instance.play_sfx(sfxtype.attack_swing);
                break;
            case "Hammer":
                AudioManager.Instance.play_sfx(sfxtype.attack_swing);
                break;
            case "Axe":
                AudioManager.Instance.play_sfx(sfxtype.attack_slash);
                break;
            case "Pistol":
                AudioManager.Instance.play_sfx(sfxtype.attack_shoot);
                break;
            case "Shotgun":
                AudioManager.Instance.play_sfx(sfxtype.attack_shoot);
                break;
            case "Machinegun":
                AudioManager.Instance.play_sfx(sfxtype.attack_shoot);
                break;
            default:
                break;
        }
    }

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
                AudioSource toPlay = MusicTracks[Random.Range(0, MusicTracks.Count)];
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
