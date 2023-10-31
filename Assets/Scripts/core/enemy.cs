using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class enemy : MonoBehaviour, ICanDie, IHaveStatusEffects
{
    public abstract void Die();
    public abstract void Burn();
    public abstract void Electrocute();
    public abstract void Wet();
    public abstract void Stun();
    public abstract void Hurt(int _amount);
}
