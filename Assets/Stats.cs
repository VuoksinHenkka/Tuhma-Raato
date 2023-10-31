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

    }

    public void Stamina_Modify(float amount)
    {
        Stamina += amount;
        if (Stamina < 0) Stamina = 0;
        if (Stamina > 100) Stamina = 100;

    }
}
