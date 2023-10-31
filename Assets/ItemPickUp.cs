using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : ItemReceiver
{
    public ItemDefiner ourItem;
    public override void Receive(ItemDefiner received)
    {
        
        bool CanPick = GameManager.Instance.ref_ItemSolver.Try_Pickup(ourItem);
        if (CanPick) gameObject.SetActive(false);
    }
}
