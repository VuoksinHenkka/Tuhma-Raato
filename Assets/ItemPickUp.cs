using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : ItemReceiver
{
    public ItemDefiner ourItem;
    public override void Receive(ItemDefiner received)
    {
        
        bool CanPick = GameManager.Instance.ref_ItemSolver.Try_Pickup(ourItem);
        if (CanPick)
        {
            GameManager.Instance.ref_messagespawner.SpawnMessage(("Picked up " + ourItem.Name), Color.white, GameManager.Instance.ref_Player.transform.position);
            gameObject.SetActive(false);
        }
        else
        {
            GameManager.Instance.ref_messagespawner.SpawnMessage(("Inventory is full"), Color.red, GameManager.Instance.ref_Player.transform.position);
        }
    }
}
