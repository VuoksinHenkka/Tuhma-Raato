using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : ItemReceiver, IHaveName, IHaveLimitedUseRange
{
    public ItemDefiner ourItem;
    public bool Spawned = false;


    private void Start()
    {
        GameManager.Instance.onGameBegin += Respawn;
    }

    private void Respawn()
    {
        Spawned = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
       if(GameManager.Instance) GameManager.Instance.onGameBegin -= Respawn;

    }


    private void OnEnable()
    {
        Spawned = true;
    }


    public string GiveName()
    {
        return ourItem.Name;
    }
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
