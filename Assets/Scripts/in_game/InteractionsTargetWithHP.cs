using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsTargetWithHP : InteractionsTarget, ICanDie
{


    public void Die() { }
    public void Hurt(int amount) { }
    public string GiveCurrentHP()
    {
        if (target != null) return target.GetComponent<ICanDie>().GiveCurrentHP();
        else return "0";
    }


}
