using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{

    public enum InsanityFX {None, SpeedUpGame, InvertControls, EatStamina, EatHP, CleanInventory}
    public InsanityFX CurrentInsanityFX = InsanityFX.None;

    public float HP = 100;
    public float Stamina = 100;
    public float Sanity = 100;
    public bool Sanity_ItsNight = false;
    public List<string> insanityMessages;
    public int Keys = 0;
    public int RFIDs = 0;


    public int _liquorice = 0;
    public int _vodka = 0;
    public int _water = 0;
    public int _lint = 0;
    public int _money = 0;
    public int _zombies = 0;

    public delegate void OnHurt();
    public event    OnHurt onHurt;



    public void Reset()
    {
        HP = 100;
        Stamina = 100;
        Sanity = 100;
        Keys = 0;
        RFIDs = 0;
        _liquorice = 0;
        _vodka = 0;
        _water = 0;
        _lint = 0;
        _money = 0;
        _zombies = 0;
    }

    private void Start()
    {
        GameManager.Instance.ref_Stats = this;
    }

    public bool Use_Key()
    {
        if (Keys == 0) return false;
        else Keys--;
        return true;
    }

    public bool Use_RFIDs()
    {
        if (RFIDs == 0) return false;
        else RFIDs--;
        return true;
    }

    public void AddKey()
    {
        Keys++;
    }

    public void AddRFID()
    {
        RFIDs++;
    }

    public void HP_Modify(float amount)
    {
        HP += amount;
        if (HP < 0) HP = 0;
        if (HP > 100) HP = 100;

        if (amount < 0) GameManager.Instance.ref_messagespawner.SpawnMessage(Mathf.RoundToInt(amount).ToString(), Color.red, GameManager.Instance.ref_Player.transform.position);
        if (amount > 0) GameManager.Instance.ref_messagespawner.SpawnMessage(Mathf.RoundToInt(amount).ToString(), Color.green, GameManager.Instance.ref_Player.transform.position);

        if (amount < 0 && onHurt != null) onHurt.Invoke();

    }
    public void HP_ModifyNoMessage(float amount)
    {
        HP += amount;
        if (HP < 0) HP = 0;
        if (HP > 100) HP = 100;
    }

    public void Stamina_Modify(float amount)
    {
        Stamina += amount;
        if (Stamina < 0) Stamina = 0;
        if (Stamina > 100) Stamina = 100;
    }

    public void Sanity_Modify(float amount)
    {
        if ((Sanity - amount) <= 0.1)
        {
            Sanity = 100;
            StartCoroutine(GoCrazy());
        }
        else Sanity += amount;

        if (Sanity < 0) Sanity = 0;
        if (Sanity > 100) Sanity = 100;
    }

    public void Stamina_Modify_WithMessage(float amount)
    {
        Stamina += amount;
        if (Stamina < 0) Stamina = 0;
        if (Stamina > 100) Stamina = 100;
        if (amount < 0) GameManager.Instance.ref_messagespawner.SpawnMessage(Mathf.RoundToInt(amount).ToString(), Color.red + Color.cyan, GameManager.Instance.ref_Player.transform.position);
        if (amount > 0) GameManager.Instance.ref_messagespawner.SpawnMessage(Mathf.RoundToInt(amount).ToString(), Color.green + Color.cyan, GameManager.Instance.ref_Player.transform.position);
    }


    IEnumerator GoCrazy()
    {
        AudioManager.Instance.play_sfx(AudioManager.sfxtype.player_cry);
        GameManager.Instance.ref_messagespawner.SpawnMessage(insanityMessages[Random.Range(0, insanityMessages.Count-1)], Random.ColorHSV(0.6f,0.9f,1,1,0.8f,1), GameManager.Instance.ref_Player.transform.position);
        yield return null;
        int randomFX = Random.Range(0, 5);
        switch(randomFX)
        {
            case 0:
                CurrentInsanityFX = InsanityFX.SpeedUpGame; break;
            case 1:
                CurrentInsanityFX = InsanityFX.InvertControls; break;
            case 2:
                CurrentInsanityFX = InsanityFX.CleanInventory;
                GameManager.Instance.ref_ItemSolver.SpendItemInHand();
                break;
            case 3:
                CurrentInsanityFX = InsanityFX.EatStamina;
                break;
            case 4:
                CurrentInsanityFX = InsanityFX.EatHP; break;
            default:
                CurrentInsanityFX = InsanityFX.EatStamina;
                break;
        }
        yield return new WaitForSeconds(15f);
        CurrentInsanityFX = InsanityFX.None;

    }
}
