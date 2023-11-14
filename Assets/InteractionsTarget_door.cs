using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsTarget_door : InteractionsTarget
{
    public interactions_door doortarget;


    public override void Receive(ItemDefiner _interactWith)
    {
        doortarget.Receive(_interactWith);
    }
}
 