using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float HP = 100;
    public float Stamina = 100;


    private void Start()
    {
        GameManager.Instance.ref_Stats = this;
    }
    public void HP_Modify(float amount)
    {
        HP += amount;
        if (HP < 0) HP = 0;
        if (HP > 100) HP = 100;

        if (amount < 0) GameManager.Instance.ref_messagespawner.SpawnMessage(Mathf.RoundToInt(amount).ToString(), Color.red, GameManager.Instance.ref_Player.transform.position);
        if (amount > 0) GameManager.Instance.ref_messagespawner.SpawnMessage(Mathf.RoundToInt(amount).ToString(), Color.green, GameManager.Instance.ref_Player.transform.position);

    }

    public void Stamina_Modify(float amount)
    {
        Stamina += amount;
        if (Stamina < 0) Stamina = 0;
        if (Stamina > 100) Stamina = 100;
    }

    public void Stamina_Modify_WithMessage(float amount)
    {
        Stamina += amount;
        if (Stamina < 0) Stamina = 0;
        if (Stamina > 100) Stamina = 100;
        if (amount < 0) GameManager.Instance.ref_messagespawner.SpawnMessage(Mathf.RoundToInt(amount).ToString(), Color.red + Color.cyan, GameManager.Instance.ref_Player.transform.position);
        if (amount > 0) GameManager.Instance.ref_messagespawner.SpawnMessage(Mathf.RoundToInt(amount).ToString(), Color.green + Color.cyan, GameManager.Instance.ref_Player.transform.position);
    }
}
